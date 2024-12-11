using System.Web.Http;

namespace CreditCardValidatorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}