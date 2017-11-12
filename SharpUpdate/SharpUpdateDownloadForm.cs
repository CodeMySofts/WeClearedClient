using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SharpUpdate
{
    /// <summary>
    /// Formulaire présenté durant le téléchargement
    /// </summary>
    internal partial class SharpUpdateDownloadForm : Form
    {
        /// <summary>
        /// Le client Web utilisé pour télécharger
        /// </summary>
        private WebClient webClient;

        /// <summary>
        /// Le thread utilisé pour télécharger, hasher le fichier et vérifier la correspondance
        /// </summary>
        private BackgroundWorker bgWorker;

        /// <summary>
        /// Le nom du fichier temporaire
        /// </summary>
        private string tempFile;

        /// <summary>
        /// Le md5 du fichier à télécharger
        /// </summary>
        private string md5;

        /// <summary>
        /// Le chemin d'accès du fichier temporaire
        /// </summary>
        internal string tempFilePath
        {
            get { return this.tempFile; }
        }

        /// <summary>
        /// Crée une instance du présent formulaire
        /// </summary>
        /// <param name="location"></param>
        /// <param name="md5"></param>
        /// <param name="programIcon"></param>
        internal SharpUpdateDownloadForm(Uri location, string md5, Icon programIcon)
        {
            InitializeComponent();

            if (programIcon != null)
            {
                Icon = programIcon;
            }

            tempFile = Path.GetTempFileName();

            this.md5 = md5;

            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

            try
            {
                webClient.DownloadFileAsync(location, tempFile);
            }
            catch
            {
                DialogResult = DialogResult.No;
                Close();
            }
        }

        /// <summary>
        /// Met à jour la progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = String.Format("Reçu {0} de {1} bytes.", FormatBytes(e.BytesReceived, 1, true), FormatBytes(e.TotalBytesToReceive, 1, true));
        }

        /// <summary>
        /// Transform la quantité de bytes téléchargé en Kb/Mb/Gb
        /// </summary>
        /// <param name="bytes">La quantité de bytes actuelle</param>
        /// <param name="decimalPlaces">La quantité de décimales à présenter</param>
        /// <param name="showByteType">Ajouter ou non le type de bytes à la fin</param>
        /// <returns>La quantité simplifiée</returns>
        private string FormatBytes(long bytes, int decimalPlaces, bool showByteType)
        {
            var newBytes = bytes;
            var formatString = "{0";
            var byteType = "B";

            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                newBytes /= 1048576;
                byteType = "MB";
            }
            else
            {
                newBytes /= 1073741824;
                byteType = "GB";
            }
            if (decimalPlaces > 0)
            {
                formatString += ":0.";
            }
            for (var i = 0; i < decimalPlaces; i++)
            {
                formatString += "0";
            }

            formatString += "}";

            if (showByteType)
            {
                formatString += byteType;
            }

            return string.Format(formatString, newBytes);
        }

        /// <summary>
        /// Démarre le backgroundworker quand le fichier à fini de télécharger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DialogResult = DialogResult.No;
                Close();
            }
            else if (e.Cancelled)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
            else
            {
                lblProgress.Text = "Verifying Download...";
                progressBar.Style = ProgressBarStyle.Marquee;

                bgWorker.RunWorkerAsync(new string[] { tempFile, md5 });
            }
        }

        /// <summary>
        /// Effectue le hashing du md5 et compare à celui dans le fichier xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var file = ((string[])e.Argument)[0];
            var updateMd5 = ((string[])e.Argument)[1];
            var hashedFileMd5 = Hasher.HashFile(file, HashType.MD5);
            e.Result = hashedFileMd5 != updateMd5 ? DialogResult.No : DialogResult.OK;
        }

        /// <summary>
        /// Ferme le présent formulaire quand le worker à terminé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = (DialogResult) e.Result;
            Close();
        }

        /// <summary>
        /// Événement qui s'execute si on ferme le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharpUpdateDownloadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (webClient.IsBusy)
            {
                webClient.CancelAsync();
                DialogResult = DialogResult.Abort;
            }

            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
                DialogResult = DialogResult.Abort;
            }
        }
    }
}
