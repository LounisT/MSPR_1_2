using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MSPR1WebApp.Controllers
{
    public class ClientController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44340/";
       
        public async Task<ActionResult> Index()
        {
            List<Client> ClientInfo = new List<Client>();
            ViewBag.CodeClient = getCodeClient();
            if (ViewBag.CodeClient != null) { 
                
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllClients using HttpClient
                    HttpResponseMessage Res = await client.GetAsync("api/Client/"+ViewBag.CodeClient.Content);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        if (Res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' }) == "Session Invalide")
                        {
                            return View("Index");
                        }
                        else {
                            //Storing the response details recieved from web api
                            var ClientResponse = Res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] {'"'});
                            //Deserializing the response recieved from web api and storing into the Client list
                            ClientInfo = JsonConvert.DeserializeObject<List<Client>>(ClientResponse.Replace("rn", "")); 
                        }
                        

                    }
                
                    //returning the Client list to view
                    ViewBag.RefClient = ClientInfo[0].RefClient;
                    ViewBag.CodeClient = ClientInfo[0].CodeClient; 
                    ViewBag.Nom = ClientInfo[0].Nom; 
                    ViewBag.Adresse1 = ClientInfo[0].Adresse1; 
                    ViewBag.Adresse2 = ClientInfo[0].Adresse2; 
                    ViewBag.Ville = ClientInfo[0].Ville; 
                    ViewBag.CP = ClientInfo[0].CP; 
                    ViewBag.Pays = ClientInfo[0].Pays;
                    ViewBag.Web = ClientInfo[0].Web;
                    ViewBag.Tel = ClientInfo[0].Tel; 
                    ViewBag.Email = ClientInfo[0].Email; 
                    ViewBag.CompteComptable = ClientInfo[0].CompteComptable; 
                    return View("Client");
                }
            }
            return View("Client");
        }

        [HttpPost]
        public ActionResult getCodeClient()
        {
            string codeClient=null;
            try
            {
               codeClient = Request.Form["codeClient"].ToString().ToUpper();
                
            }
            catch (Exception e)
            {
                return null;
            }
            return Content(codeClient);

        }

        [HttpPost]
        public async Task<ActionResult> AllerSurLaPageMaj()
        {
            return View("AddClient");
        }
        public async Task<ActionResult> MajClient()
        {
           
                if (ViewBag.CodeClient != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        //Define request data format
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Sending request to find web api REST service resource GetAllClients using HttpClient
                        HttpResponseMessage Res = await client.GetAsync("api/Client/Update/" + getInfoNewClient());
                    }
                }
                return View("Client");

        }

        [HttpPost]
        public ActionResult getInfoNewClient()
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

            string xml = "";
            try
            {
                RefClient = Request.Form["codeClient"].ToString().ToUpper();
                CodeClient = Request.Form["codeClient"].ToString().ToUpper();
                Nom = Request.Form["codeClient"].ToString().ToUpper();
                Adresse1 = Request.Form["codeClient"].ToString().ToUpper();
                Adresse2 = Request.Form["codeClient"].ToString().ToUpper();
                Ville = Request.Form["codeClient"].ToString().ToUpper();
                CP = Request.Form["codeClient"].ToString().ToUpper();
                Pays = Request.Form["codeClient"].ToString().ToUpper();
                Web = Request.Form["codeClient"].ToString().ToUpper();
                Tel = Request.Form["codeClient"].ToString().ToUpper();
                Email = Request.Form["codeClient"].ToString().ToUpper();
                CompteComptable = Request.Form["codeClient"].ToString().ToUpper();

                xml = "<FB_CLIENTS>" +
                        "< FICHE >" +
                            "< REFCLIENT >< ![CDATA[ " + RefClient + "]] > </ REFCLIENT >" +
                            "< CODECLIENT >< ![CDATA[ " + CodeClient + "]] > </ CODECLIENT >" +
                            "< NOM >< ![CDATA[ " + Nom  + "]] > </ NOM >" +
                            "< ADRESSE1 >< ![CDATA[ " + Adresse1+ "]] > </ ADRESSE1 >" +
                            "< ADRESSE2 >< ![CDATA[ " + Adresse2  + "]] > </ ADRESSE2 >" +
                            "< VILLE >< ![CDATA[ " + Ville + "]] > </ VILLE >" +
                            "< CP >< ![CDATA[ " + CP  + "]] > </ CP >" +
                            "< PAYS >< ![CDATA[ " + Pays+ "]] > </ PAYS >" +
                             "< EMAIL >< ![CDATA[ " +  Web + "]] > </ EMAIL >" +
                            "< TEL >< ![CDATA[ " + Tel+ "]] > </ TEL >" +
                            "< WEB >< ![CDATA[ " + Web + "]] > </ WEB >" +
                            "< COMP_COMPTABLE >< ![CDATA[ " + CompteComptable + "]] > </ COMP_COMPTABLE >" +
                        "</ FICHE >" +
                      "</ FB_CLIENTS > ";

            }
            catch (Exception e)
            {
                return View(e.ToString(), "AddClient");
            }


            return Content(xml);

        }

    }
}
