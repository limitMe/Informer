using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonMonitor
{
    public class JsonChecker
    {
        private string url;

        //Improve: Init with a function as a parameter, seeking information with customized method.
        public JsonChecker(String url)
        {
            this.url = url;
        }

        internal ResultModel CheckCurrent(){
            ResultModel result = new ResultModel();
            result.status = false;

            using (var webClient = new System.Net.WebClient())
            {
                Console.WriteLine("Fetching data...");
                var json = webClient.DownloadString(this.url);

                JObject parsed = JObject.Parse(json);
                if(parsed["head"]["status"].ToString() == "200"){
                    Console.WriteLine("Obtained current data. Now Analysing...");

                    var stores =
                        from s in parsed["body"]["stores"]
                        select new
                        {
                            storeName = (string)s["storeName"],
                            avaliable = (bool)s["partsAvailability"]["MQA52X/A"]["storeSelectionEnabled"],
                            summary = (string)s["partsAvailability"]["MQA52X/A"]["storePickupQuote"]
                        };

                    var avaliableCount = 0;
                    foreach(var store in stores){
                        if(store.avaliable){
                            Console.WriteLine("Found iPhone X avaliable in " + store.storeName);
                            result.status = true;
                            result.body = result.body + "\n\n" + store.summary;
                            avaliableCount++;
                        } else {
                            Console.WriteLine("Currently not iPhone X avaliable in " + store.storeName);
                        }
                    }
                    
                    Console.WriteLine("Finished iPhone X search, found in " + avaliableCount.ToString() + " stores near Sydney");
                    result.title = "Hey, currently there is " + avaliableCount.ToString() + " stores with iPhone X in Sydney";
                } else {
                    Console.WriteLine("Server returned unacceptable data, session aborted.");
                }

            }

            return result;
        }
    }
}
