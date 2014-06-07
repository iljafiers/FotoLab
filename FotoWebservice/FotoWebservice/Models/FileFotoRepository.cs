using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FotoWebservice.Models
{
    public class FileFotoRepository
    {
        // http://www.codeguru.com/csharp/.net/returning-images-from-asp.net-web-api.htm
        // http://jamessdixon.wordpress.com/2013/10/01/handling-images-in-webapi/
        // http://www.asp.net/web-api/overview/working-with-http/sending-html-form-data,-part-2

        private string basePath;
        private string tempPath;
        private string[] acceptList = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".tif", ".bmp" };

        public FileFotoRepository()
        {
            /*FotoWebservice.Lib.FotoApiSection config = (FotoWebservice.Lib.FotoApiSection)System.Configuration.ConfigurationManager.GetSection("fotoApiGroup/fotoApi");
            basePath = HttpContext.Current.Server.MapPath(config.BasePath.Name);
            tempPath = HttpContext.Current.Server.MapPath(config.TempPath.Name);*/

            basePath = HttpContext.Current.Server.MapPath("~/Fotos/");
            tempPath = HttpContext.Current.Server.MapPath("~/App_Data/Upload_Temp");
        }

        public string TempPath { 
            get { return this.tempPath; }
        }

        public string Add(string tempFile, int fotoserieId, int id, string extension)
        {
            string filename = id.ToString() + extension;
            string permPath = Path.Combine(FileFotoRepository.RemoveBadPathChars(FotoserieDirectoryExists(fotoserieId.ToString())), FileFotoRepository.RemoveBadPathChars(filename));

            string otherPath = ImageTypeExists(permPath);
            if (otherPath != string.Empty)
            {
                File.Delete(tempFile);
                return otherPath;
            }

            File.Move(tempFile, permPath);

            return permPath;
        }

        public void Get(string fotoserieKey, int id, Byte[] fotoByteArray)
        {
            string fotoseriePath = FotoserieDirectoryExists(fotoserieKey);


        }

        public void Remove(string fotoserieKey, int id)
        {
            string fotoseriePath = FotoserieDirectoryExists(fotoserieKey);
        }

        private string FotoserieDirectoryExists(string fotoserieKey)
        {
            string fotoseriePath = Path.Combine(this.basePath, fotoserieKey);

            if (!Directory.Exists(fotoseriePath))
            {
                Directory.CreateDirectory(fotoseriePath);
            }

            return fotoseriePath;
        }

        private string ImageTypeExists(string fullFilePath)
        {
            string extension = Path.GetExtension(fullFilePath);

            foreach (string ext in this.acceptList)
            {
		        string possiblePath = Path.Combine(Path.GetDirectoryName(fullFilePath), Path.GetFileNameWithoutExtension(fullFilePath), ext);

                if (File.Exists(possiblePath))
                {
                    return possiblePath;
                }
            }

            return "";
        }

        public static string RemoveBadPathChars(string path) {
            string[] badChars = new string[] { "\"", "<", ">", "|", "\b", "\0", "\t" };

            foreach (string badChr in badChars)
	        {
		        path = path.Replace(badChr, "");
	        }

            return path;
        }

        public static string CalculateMD5Hash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return FileFotoRepository.CalculateMD5Hash(inputBytes);
        }

        public static string CalculateMD5Hash(byte[] input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(input);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


    }
}