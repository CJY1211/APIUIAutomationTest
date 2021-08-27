using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Net.Http;

namespace CliTool
{
    class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            string userName = null; //torvalds
            Console.Write("Enter User Name: ");
            userName = Console.ReadLine();
            var github = new GitHubClient(new Octokit.ProductHeaderValue("MyAmazingApp"));
            var user = await github.User.Get(userName);
            Console.WriteLine("Username = " + userName);
            Console.WriteLine("Name = " + user.Name);           
            Console.WriteLine("Created on = " + user.CreatedAt);            
                       
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"                   
            HttpResponseMessage response = client.GetAsync("https://api.github.com/users/" + userName + "/repos").Result;    
                          
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();                     
                JArray jsonArray = JArray.Parse(responseBody);
                dynamic data = JObject.Parse(jsonArray[0].ToString());          
         
                for(int j =0; j < jsonArray.Count; j++)
                {
                    dynamic r1 = JObject.Parse(jsonArray[j].ToString());
                    var name = r1["name"];
                    Console.WriteLine("Repository " + (j+1) + "= " + name);
                    var stars = r1["stargazers_count"];
                    Console.WriteLine("Stars = " + stars);
                }               
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }        
        }
    }
}
