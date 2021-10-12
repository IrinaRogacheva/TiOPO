using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BrokenLinks
{
    class Program
    {
        const string START_URL = "http://91.210.252.240/broken-links/";
        const string DOMEN = "http://91.210.252.240";

        static void CheckLinksOnOnePage(IWebDriver webDriver, string tagName, string attributeName, ref int validLinksNumber, ref int invalidLinksNumber, HashSet<string> previousLinks, StreamWriter validLinksFile, StreamWriter invalidLinksFile, List<string> validLinks)
        {
            HttpWebRequest req = null;
            IList<IWebElement> urls = webDriver.FindElements(By.TagName(tagName));
            string href;
            foreach (var url in urls)
            {
                try
                {
                    href = url.GetAttribute(attributeName);
                    if (!String.IsNullOrEmpty(href) && href.Contains('#'))
                    {
                        href = href.Substring(0, href.IndexOf('#'));
                    }
                    if (!String.IsNullOrEmpty(href) && href.StartsWith(DOMEN) && !previousLinks.Contains(href))
                    {
                        try
                        {
                            req = (HttpWebRequest)WebRequest.Create(href);
                            req.AllowAutoRedirect = false;
                            var response = (HttpWebResponse)req.GetResponse();
                            validLinksFile.WriteLine(href);
                            validLinksFile.WriteLine((int)response.StatusCode);
                            validLinksNumber++;
                            if (tagName == "a")
                            {
                                validLinks.Add(href);
                            }
                        }
                        catch (WebException e)
                        {
                            var errorResponse = (HttpWebResponse)e.Response;
                            invalidLinksFile.WriteLine(href);
                            invalidLinksFile.WriteLine((int)errorResponse.StatusCode);
                            invalidLinksNumber++;
                        }
                        previousLinks.Add(href);
                    }
                }
                catch (StaleElementReferenceException)
                {
                }
            }
        }

        static void CheckLinksRecursively(IWebDriver webDriver, string startUrl, ref int validLinksNumber, ref int invalidLinksNumber, HashSet<string> previousLinks, StreamWriter validLinksFile, StreamWriter invalidLinksFile, List<string> validLinks)
        {
            webDriver.Navigate().GoToUrl(startUrl);
            CheckLinksOnOnePage(webDriver, "a", "href", ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);
            CheckLinksOnOnePage(webDriver, "script", "src", ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);
            CheckLinksOnOnePage(webDriver, "link", "href", ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);
            CheckLinksOnOnePage(webDriver, "img", "src", ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);
            validLinks.Remove(startUrl);
            foreach (string validHref in validLinks)
            {
                CheckLinksRecursively(webDriver, validHref, ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);
                validLinks.Remove(validHref);
                if (validLinks.Count() == 0)
                {
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            IWebDriver webDriver = new ChromeDriver(@"C:\Users\Irina\source\repos\TiOPO\BrokenLinks\chromedriver");

            int validLinksNumber = 0;
            int invalidLinksNumber = 0;

            HashSet<string> previousLinks = new HashSet<string>();

            StreamWriter validLinksFile = new StreamWriter("../../valid_links.txt");
            StreamWriter invalidLinksFile = new StreamWriter("../../invalid_links.txt");
           
            List<string> validLinks = new List<string>();

            CheckLinksRecursively(webDriver, START_URL, ref validLinksNumber, ref invalidLinksNumber, previousLinks, validLinksFile, invalidLinksFile, validLinks);

            validLinksFile.WriteLine($"Всего ссылок: {validLinksNumber}");
            invalidLinksFile.WriteLine($"Всего ссылок: {invalidLinksNumber}");

            validLinksFile.WriteLine($"Дата проверки: {DateTime.Now.ToString()}");
            invalidLinksFile.WriteLine($"Дата проверки: {DateTime.Now.ToString()}");

            validLinksFile.Close();
            invalidLinksFile.Close();
        }
    }
}
