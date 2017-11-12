using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SharpUpdate
{
    /// <summary>
    /// Permet de faire des mises à jour d'application en C#
    /// </summary>
    public class SharpUpdater
    {
        /// <summary>
        /// Contien les informations de l'application à mettre à jour
        /// </summary>
        private ISharpUpdatable applicationInfo;

        /// <summary>
        /// Thread qui tourne en arriere plan pour faire la mise à jour
        /// </summary>
        private BackgroundWorker bgWorker;

        /// <summary>
        /// Crée une instance de l'updater
        /// </summary>
        /// <param name="p_ApplicationInfo"></param>
        public SharpUpdater(ISharpUpdatable p_ApplicationInfo)
        {
            applicationInfo = p_ApplicationInfo;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Vérifie si une mise à jour est disponible
        /// Ouvre une fenetre de confirmation si tel est le cas
        /// </summary>
        public void DoUpdate()
        {
            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync(applicationInfo);
            }
        }

        /// <summary>
        /// Cherche et Parse un fichier xml en provenance de l'addresse de l'update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var application = (ISharpUpdatable) e.Argument;

            if (!SharpUpdateXml.ExistsOnServer(application.UpdateXmlLocation))
            {
                e.Cancel = true;
            }
            else
            {
                e.Result = SharpUpdateXml.Parse(application.UpdateXmlLocation, application.ApplicationId);
            }
        }

        /// <summary>
        /// Quand la mise à jour est prête
        /// Demander confirmation de l'utilisateur avant de redémarrer et mettre à jour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                var update = (SharpUpdateXml) e.Result;

                if (update != null && update.IsNewerThan(applicationInfo.ApplicationAssembly.GetName().Version))
                {
                    if (new SharpUpdateAcceptForm(applicationInfo, update).ShowDialog(applicationInfo.Context) == DialogResult.Yes)
                    {
                        DownloadUpdate(update);
                    }
                }
            }
        }

        /// <summary>
        /// Télécharge la mise à jour et l'installe
        /// </summary>
        /// <param name="update">Les informations de Xml</param>
        private void DownloadUpdate(SharpUpdateXml update)
        {
            var form = new SharpUpdateDownloadForm(update.Uri, update.MD5, applicationInfo.ApplicationIcon);
            var result = form.ShowDialog(applicationInfo.Context);

            if (result == DialogResult.OK)
            {
                var currentPath = applicationInfo.ApplicationAssembly.Location;
                var newPath = Path.GetDirectoryName(currentPath) + "\\" + update.Filename;

                UpdateApplication(form.tempFilePath, currentPath, newPath, update.LaunchArgs);

                Application.Exit();
            }
            else if (result == DialogResult.Abort)
            {
                MessageBox.Show("La mise à jour à été abortée.\nLe logiciel n'a pas été modifié.", "Mise à jour abortée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Une erreur est survenue lors de la mise à jour.\nVeuillez essayer de nouveau.", "Échec de la mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// En utilisant le cmd.exe, fermer le logiciel, effacer l'original et déplacer l'update à cet endroit
        /// </summary>
        /// <param name="tempFilePath"></param>
        /// <param name="currentPath"></param>
        /// <param name="newPath"></param>
        /// <param name="lauchArgs"></param>
        private void UpdateApplication(string tempFilePath, string currentPath, string newPath, string lauchArgs)
        {
            var argument = "/C Choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & Choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";

            var info = new ProcessStartInfo();
            info.Arguments = string.Format(argument, currentPath, tempFilePath, newPath, Path.GetDirectoryName(newPath), Path.GetFileName(newPath), lauchArgs);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            Process.Start(info);
        }
    }
}
