using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace WeCleared.Core
{
    /// <summary>
    /// Classe qui contien les méthodes pour gérer les addons
    /// </summary>
    public class AddonsParser
    {
        #region Constructeur

        /// <summary>
        /// Point d'acces vers le formulaire principal
        /// </summary>
        private frmWeCleared Base { get; set; }

        /// <summary>
        /// Retourne une instance du addon parser
        /// </summary>
        /// <param name="p_Base"></param>
        public AddonsParser(frmWeCleared p_Base)
        {
            Base = p_Base;
            Addons = new List<Addon>();
            AddonsList = new List<AddonDataSet>();
        }

        #endregion

        #region Variables

        /// <summary>
        /// Définie à true quand la mise à jour débute
        /// </summary>
        internal bool Updating { get; set; }

        /// <summary>
        /// Liste des addons
        /// </summary>
        public List<Addon> Addons { get; set; }

        /// <summary>
        /// Data set des addons
        /// </summary>
        public List<AddonDataSet> AddonsList { get; set; }

        /// <summary>
        /// Chemin d'accès vers l'API Web
        /// </summary>
#if DEBUG
        internal static string WebPath = "http://192.168.0.59:44443";
#elif LOCAL
        internal static string WebPath = "http://192.168.0.59:44443";
#else
        internal static string WebPath = "http://" + Dns.GetHostAddresses("codemylife.ca")[0] + ":44443";
#endif

        #endregion

        #region Fonctions

        /// <summary>
        /// Point d'entré appelé toutes les heures (Pulse)
        /// </summary>
        public void Parse()
        {
            if (!Updating)
            {
                var l_AddonsList = new List<Addon>();
                // Aller chercher les données à partir de l'API et les storer dans la liste de addons
                l_AddonsList = UpdateAddons(l_AddonsList);
                UpdateAddonsLists(l_AddonsList);

                var l_AddonsListDataSet = new List<AddonDataSet>();

                foreach (var l_Addon in l_AddonsList)
                {
                    l_Addon.Local = GetAddonVersion(l_Addon);
                    l_AddonsListDataSet.Add(new AddonDataSet(l_Addon.Nom, l_Addon.Description, l_Addon.Local, l_Addon.Web));
                }
                Addons = l_AddonsList;
                AddonsList = l_AddonsListDataSet.Where(x => Base.Settings.GetEnabled(x.Nom)).ToList();
            }
        }

        /// <summary>
        /// Mettre à jour les addons au besoin, mais garder les propriétés déjà existantes
        /// </summary>
        /// <param name="p_AddonsList"></param>
        private void UpdateAddonsLists(List<Addon> p_AddonsList)
        {
            // Si la quantité de addons à changé
            if (Addons.Count != p_AddonsList.Count)
            {
                Addons.Clear();
                foreach (var l_Addon in p_AddonsList)
                {
                    l_Addon.Local = GetAddonVersion(l_Addon);

                    Addons.Add(new Addon(l_Addon.Nom, l_Addon.Description, l_Addon.VersionPath, l_Addon.Web, l_Addon.WebPath));
                }
            }
            else
            {
                for (var i = 0; i < Addons.Count; i++)
                {
                    Addons[i] = UpdateAddon(p_AddonsList[i], Addons[i]);
                }
            }
            // Si la quantité de addons à changé
            p_AddonsList = p_AddonsList.Where(x => Base.Settings.GetEnabled(x.Nom)).ToList();
            if (AddonsList.Count != p_AddonsList.Count)
            {
                AddonsList.Clear();
                foreach (var l_Addon in p_AddonsList.Where(x => Base.Settings.GetEnabled(x.Nom)))
                {
                    l_Addon.Local = GetAddonVersion(l_Addon);

                    AddonsList.Add(new AddonDataSet(l_Addon.Nom, l_Addon.Description, l_Addon.Local, l_Addon.Web));
                }
            }
            else
            {
                for (var i = 0; i < AddonsList.Count; i++)
                {
                    AddonsList[i] = UpdateAddon(p_AddonsList[i], AddonsList[i]);
                }
            }
        }

        /// <summary>
        /// Met à jour les variable des addons affichés dans le DataGrid
        /// </summary>
        /// <param name="p_NewAddon"></param>
        /// <param name="p_Addon"></param>
        /// <returns></returns>
        private AddonDataSet UpdateAddon(Addon p_NewAddon, AddonDataSet p_Addon)
        {
            p_Addon.Local = GetAddonVersion(p_NewAddon);
            p_Addon.Web = p_NewAddon.Web;
            return p_Addon;
        }

        /// <summary>
        /// Met à jour les variables des addons disponibles dans le dropdown
        /// </summary>
        /// <param name="p_NewAddon"></param>
        /// <param name="p_Addon"></param>
        /// <returns></returns>
        private Addon UpdateAddon(Addon p_NewAddon, Addon p_Addon)
        {
            p_Addon.Local = GetAddonVersion(p_NewAddon);
            p_Addon.Web = p_NewAddon.Web;
            return p_Addon;
        }

        /// <summary>
        /// Lire le .toc pour trouver le # de version.
        /// </summary>
        /// <param name="p_Addon"></param>
        /// <returns></returns>
        private string GetAddonVersion(Addon p_Addon)
        {
            try
            {
                // Emplacement du fichier .toc
                var tempAddonFolder = Regex.Replace(p_Addon.Nom, "[^A-Za-z0-9()]", "");
                var tocPath = Path.Combine(Base.MainSettings["WoWPath"], p_Addon.VersionPath, p_Addon.VersionPath) + ".toc";

                // Streamreader pour trouver la ligne qui contien la version
                var l_SettingsReader = new StreamReader(tocPath);
                var l_VersionLine = "";
                while (!l_SettingsReader.EndOfStream && l_VersionLine == "")
                {
                    var l_ThisLine = l_SettingsReader.ReadLine();
                    if (l_ThisLine != null && l_ThisLine.Contains("## Version:"))
                    {
                        l_VersionLine = l_ThisLine;
                    }
                }
                l_SettingsReader.Close();

                // Regex: Conserver seulement les chiffres
                var l_AddonVersion = Regex.Replace(l_VersionLine, "[^0-9.]", "");

                return l_AddonVersion;

            }
            catch (Exception ex)
            {
                Base.ExceptionString = "Addons non trouvés, vérifiez le Dossier WoW.";
            }

            return "0.0";
        }


        /// <summary>
        /// Aller chercher la liste mise à jour des addons sur l'api.
        /// </summary>
        internal List<Addon> UpdateAddons(List<Addon> p_AddonsList)
        {
            try
            {
                var l_RestClient = new RestClient
                {
                    EndPoint = WebPath + "/api/versions/",

                };

                var l_Response = l_RestClient.MakeRequest();
                if (l_Response != String.Empty)
                    p_AddonsList = DeserialiseJson(l_Response, p_AddonsList);
            }
            catch (Exception ex)
            {
                Base.ExceptionString = "Erreur lors de la mise à jour des addons.";
#if DEBUG
                MessageBox.Show("Erreur UpdateAddons: " + ex.Message);
#endif
            }

            return p_AddonsList.OrderBy(x => x.Nom).ToList();
        }

        /// <summary>
        /// Effectuer les mises à jour
        /// </summary>
        internal void ApplyUpdates()
        {
            Updating = true;
            var l_AddonsToUpdate = AddonsList.Where(x => x.Local != x.Web && Base.Settings.GetEnabled(x.Nom)).ToList();
            if (l_AddonsToUpdate.Count != 0)
            {
                Base.StartProgressBar(5);
                var l_ProgressPerAddon = 90 / l_AddonsToUpdate.Count;
                foreach (var l_Addon in Addons)
                {
                    Base.ContinueProgress((int)l_ProgressPerAddon, "Mise à jour de " + l_Addon.Nom);
                    if (Base.Settings.GetEnabled(l_Addon.Nom) && NewerVersionAvailable(l_Addon))
                    {
                        DownloadAddon(l_Addon);
                        Base.ZipClass.ExtractAddon(l_Addon);
                    }
                }
                Base.ContinueProgress(20, "Achevement des mises à jour.");
            }
            Updating = false;
        }

        /// <summary>
        /// Vérifier si la verrsion locale et la version web correspondent
        /// </summary>
        /// <param name="p_Addon"></param>
        /// <returns></returns>
        private bool NewerVersionAvailable(Addon p_Addon)
        {
            return p_Addon.Local != p_Addon.Web;
        }

        /// <summary>
        /// Utilise un Web client pour télécharger l'Addon
        /// </summary>
        /// <param name="p_Addon"></param>
        private void DownloadAddon(Addon p_Addon)
        {
            using (var l_WebClient = new WebClient())
            {
                var l_AddonZipUri = p_Addon.WebPath.Contains("local") ? new Uri(WebPath + "/" + p_Addon.WebPath.Substring(6)) : new Uri(p_Addon.WebPath);
                var l_TempAddonFolder = Regex.Replace(p_Addon.Nom, "[^A-Za-z0-9()]", "");
                var l_TempDirectoryPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Temp");
                var l_TempsFolderZipPath = Path.Combine(l_TempDirectoryPath, l_TempAddonFolder) + ".zip";
                if (!Directory.Exists(Path.Combine(l_TempDirectoryPath)))
                {
                    Directory.CreateDirectory(l_TempDirectoryPath);
                }
                l_WebClient.DownloadFile(l_AddonZipUri, l_TempsFolderZipPath);
            }
        }

        #endregion

        #region JSON Deserializer

        /// <summary>
        /// Transforme les éléments reçus en JSON en Addons
        /// </summary>
        /// <param name="p_JsonString"></param>
        /// <param name="p_AddonsList"></param>
        /// <returns></returns>
        private List<Addon> DeserialiseJson(string p_JsonString, List<Addon> p_AddonsList)
        {

            try
            {
                //MessageBox.Show(p_JsonString);
                var l_Addons = JsonConvert.DeserializeObject<dynamic>(p_JsonString);

                foreach (var l_Addon in l_Addons)
                {
                    var l_Nom = l_Addon.Nom.ToString();
                    var l_Description = l_Addon.Description.ToString();
                    var l_VersionPath = l_Addon.VersionPath.ToString();
                    var l_Version = l_Addon.Version.ToString();
                    var l_WebPath = l_Addon.WebPath.ToString();
                    p_AddonsList.Add(new Addon(l_Nom, l_Description, l_VersionPath, l_Version, l_WebPath));
                }
            }
            catch (Exception ex)
            {
                Base.ExceptionString = "Erreur de désérialization du JSON.";
#if DEBUG
                MessageBox.Show("Erreur Deserializer: " + ex.Message);
#endif
            }
            return p_AddonsList;
        }

        #endregion
    }
}
