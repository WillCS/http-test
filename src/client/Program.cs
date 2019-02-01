using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InventoryTest {
    class Program {
        static HttpClient client = new HttpClient();
        static Dictionary<string, Func<string[], Task<string[]>>> commands = new Dictionary<string, Func<string[], Task<string[]>>>();

        static void Main(string[] args) {
            client.BaseAddress = new Uri("http://localhost:3080");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            commands.Add("add", async (commandArgs) => {
                string content = JsonConvert.SerializeObject(commandArgs.Skip(1));
                HttpResponseMessage response = await client.PostAsync("/add/",
                        new StringContent(content, Encoding.UTF8, "application/json"));

                string responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeAnonymousType(responseText, new string[] {});
            });

            commands.Add("get", async (commandArgs) => {
                HttpResponseMessage response = await client.GetAsync("/get/");
                
                string responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeAnonymousType(responseText, new string[] {});
            });

            while(true) {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ");
                
                if((tokens.Length == 1 && tokens[0].Equals("")) || tokens[0].Equals("exit")) {
                    Console.WriteLine("Exiting...");
                    break;
                }
                
                if(commands.ContainsKey(tokens[0])) {
                    Task<string[]> result = commands[tokens[0]](tokens);
                    result.Wait();
                    foreach(string line in result.Result) {
                        Console.WriteLine(line);
                    }
                } else {
                    Console.WriteLine($"Invalid command {tokens[0]}.");
                }
                Console.WriteLine();
            }
        }
    }
}
