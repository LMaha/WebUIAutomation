using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Utilities
{
    public class PDFExtractor
    {
        public static string ExtractTextFromPDF(string pdfFileName)
        {
            StringBuilder result = new StringBuilder();
            // Create a reader for the given PDF file
            using (PdfReader reader = new PdfReader(pdfFileName))
            {
                // Read pages
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    SimpleTextExtractionStrategy strategy =
                        new SimpleTextExtractionStrategy();
                    string pageText =
                        PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    result.Append(pageText);
                }
            }
            return result.ToString();
        }

		public static bool WaitForFileDownloadKnownFileName(string filepath)
		{
			int maxWait = 50;
			int counter = 0;
			bool downloadComplete = true;
			while (!File.Exists(filepath) && counter <= maxWait)
			{
				Thread.Sleep(1000);
				counter++;
			}
		
                if (!File.Exists(filepath))
                {
                    downloadComplete = false;
                }
                else
                {
                    downloadComplete = true;
                }
			
			return downloadComplete;
		}

		public static FileInfo WaitForFileDownloadUnknownFileName(string downloadPath, FileInfo currentLatestFile)
		{
			int maxWait = 30;
			int counter = 0;
			var downloadDir = new DirectoryInfo(downloadPath);
			var newFile = downloadDir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
			while (currentLatestFile.FullName == newFile.FullName && counter < maxWait)
			{
				downloadDir = new DirectoryInfo(downloadPath);
				newFile = downloadDir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
				Thread.Sleep(1000);
				counter++;
			}

			return newFile;
		}
    }
}
