using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BrokenLinks
{
    class Program
    {
        static void CheckLinks(ref IWebDriver webDriver, ref string href, ref int validLinksNumber, ref int invalidLinksNumber, HashSet<string> urlSet, StreamWriter validLinks, StreamWriter invalidLinks)
        {
            webDriver.Navigate().GoToUrl(href);
            HttpWebRequest req = null;
            IList<IWebElement> urls = webDriver.FindElements(By.TagName("a"));

            foreach (var url in urls)
            {

                WebDriverWait waitForElement = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
                //var newLinks = webDriver.FindElements(By.TagName("a"))[i];
                waitForElement.Until(drv => drv.FindElements(By.TagName("a")));

                bool staleElement = true;
                while (staleElement)
                {
                    try
                    {
                        href = url.GetAttribute("href");
                        if (!String.IsNullOrEmpty(href) && href.StartsWith("http://91.210.252.240") && !urlSet.Contains(href))
                        {
                            req = (HttpWebRequest)WebRequest.Create(href);
                            var response = (HttpWebResponse)req.GetResponse();
                            System.Console.WriteLine($"URL: {href} status is :{response.StatusCode}");
                            urlSet.Add(href);
                            validLinks.WriteLine(href);
                            validLinks.WriteLine(response.StatusCode);
                            validLinksNumber++;
                            Actions actions = new Actions(webDriver);
                            //actions.MoveToElement(url).Click();
                            CheckLinks(ref webDriver, ref href, ref validLinksNumber, ref invalidLinksNumber, urlSet, validLinks, invalidLinks);
                            webDriver.Navigate().Back();
                        }
                        staleElement = false;
                    }
                    catch (WebException e)
                    {
                        var errorResponse = (HttpWebResponse)e.Response;
                        System.Console.WriteLine($"URL: {href} status is :{errorResponse.StatusCode}");
                        urlSet.Add(href);
                        invalidLinks.WriteLine(href);
                        invalidLinks.WriteLine(errorResponse.StatusCode);
                        invalidLinksNumber++;
                        staleElement = false;
                    }
                    catch (StaleElementReferenceException e)
                    {
                        
                        if (String.IsNullOrEmpty(href) || !href.StartsWith("http://91.210.252.240") || urlSet.Contains(href))
                        {
                            staleElement = false;
                            continue;
                        }
                        else
                        {
                            staleElement = true;
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--ignore-certificate-errors", "--ignore-ssl-errors");
            IWebDriver webDriver = new ChromeDriver(@"C:\Users\Irina\source\repos\chromedriver", options);

            StreamWriter validLinks = new StreamWriter("../../valid_links.txt");
            StreamWriter invalidLinks = new StreamWriter("../../invalid_links.txt");

            int validLinksNumber = 0;
            int invalidLinksNumber = 0;

            HashSet<string> urlSet = new HashSet<string>();
            string url = "http://91.210.252.240/broken-links/";

            CheckLinks(ref webDriver, ref url, ref validLinksNumber, ref invalidLinksNumber, urlSet, validLinks, invalidLinks);


            validLinks.WriteLine($"Всего ссылок: {validLinksNumber}");
            invalidLinks.WriteLine($"Всего ссылок: {invalidLinksNumber}");

            DateTime thisDay = DateTime.Today;
            validLinks.WriteLine($"Дата проверки: {thisDay.ToString()} {DateTime.Now.ToString("h:mm:ss tt")}");
            invalidLinks.WriteLine($"Дата проверки: {thisDay.ToString()} {DateTime.Now.ToString("h:mm:ss tt")}");

            validLinks.Close();
            invalidLinks.Close();
        }
    }
}
