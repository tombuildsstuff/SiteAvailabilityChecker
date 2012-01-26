namespace AppMonitor
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                Environment.Exit(-1);

            var server = args[0];
            var port = args.Length >= 2 ? int.Parse(args[1]) : 80;
            var urlStem = args.Length >= 3 ? args[2] : "";

            try
            {
                var successful = UrlIsAvailable(server, port, urlStem);
                Environment.Exit(successful ? 0 : -1);
            }
            catch (WebException)
            {
                Environment.Exit(-1);
            }
        }

        private static bool UrlIsAvailable(string server, int port, string uristem)
        {
            var request = WebRequest.Create(string.Format("http://{0}:{1}/{2}", server, port, uristem));
            var response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}