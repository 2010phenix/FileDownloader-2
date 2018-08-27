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
        private const string API_KEY = "378cd69674b87658c6bec05360952b39b21037befe907f613acb34e8af2e6d28";

        static void Main(string[] args)
        {
            Bootstrap boot = new Bootstrap(API_KEY);
            boot.Start();
            
        }  
    }
}
