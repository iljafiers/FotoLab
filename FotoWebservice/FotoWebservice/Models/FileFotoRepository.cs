using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class FileFotoRepository
    {
        // http://www.codeguru.com/csharp/.net/returning-images-from-asp.net-web-api.htm
        // http://jamessdixon.wordpress.com/2013/10/01/handling-images-in-webapi/

        private string basePath;

        public FileFotoRepository()
        {
            FotoWebservice.Lib.FotoApiSection config = (FotoWebservice.Lib.FotoApiSection)System.Configuration.ConfigurationManager.GetSection("fotoApiGroup/fotoApi");
            basePath = config.BaseFotoPath.Name;
        }

        public void Add(string fotoserieKey, int id, Byte[] fotoByteArray)
        {
            string fotoseriePath = Path.Combine(this.basePath,fotoserieKey);
            Uri uriFotoseriePath = new Uri(fotoseriePath);
            if (!uriFotoseriePath.IsWellFormedOriginalString())
            {
                throw new FileNotFoundException();
            }

            if (!Directory.Exists(fotoseriePath))
            {
                Directory.CreateDirectory(fotoseriePath);
            }


        }

        public void Remove(string fotoserieKey, int id)
        {

        }
    }
}