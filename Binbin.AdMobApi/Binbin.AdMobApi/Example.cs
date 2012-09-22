using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binbin.AdMobApi
{
    public static class Example
    {
        public static void Main()
        {


            // Step 1: Fill in this information below
            var client_key = "Your AdMob client key";
            var email = "Your AdMob login email";
            var password = "Your AdMob login password";
            var site_id_array = "Site id 1";
//var site_id_array[] = 'Site id 2';
// Step 2: Execute the php file! 

            try
            {
                var api = new AdMobApiClient(client_key);
                api.login(email, password);

                var start_date = gmdate("Y-m-d", time() - 7*24*60*60); // 7 days ago
                var end_date = gmdate("Y-m-d"); // today

                var @params = new Dictionary<string, string>
                                  {
                                      {"site_id", site_id_array},
                                      {"start_date", start_date},
                                      {"end_date", end_date},
                                      {"time_dimension", AdMobApiClient.DIMENSION_DAY},
                                      {"object_dimension", AdMobApiClient.OBJECT_SITE}
                                  };


                var data = api.getData("site", "stats", @params, false);

                foreach (var row in data)
                {
                    Console.Write("Site {" + row["site_id"] + "} had \${" + row["revenue"] + "} of revenue for {" +
                                  row["date"] + "}\n");
                }
            }
            catch (AdMobApiException e)
            {
                Console.Write(e.Message);
            }

        }

        private static string gmdate(string yMD)
        {
            throw new NotImplementedException();
        }

        private static int time()
        {
            throw new NotImplementedException();
        }

        private static string gmdate(string yMD, int i)
        {
            throw new NotImplementedException();
        }
    }
}