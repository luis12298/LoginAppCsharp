using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class Form1 : Form
    {
        private HttpListener httpListener;

        public Form1()
        {
            InitializeComponent();
            //Hay que agregar uiAutomationCliente y uiAutomationTypes en reference
            //Instalar el nuget Newtonsoft.Json
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string clientId = "xxxxxxxxxxxx";
            string clientSecret = "xxxxxxxx";
            string redirectURI = "http://localhost:8080/";

            StartHttpListener();

            // Obtener la URL de autenticación y abrir el navegador
            string authUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={clientId}&redirect_uri={redirectURI}&scope=https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile&response_type=code";
            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            // Esperar la respuesta de autenticación
            string authCode = WaitForAuthCode();
            if (string.IsNullOrEmpty(authCode))
            {
                MessageBox.Show("No se pudo obtener el código de aprobación.");
                return;
            }

            // Intercambiar el código de aprobación por un token de acceso
            string accessToken = ExchangeToken(authCode, clientId, clientSecret, redirectURI);

            // Obtener el perfil del usuario
            var profile = GetUserProfile(accessToken);

            // Mostrar el perfil en un MessageBox
            MessageBox.Show($"Nombre: {profile["name"]}\nEmail: {profile["email"]}");

            StopHttpListener();
        }

        private void StartHttpListener()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();
        }

        private void StopHttpListener()
        {
            httpListener.Stop();
        }

        private string WaitForAuthCode()
        {
            HttpListenerContext context = httpListener.GetContext();
            string code = context.Request.QueryString["code"];
            using (StreamWriter writer = new StreamWriter(context.Response.OutputStream))
            {
                writer.WriteLine("<html><body>Autenticación completada. Puedes cerrar esta ventana.</body></html>");
            }
            return code;
        }

        private string ExchangeToken(string authCode, string clientId, string clientSecret, string redirectURI)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");

            string postData = $"code={authCode}&client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectURI}&grant_type=authorization_code";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            JObject tokenResponse = JObject.Parse(responseString);
            return tokenResponse["access_token"].ToString();
        }

        private JObject GetUserProfile(string accessToken)
        {
            string url = $"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}";
            WebClient wc = new WebClient
            {
                Headers = { [HttpRequestHeader.AcceptCharset] = "utf-8", [HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; …) Gecko/20100101 Firefox/55.0" },
                Encoding = Encoding.UTF8
            };

            string jsonProfile = wc.DownloadString(url);
            return JObject.Parse(jsonProfile);
        }
    }
}
