using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace MSPR1WebApp.Controllers
{
    public class AddClientController : Controller
    {
        string xml = "";

        [HttpPost]
        public async Task<ActionResult> AllerSurLaPageMaj()
        {
            return View("AddClient");
        }

        string Baseurl = "https://localhost:44340/";
        public async Task<ActionResult> Index()
        {
            if (getInfoNewClient().RefClient != null)
            {

                using (var client = new HttpClient())
                {
                    Client leClient = new Client();
                    leClient = new Client
                    {
                        RefClient = Request.Form["RefClient"].ToString().ToUpper(),
                        CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                        Nom = Request.Form["Nom"].ToString().ToUpper(),
                        Adresse1 = Request.Form["Adresse1"].ToString().ToUpper(),
                        Adresse2 = Request.Form["Adresse2"].ToString().ToUpper(),
                        Ville = Request.Form["Ville"].ToString().ToUpper(),
                        CP = Request.Form["CP"].ToString().ToUpper(),
                        Pays = Request.Form["Pays"].ToString().ToUpper(),
                        Email = Request.Form["Email"].ToString().ToUpper(),
                        Tel = Request.Form["Tel"].ToString().ToUpper(),
                        Web = Request.Form["Web"].ToString().ToUpper(),
                        CompteComptable = Request.Form["CompteComptable"].ToString().ToUpper()
                    };


                    string json = JsonConvert.SerializeObject(leClient, Formatting.Indented);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync(Baseurl + "api/Client/Add", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return View("Client");
                    }
                }


                return View("AddClient");
            }
            return View("AddClient");
        }

        [HttpPost]
        public Client getInfoNewClient()
        {
            string RefClient = null;
            string CodeClient = null;
            string Nom = null;
            string Adresse1 = null;
            string Adresse2 = null;
            string Ville = null;
            string CP = null;
            string Pays = null;
            string Web = null;
            string Tel = null;
            string Email = null;
            string CompteComptable = null;

           
                Client client = new Client();

            try
            {
                Request.Form["RefClient"].ToString().ToUpper();
            } catch (Exception ex)
            {
                return client;
            }
                client = new Client
                {
                    RefClient = Request.Form["RefClient"].ToString().ToUpper(),
                    CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                    Nom = Request.Form["Nom"].ToString().ToUpper(),
                    Adresse1 = Request.Form["Adresse1"].ToString().ToUpper(),
                    Adresse2 = Request.Form["Adresse2"].ToString().ToUpper(),
                    Ville = Request.Form["Ville"].ToString().ToUpper(),
                    CP = Request.Form["CP"].ToString().ToUpper(),
                    Pays = Request.Form["Pays"].ToString().ToUpper(),
                    Email = Request.Form["Email"].ToString().ToUpper(),
                    Tel = Request.Form["Tel"].ToString().ToUpper(),
                    Web = Request.Form["Web"].ToString().ToUpper(),
                    CompteComptable = Request.Form["CompteComptable"].ToString().ToUpper()
                };

            return client;

        }

        public async Task<ActionResult> Delete(string CodeClient)
        {
            

            using (var client = new HttpClient())
            {
                Client leClient = new Client();
                leClient = new Client
                {
                    RefClient = "",
                    CodeClient = CodeClient,
                    Nom = "",
                    Adresse1 = "",
                    Adresse2 = "",
                    Ville = "",
                    CP = "",
                    Pays = "",
                    Email ="",
                    Tel = "",
                    Web = "",
                    CompteComptable = ""
                };


                string json = JsonConvert.SerializeObject(leClient, Formatting.Indented);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await client.PostAsync(Baseurl + "api/Client/Delete", data);

                if (response.IsSuccessStatusCode)
                {
                    return View("Client");
                }
            }


            return View("Client");
        }

        public async Task<ActionResult> IndexUpdate(string RefClient, string CodeClient, string Nom, string Adresse1, string Adresse2, string Ville, string CP, string Pays, string Web, string Tel, string Email, string CompteComptable)
        {
            ViewBag.RefClient = RefClient;
            ViewBag.CodeClient = CodeClient;
            ViewBag.Nom = Nom;
            ViewBag.Adresse1 = Adresse1;
            ViewBag.Adresse2 = Adresse2;
            ViewBag.Ville = Ville;
            ViewBag.CP = CP;
            ViewBag.Pays = Pays;
            ViewBag.Web = Web;
            ViewBag.Tel = Tel;
            ViewBag.Email = Email;
            ViewBag.CompteComptable = CompteComptable;
            return View("UpdateClient");
        }

        public async Task<ActionResult> Update()
        {
            if (getInfoNewClient().RefClient != null)
            {

                using (var client = new HttpClient())
                {
                    Client leClient = new Client();
                    leClient = new Client
                    {
                        RefClient = Request.Form["RefClient"].ToString().ToUpper(),
                        CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                        Nom = Request.Form["Nom"].ToString().ToUpper(),
                        Adresse1 = Request.Form["Adresse1"].ToString().ToUpper(),
                        Adresse2 = Request.Form["Adresse2"].ToString().ToUpper(),
                        Ville = Request.Form["Ville"].ToString().ToUpper(),
                        CP = Request.Form["CP"].ToString().ToUpper(),
                        Pays = Request.Form["Pays"].ToString().ToUpper(),
                        Email = Request.Form["Email"].ToString().ToUpper(),
                        Tel = Request.Form["Tel"].ToString().ToUpper(),
                        Web = Request.Form["Web"].ToString().ToUpper(),
                        CompteComptable = Request.Form["CompteComptable"].ToString().ToUpper()
                    };


                    string json = JsonConvert.SerializeObject(leClient, Formatting.Indented);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync(Baseurl + "api/Client/Update", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return View("Client");
                    }
                }


                return View("Client");
            }
            return View("Client");
        }        
    }
}
