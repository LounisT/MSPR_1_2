using Microsoft.AspNetCore.Mvc;
using MSPR1WebApp.Models;
using System.Diagnostics;
using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;


namespace MSPR1WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string CodeAbonne;
        private string Identifiant;
        private string MotDePasse;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            string Baseurl = "https://localhost:44340/";

            List<Client> ClientInfo = new List<Client>();

            
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllClients using HttpClient
                    HttpResponseMessage res = new HttpResponseMessage();
                    try
                    {
                        res = await client.GetAsync("api/Authentification/" + getCodeAbonne() + "/" + getCodeIdentifiant() + "/" + getMotDePasse()) ;
                        
                    } catch
                    {
                        return View("Index");
                    }
                    //Checking the response is successful or not which is sent using HttpClient
                    if (res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' }) == "Session Invalide")
                    {
                        return View("Index");
                    }
                    else
                    {
                        //Storing the response details recieved from web api
                        var ClientResponse = res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                        //Deserializing the response recieved from web api and storing into the Client list
                        ClientInfo = JsonConvert.DeserializeObject<List<Client>>(ClientResponse.Replace("rn", ""));
                    }


                    return View("Index");
                }
            return View("Index");
        }

        [HttpPost]
        public ActionResult getCodeAbonne()
        {
            CodeAbonne = Request.Form["CodeAbonne"];

            return Content(CodeAbonne);
        }

        [HttpPost]
        public ActionResult getCodeIdentifiant()
        {
            Identifiant = Request.Form["Identifiant"];
            return Content(Identifiant);
        }

        [HttpPost]
        public ActionResult getMotDePasse()
        {
            MotDePasse = Request.Form["MotDePasse"];
            return Content(MotDePasse);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
