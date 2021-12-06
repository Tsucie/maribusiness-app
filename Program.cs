﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace maribusiness_app
{
    class Program
    {
        const string apiKey = "LHnJg8Z8wdiyboSs38cXXz7w9nbgWCx2";
        const string uriPath = "http://630317bb-4577-4216-82a1-5e4fe48d92db.southeastasia.azurecontainer.io/score";

        static void Main(string[] args)
        {
            Console.WriteLine("Price Prediction Test Program\n");
            InvokeRequestPricePredictionService().Wait();
        }

        static async Task InvokeRequestPricePredictionService()
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => {
                    return true;
                }
            };
            using (var client = new HttpClient(handler))
            {
                // Request data goes here
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>()
                    {
                        {
                            "WebServiceInput0",
                            new List<Dictionary<string, string>>()
                            {
                                new Dictionary<string, string>()
                                {
                                    {
                                        "id", "2525363215"
                                    },
                                    {
                                        "date", "20201013T000000"
                                    },
                                    {
                                        "price", "356300"
                                    },
                                    {
                                        "bedrooms", "3"
                                    },
                                    {
                                        "bathrooms", "1"
                                    },
                                    {
                                        "sqft_living", "1180"
                                    },
                                    {
                                        "sqft_lot", "5650"
                                    },
                                    {
                                        "floors", "1"
                                    },
                                    {
                                        "waterfront", "0"
                                    },
                                    {
                                        "view", "0"
                                    },
                                    {
                                        "condition", "3"
                                    },
                                    {
                                        "grade", "7"
                                    },
                                    {
                                        "sqft_above", "1180"
                                    },
                                    {
                                        "sqft_basement", "0"
                                    },
                                    {
                                        "yr_built", "1955"
                                    },
                                    {
                                        "yr_renovated", "0"
                                    },
                                    {
                                        "zipcode", "98178"
                                    },
                                    {
                                        "lat", "475.11199999999997"
                                    },
                                    {
                                        "long", "-122.257"
                                    },
                                    {
                                        "sqft_living15", "1340"
                                    },
                                    {
                                        "sqft_lot15", "5650"
                                    },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>() {}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",apiKey);
                client.BaseAddress = new Uri(uriPath);

                var requestString = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(requestString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}
