using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Foto
    {
        private int id;
        private string path;
        private string url;

        public int Id { get { return id; } set { id = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string Url { get { return url; } set { url = value; } }
    }
}