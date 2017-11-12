using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCleared.Core
{
    /// <summary>
    /// Classe des addons utilisée pour faire les vérifications via le parser
    /// </summary>
    public class Addon
    {
        #region Constructeur

        /// <summary>
        /// Constructeur d'addon.
        /// </summary>
        /// <param name="p_Nom"></param>
        /// <param name="p_Description"></param>
        /// <param name="p_VersionPath"></param>
        /// <param name="p_Web"></param>
        /// <param name="p_WebPath"></param>
        public Addon(string p_Nom, string p_Description, string p_VersionPath, string p_Web, string p_WebPath)
        {
            Nom = p_Nom;
            Description = p_Description;
            Web = p_Web;
            VersionPath = p_VersionPath;
            WebPath = p_WebPath;
        }

        #endregion
        
        #region Propriétés

        /// <summary>
        /// Nom de l'Addon
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Description de l'Addon
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Version Web de l'addon
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// Version locale de l'Addon
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Chemin de téléchargement de mise à jour
        /// </summary>
        public string WebPath { get; set; }

        /// <summary>
        /// Chemin pour trouver la version locale
        /// </summary>
        public string VersionPath { get; set; }

        #endregion
    }

    /// <summary>
    /// Classe utilisée pour représenter les variables dans le DataGrid
    /// </summary>
    public class AddonDataSet
    {
        #region Constructeur

        /// <summary>
        /// Constructeur d'addon.
        /// </summary>
        /// <param name="p_Nom"></param>
        /// <param name="p_Description"></param>
        /// <param name="p_Local"></param>
        /// <param name="p_Web"></param>
        public AddonDataSet(string p_Nom, string p_Description, string p_Local, string p_Web)
        {
            Nom = p_Nom;
            Description = p_Description;
            Local = p_Local;
            Web = p_Web;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de l'Addon
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Description de l'Addon
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Version locale de l'Addon
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Version Web de l'addon
        /// </summary>
        public string Web { get; set; }

        #endregion
    }
}
