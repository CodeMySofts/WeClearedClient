using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SharpUpdate
{
    /// <summary>
    /// L'interface que l'application doit implémenter pour utiliser l'updater
    /// </summary>
    public interface ISharpUpdatable
    {
        /// <summary>
        /// Nom de l'application
        /// </summary>
        string ApplicationName { get; }
        /// <summary>
        /// Numéro de référence de l'application
        /// </summary>
        string ApplicationId { get; }
        /// <summary>
        /// La présente assembly
        /// </summary>
        Assembly ApplicationAssembly { get; }
        /// <summary>
        /// l'icône de l'application
        /// </summary>
        Icon ApplicationIcon { get; }
        /// <summary>
        /// le chemin d'acces du xml sur notre serveur
        /// </summary>
        Uri UpdateXmlLocation { get; }
        /// <summary>
        /// le formulaire
        /// </summary>
        Form Context { get; }
    }
}
