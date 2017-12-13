using System;


namespace JsonMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new EmailHelper();
            helper.SendEmail("YAT","Yet another test");
        }
    }
}
