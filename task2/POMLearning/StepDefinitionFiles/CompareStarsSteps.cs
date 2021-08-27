using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace POMLearning.StepDefinitionFiles
{
    [Binding]
    public class CompareStarsSteps : EntryPoint
    {
        gitHubPage gitHub = new gitHubPage(driver);
        string url = "https://github.com/";
        string searchValue = "torvalds";
        string repoName = "linux";

        [When(@"I open GitHub page")]
        public void OpenGitHubPage()
        {
            driver.Url = url;
            driver.Manage().Window.Maximize();
        }

        [When(@"I search the given username")]
        public void SearchTheGivenUsername()
        {
            gitHub.EnterSearchInformation("torvalds/");
            gitHub.EnterSearchInformation(Keys.Enter);
        }

        [When(@"I find any repository belong to that user")]
        public void FindAnyRepository()
        {
            gitHub.ClickOnLinux();
        }

        [Then(@"I verify that the star value is correct by comparing with result returned from API")]
        public void VerifyStarValue()
        {   
            string apiStarCount = RetrieveStarCount(searchValue, repoName);
            string apiStarCountK = (Math.Round(double.Parse(apiStarCount) / 1000)).ToString() + "k";
            Assert.AreEqual(apiStarCountK, gitHub.GetStarsCount());
        }                
        public string RetrieveStarCount(string searchValue, String repoName)
        {            
            Console.WriteLine(searchValue);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"                   
            HttpResponseMessage response = client.GetAsync("https://api.github.com/users/" + searchValue + "/repos").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;                             
                JArray jsonArray = JArray.Parse(responseBody);
                dynamic data = JObject.Parse(jsonArray[0].ToString());

                for (int j = 0; j < jsonArray.Count; j++)
                {
                    dynamic r1 = JObject.Parse(jsonArray[j].ToString());
                    var name = r1["name"];
                    var stars = r1["stargazers_count"];
                  
                    if (r1["name"] == repoName) 
                    {
                       return r1["stargazers_count"];
                    }
                }
                return null;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }
    }
}
