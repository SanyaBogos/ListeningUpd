using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Services.Contracts;
using System.Collections.Generic;
using Listening.Core.ViewModels.DebianFAI;

namespace Listening.Infrastructure.Services
{
    public class WebPageService : IWebPageService
    {
        public string GetHtmlByUrl(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new WebPageException("Didn't receive web page correctly");

            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;

            if (String.IsNullOrWhiteSpace(response.CharacterSet))
                readStream = new StreamReader(receiveStream);
            else
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            string data = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return data;
        }

        public Dictionary<ArchitectureType, string> GetArhitecturesDictionary(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var results = doc.DocumentNode.SelectNodes("//ul//a[starts-with(@href, 'https')][substring(@href, string-length(@href) - string-length('.iso') +1) = '.iso']");

            if (results == null || results.Count == 0)
                throw new WebPageException("No links for download found");

            var arhitectureDictionaries = new Dictionary<ArchitectureType, string>();

            foreach (var result in results)
                arhitectureDictionaries.Add((ArchitectureType)Enum.Parse(typeof(ArchitectureType), result.InnerText),
                    result.Attributes["href"].Value);

            return arhitectureDictionaries;
        }

        public string DownloadFile(string link, string pathToFolder)
        {
            var fileName = link.Substring(link.LastIndexOf('/') + 1);
            var fullFilePath = $"{pathToFolder}{fileName}";

            var net = new WebClient();
            net.DownloadFile(new Uri(link), fullFilePath);
            return fullFilePath;
        }
    }
}