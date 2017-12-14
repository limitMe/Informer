using System;


namespace JsonMonitor
{
    class Program
    {
        static EmailHelper helper;
        static JsonChecker checker;

        static void Main(string[] args)
        {
            helper = new EmailHelper();
            checker = new JsonChecker("https://www.apple.com/au/shop/retail/pickup-message?pl=true&parts.0=MQA52X/A&location=2000");


            while (true)
            {
                var result = checker.CheckCurrent();
                if (result.status == true)
                {
                    helper.SendEmail(result.title,result.body);
                }

                //Set the check interval by seconds
                var queryInterval = 3600;
                Console.WriteLine(DateTime.Now.ToString() + ": Gonna rest for " + queryInterval.ToString() + " seconds.");
                System.Threading.Thread.Sleep(queryInterval * 1000);
            }
        }
    }
}
