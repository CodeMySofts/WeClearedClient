using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeCleared.Core
{
    /// <summary>
    /// Classe qui gère le dézippage des fichiers
    /// </summary>
    /// <param name="p_Base"></param>
    public class ZipClass
    {
        #region Constructeur

        /// <summary>
        /// Point d'acces vers le formulaire principal
        /// </summary>
        private frmWeCleared Base { get; set; }

        /// <summary>
        /// Retourne une instance de la classe
        /// </summary>
        /// <param name="p_Base"></param>
        public ZipClass(frmWeCleared p_Base)
        {
            Base = p_Base;
        }

        #endregion

        #region Fonctions

        /// <summary>
        /// Extraire un addon à partir du dossier Temp
        /// </summary>
        /// <param name="p_Addon"></param>
        internal void ExtractAddon(Addon p_Addon)
        {
            try
            {
                // Extraire le zip dans le dossier Temp/AddonFolderName
                // Vérifier si les dossiers existent déjà, les enlever si besoin est
                // copier les dossier extraits

                // Racine du dossier temp
                var l_TempDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Temp");
                // Nom du zip/du dossier temp du addon
                var l_AddonZipName = Regex.Replace(p_Addon.Nom, "[^A-Za-z0-9()]", "");
                // emplacement du dossier temp du addon
                var l_AddonTempFolder = Path.Combine(l_TempDirectory, l_AddonZipName);
                // Dossier ou sont les addons de wow
                var l_AddonsFolder = Base.MainSettings["WoWPath"];

                if (Directory.Exists(l_AddonTempFolder))
                    Directory.Delete(l_AddonTempFolder, true);

                ZipFile.ExtractToDirectory(Path.Combine(l_TempDirectory, l_AddonZipName) + ".zip", l_AddonTempFolder);

                // Exception to pull ElvUI out of the master folder
                if (p_Addon.Nom == "ElvUI")
                {
                    var l_ElvUiDirectory = Directory.GetDirectories(l_AddonTempFolder)[0];
                    //Now Create all of the directories
                    foreach (var l_DirectoryPath in Directory.GetDirectories(l_ElvUiDirectory, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(l_DirectoryPath.Replace(l_ElvUiDirectory, l_AddonTempFolder));
                    }

                    //Copy all the files & Replaces any files with the same name
                    foreach (var l_CurrentPath in Directory.GetFiles(l_ElvUiDirectory, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(l_CurrentPath, l_CurrentPath.Replace(l_ElvUiDirectory, l_AddonTempFolder), true);
                    }

                    // retirer le dossier temporaire
                    Directory.Delete(l_ElvUiDirectory, true);
                }

                Thread.Sleep(1000);

                //// Créer le dossier cible
                var l_TargetDirectory = Path.Combine(l_AddonsFolder, l_AddonZipName);
                if (!Directory.Exists(l_TargetDirectory))
                {
                    Directory.CreateDirectory(l_TargetDirectory);
                }

                // Copier
                var l_Directories = Directory.GetDirectories(l_AddonTempFolder, "*");
                foreach (var l_CurrentPath in l_Directories)
                {
                    if (!Directory.Exists(l_CurrentPath.Replace(l_AddonTempFolder, l_AddonsFolder)))
                    {
                        Directory.CreateDirectory(l_CurrentPath.Replace(l_AddonTempFolder, l_AddonsFolder));
                    }
                    CopyAll(l_CurrentPath, l_CurrentPath.Replace(l_AddonTempFolder, l_AddonsFolder));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur ExtractAddons: " + ex.Message);
            }
        }

        /// <summary>
        /// Copie récursivement tout les dossiers, sous dossier, et fichiers d'un endroit vers un autre
        /// </summary>
        /// <param name="p_SourcePath"></param>
        /// <param name="p_DestinationPath"></param>
        private static void CopyAll(string p_SourcePath, string p_DestinationPath)
        {

            var l_Directories = Directory.GetDirectories(p_SourcePath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(l_Directories, p_DirPath =>
            {
                Directory.CreateDirectory(p_DirPath.Replace(p_SourcePath, p_DestinationPath));
            });

            var l_Files = Directory.GetFiles(p_SourcePath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(l_Files, p_NewPath =>
            {
                File.Copy(p_NewPath, p_NewPath.Replace(p_SourcePath, p_DestinationPath), true);
            });
        }

        #endregion
    }
}
