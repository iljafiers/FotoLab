using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            FotoWebservice.Lib.FotoApiSection config = (FotoWebservice.Lib.FotoApiSection)System.Configuration.ConfigurationManager.GetSection("fotoApiGroup/fotoApi");
            basePath = HttpContext.Current.Server.MapPath(config.BasePath.Name);
            tempPath = HttpContext.Current.Server.MapPath(config.TempPath.Name);
        }

        public string TempPath { 
            get { return this.tempPath; }
        }

        public string Add(string tempFile, int fotoserieId, int id, string extension)
        {
            string permPath = Path.Combine(FotoserieDirectoryExists(fotoserieId.ToString()), id.ToString(), extension);

            string otherPath = FileExists(permPath);
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
            Uri uriFotoseriePath = new Uri(fotoseriePath);
            if (!uriFotoseriePath.IsWellFormedOriginalString())
            {
                throw new FileNotFoundException();
            }

            if (!Directory.Exists(fotoseriePath))
            {
                Directory.CreateDirectory(fotoseriePath);
            }

            return fotoseriePath;
        }

        private string FileExists(string fullFilePath)
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
    }
}