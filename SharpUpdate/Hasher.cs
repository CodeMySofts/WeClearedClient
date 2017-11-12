using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpUpdate
{
    /// <summary>
    /// Type de hash à créer
    /// </summary>
    internal enum HashType
    {
        MD5,
        SHA1,
        SHA512
    }

    /// <summary>
    /// Classe utilisée pour créer le hash sums des fichiers
    /// </summary>
    internal class Hasher
    {
        /// <summary>
        /// Génère le hash sums d'un fichier
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="algo"></param>
        /// <returns>Le hash du fichier</returns>
        internal static string HashFile(string filePath, HashType algo)
        {
            switch (algo)
            {
                case HashType.MD5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA1:
                    return MakeHashString(SHA1.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA512:
                    return MakeHashString(SHA512.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                default:
                    return "";
            }
        }

        /// <summary>
        /// Convertis les byte[] en string
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>Le hash en string</returns>
        private static string MakeHashString(byte[] hash)
        {
            var stringBuilder = new StringBuilder(hash.Length * 2);

            foreach (var l_Byte in hash)
            {
                stringBuilder.Append(l_Byte.ToString("X2").ToLower());
            }

            return stringBuilder.ToString();
        }
    }
}
