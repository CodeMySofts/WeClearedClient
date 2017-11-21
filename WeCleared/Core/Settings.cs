using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeCleared.Core
{
    /// <summary>
    /// Classe utilisée pour enregistrer en lire les paramêtres de l'application
    /// </summary>
    internal class SettingsClass
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
        internal SettingsClass(frmWeCleared p_Base)
        {
            Base = p_Base;
        }

        #endregion

        #region Variables

        private string MainFile = "AppData.txt";

        #endregion

        #region Fonctions

        /// <summary>
        /// Lis les options enregistrées dans un fichier à l'endroit spécifié
        /// </summary>
        internal void ReadMainSettings()
        {
            try
            {
                var l_SettingsReader = new StreamReader(MainFile);
                while (!l_SettingsReader.EndOfStream)
                {
                    var l_ThisSetting = l_SettingsReader.ReadLine()?.Split(';');
                    if (l_ThisSetting != null && Base.MainSettings.ContainsKey(l_ThisSetting[0]))
                    {
                        Base.MainSettings[l_ThisSetting[0]] = l_ThisSetting[1];
                    }
                    else
                    {
                        if (l_ThisSetting != null) Base.MainSettings.Add(l_ThisSetting[0], l_ThisSetting[1]);
                    }
                }
                l_SettingsReader.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Écris les options dans un fichier à l'endroit spécifié
        /// </summary>
        internal void WriteMainSettings()
        {
            try
            {
                var l_SettingsWriter = new StreamWriter(MainFile);
                foreach (var l_Setting in Base.MainSettings)
                {
                    l_SettingsWriter.WriteLine(l_Setting.Key + ";" + l_Setting.Value);
                }
                l_SettingsWriter.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Voir si un parametre est activé
        /// </summary>
        /// <param name="p_AddonName"></param>
        /// <returns></returns>
        internal bool GetEnabled(string p_AddonName)
        {
            if (Base.MainSettings.ContainsKey(p_AddonName))
            {
                return Base.MainSettings[p_AddonName] == "True";
            }
            return false;
        }

        /// <summary>
        /// Activer ou désactiver un parametre
        /// </summary>
        /// <param name="p_SettingName"></param>
        /// <param name="p_Value"></param>
        internal void SetEnabled(string p_SettingName, bool p_Value)
        {
            if (Base.MainSettings.ContainsKey(p_SettingName))
            {
                Base.MainSettings[p_SettingName] = p_Value.ToString();
                WriteMainSettings();
            }
            else
            {
                Base.MainSettings.Add(p_SettingName, p_Value.ToString());
                WriteMainSettings();
            }
        }

#endregion
    }
}
