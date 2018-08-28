using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;



namespace FileDownloader
{
    class FileDownloaderMainClass
    {
        // Use VirusTotal ApiKey
        private const string API_KEY = "";

        static void Main(string[] args)
        {
            Bootstrap boot = new Bootstrap(API_KEY);
            boot.Start();
            
        }  
    }
}
