using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WeCleared.Core
{
    /// <summary>
    /// Classe utilisée pour contacter l'API
    /// </summary>
    internal class RestClient
    {
        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public RestClient()
        {
            EndPoint = string.Empty;
            HttpMethod = HttpVerb.GET;
        }

        #endregion

        #region Variables

        /// <summary>
        /// Chemin d'acces complet de l'API Web
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// Méthode Http utilisée pour aller chercher le fichier Json
        /// </summary>
        public HttpVerb HttpMethod { get; set; }

        #endregion

        #region Méthodes

        public string MakeRequest()
        {
            try
            {
                var l_ResponseValue = string.Empty;

                var l_Resquest = (HttpWebRequest)HttpWebRequest.Create(EndPoint + "get?type=json");
                l_Resquest.ContentType = "application/json";
                l_Resquest.Method = HttpMethod.ToString();
                l_Resquest.Accept = "application/json; odata=verbose";
                l_Resquest.UserAgent = "Mozilla/4.0+(compatible;+MSIE+5.01;+Windows+NT+5.0";
                l_Resquest.Timeout = 30000;

                using (var l_Response = (HttpWebResponse)l_Resquest.GetResponse())
                {
                    if (l_Response.StatusCode == HttpStatusCode.OK)
                    {
                        // Process the response Stream (could be JSON, XML or HTML)
                        using (var l_ResponseStream = l_Response.GetResponseStream())
                        {
                            if (l_ResponseStream != null)
                            {
                                using (var l_Reader = new StreamReader(l_ResponseStream))
                                {
                                    l_ResponseValue = l_Reader.ReadToEnd();
                                }
                            }
                        }
                    }
                }

                return l_ResponseValue;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("MakeRequest" + ex.ToString());
#endif
                return string.Empty;
            }
        }

#endregion
    }
#region Enumérations

    /// <summary>
    /// Énumération de types de web request
    /// </summary>
    internal enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

#endregion
}
