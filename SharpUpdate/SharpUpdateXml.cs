using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;

namespace SharpUpdate
{
    /// <summary>
    /// Contien les informations de mise à jour
    /// </summary>
    public class SharpUpdateXml
    {
        private Version version;
        private Uri uri;
        private string fileName;
        private string md5;
        private string description;
        private string launchArgs;

        /// <summary>
        /// Le # de version de la mise à jour
        /// </summary>
        internal Version Version => version;

        /// <summary>
        /// L'url de la binary d'update
        /// </summary>
        internal Uri Uri => uri;

        /// <summary>
        /// Le nom de fichier de la binary
        /// </summary>
        internal string Filename => fileName;

        /// <summary>
        /// Le MD5 de la mise a jour
        /// </summary>
        internal string MD5 => md5;

        /// <summary>
        /// La description de la mise a jour
        /// </summary>
        internal string Description => description;

        /// <summary>
        /// Les argument à passer au lancement de la mise a jour
        /// </summary>
        internal string LaunchArgs => launchArgs;

        /// <summary>
        /// Constructeur qui crée l'objet
        /// </summary>
        /// <param name="version"></param>
        /// <param name="uri"></param>
        /// <param name="filename"></param>
        /// <param name="md5"></param>
        /// <param name="description"></param>
        /// <param name="launchArgs"></param>
        public SharpUpdateXml(Version version, Uri uri, string filename, string md5, string description, string launchArgs)
        {
            this.version = version;
            this.uri = uri;
            this.fileName = filename;
            this.md5 = md5;
            this.description = description;
            this.launchArgs = launchArgs;
        }

        /// <summary>
        /// Comparer les versions et voir si la nouvelle version est plus récente
        /// </summary>
        /// <param name="version"></param>
        /// <returns>True si la version serveur est nouvelle</returns>
        internal bool IsNewerThan(Version version)
        {
            return this.version > version;
        }

        /// <summary>
        /// Vérifier l'Uri et faire sur que le fichier existe
        /// </summary>
        /// <param name="location"></param>
        /// <returns>True si le fichier existe</returns>
        internal static bool ExistsOnServer(Uri location)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(location.AbsoluteUri);
                var response = (HttpWebResponse)request.GetResponse();
                response.Close();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Récupérer les informations et les inclure dans un objet SharpUpdateXml
        /// </summary>
        /// <param name="location"></param>
        /// <param name="appID"></param>
        /// <returns>La présente classe</returns>
        internal static SharpUpdateXml Parse(Uri location, string appID)
        {
            try
            {
                // Charger le document Xml
                ServicePointManager.ServerCertificateValidationCallback = (s, ce, ch, ssl) => true;
                var l_Doc = new XmlDocument();
                l_Doc.Load(location.AbsoluteUri);

                // Voir l'id de l'application dans le document Xml 
                var l_Node = l_Doc.DocumentElement.SelectSingleNode("//update[@appId='" + appID + "']");

                // Si la node n'exite pas, il n'y a pas de mises à jour
                if (l_Node == null)
                {
                    return null;
                }

                // Récupérer les informations
                var version = Version.Parse(l_Node["version"].InnerText);
                var url = l_Node["url"].InnerText;
                var fileName = l_Node["fileName"].InnerText;
                var md5 = l_Node["md5"].InnerText;
                var description = l_Node["description"].InnerText;
                var launchArgs = l_Node["launchArgs"] != null ? l_Node["launchArgs"].InnerText : "";

                return new SharpUpdateXml(version, new Uri(url), fileName, md5, description, launchArgs);
            }
            catch
            {
                return null;
            }
        }
    }
}
