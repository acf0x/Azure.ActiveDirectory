using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace EntraID.ConsoleApp1
{
    internal class Program
    {
        static IConfidentialClientApplication app;

        static void Main(string[] args)
        {
            Console.Clear();


            // Iniciar objeto app

            app = ConfidentialClientApplicationBuilder
                .Create("")             // id de aplicación cliente
                .WithClientSecret("")   // clave generada en secretos
                .WithAuthority("")      // tenantid
                .Build();


            // Generar Token

            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };         // urls a las que daría acceso, dentro del array
                                                                                               // .default -> acceso a todo lo que hay detrás de la /
                                                                                               
            AuthenticationResult token = app.AcquireTokenForClient(scopes).ExecuteAsync().Result;

            Console.WriteLine(token.AccessToken);
            Console.ReadKey();


            // Listado de usuarios con HTTP

            Console.Clear();

            var http = new HttpClient();
            string url = "https://graph.microsoft.com/v1.0/users";

            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = http.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string dataJSON = response.Content.ReadAsStringAsync().Result;
                OData data = JsonConvert.DeserializeObject<OData>(dataJSON);
                List<User> usuarios = JsonConvert.DeserializeObject<List<User>>(data.Value.ToString());
                foreach (var usuario in usuarios) Console.WriteLine($"HTTP -> {usuario.DisplayName} - {usuario.UserPrincipalName}");
                Console.WriteLine(Environment.NewLine);
            }
            else Console.WriteLine($"Error {response.StatusCode}");


            // Listado de usuarios con .NET

            ClientCredentialProvider authProvider = new ClientCredentialProvider(app);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var result = graphClient.Users.Request().GetAsync().Result;

            foreach (var user in result) Console.WriteLine($".NET -> {user.DisplayName} - {user.UserPrincipalName}");
            Console.WriteLine(Environment.NewLine);
        }
    }

    public class OData  // clase para deserializar
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        public Object Value { get; set; }
    }
}
