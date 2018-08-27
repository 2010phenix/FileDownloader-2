using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VirusTotalUrlClient;

namespace FileDownloader
{
    class Bootstrap
    {
        private string downloadsPath;
        private string downloadedPathDisplay;
        private string downloadedPath;
        private string downloadFileName;
        private string virusTotalApiKey;
        private string openFile;
        private string inputFileURL;
        private VirusTotalUrl virusCheck;
        private Uri uri;

        public Bootstrap(string apiKey)
        {
            virusTotalApiKey = apiKey;
        }

        public void Start()
        {
            Console.WriteLine("Type the link of the file you want to download! If you want to close the program type \"Quit\" , \"Exit\" or \"Close\"");
            inputFileURL = Convert.ToString(Console.ReadLine()).ToLower();

            if (inputFileURL == "exit" || inputFileURL == "quit" || inputFileURL == "close")
            {
                Environment.Exit(0);
            }

            FileDownloader(inputFileURL, inputFileURL);
        }

        private void FileDownloader(string fileURL, string inputFileURL)
        {
            try
            {
                uri = new Uri(inputFileURL);

                downloadFileName = Path.GetFileName(uri.LocalPath);
                downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
                downloadedPathDisplay = $@"{downloadsPath}\{downloadFileName}";
                downloadedPath = $@"{downloadsPath}\ {downloadFileName}";

                WebClient client = new WebClient();
                client.DownloadFile(fileURL, downloadsPath + $@"\ { downloadFileName } ");

                Console.Clear();

                VirusCheck(inputFileURL);

                Console.WriteLine($"Link: { inputFileURL }");
                Console.WriteLine($"The file name is: { downloadFileName } ");
                Console.WriteLine("Download Complete");
                Console.WriteLine($"Path: { downloadedPathDisplay }");

                Console.WriteLine("Type \"Open\" to open the downloaded file or Press Enter To Continue . . .");

                openFile = Convert.ToString(Console.ReadLine()).ToLower();

                if (openFile == "open")
                {
                    Opener();
                }
                else
                {
                    Reset();
                }
            }
            catch (InvalidProgramException)
            {
                Console.WriteLine("Unsuccessful scanning! Beware, this file may contain malicious software!");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("You typed a wrong URL. Make sure you copied the URL correctly!\nPress Any Key To Continue . . .");
                Console.ReadKey();
            }
            finally
            {
                Reset();
            }
        }

        private void Opener()
        {
            if (!File.Exists(@downloadedPath))
            {
                Console.Clear();
                Console.WriteLine("The downloaded file is not found!\nPress Any Key To Continue . . .");
                Console.ReadKey();
            }

            Process.Start(@downloadedPath);
            Console.WriteLine("--------------------------------------------------\nFile opened!\nPress Enter To Continue . . .");
            Console.ReadKey();
        }

        private void Reset()
        {
            Console.Clear();
            Start();
        }

        private void VirusCheck(string urlToCheck)
        {
            virusCheck = new VirusTotalUrl(virusTotalApiKey, urlToCheck);

            if (virusCheck.StartScan())
            {
                Console.WriteLine("Scanning: The file is most likely safe!");
            }
            else
            {
                Console.WriteLine("Beware, this file may contain malicious software!\nPress Any Key . . .");
                Console.ReadKey();
                Reset();
            }
        }
    }
}

