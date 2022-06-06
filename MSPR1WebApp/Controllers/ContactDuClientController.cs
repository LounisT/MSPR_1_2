using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace MSPR1WebApp.Controllers
{
    public class ContactDuClientController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44340/";
        public async Task<ActionResult> Index(string codeClient)
        {
            List<Contact> ContactDuClientInfo = new List<Contact>();
            ViewBag.CodeClient = Content(codeClient);
            if (ViewBag.CodeClient != null)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("api/ContactDuClient/" + ViewBag.CodeClient.Content);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var ClientResponse = Res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                        //Deserializing the response recieved from web api and storing into the Client list
                        ContactDuClientInfo = JsonConvert.DeserializeObject<List<Contact>>(ClientResponse.Replace("rn", ""));

                        ViewBag.All = ContactDuClientInfo;
                        for (int i = 0; i < ContactDuClientInfo.Count; i++)
                        { //returning the Client list to view
                            ViewBag.All[i].RefContact = ContactDuClientInfo[i].RefContact;
                            ViewBag.All[i].CodeClient = ContactDuClientInfo[i].CodeClient;
                            ViewBag.All[i].Nom = ContactDuClientInfo[i].Nom;
                            ViewBag.All[i].Prenom = ContactDuClientInfo[i].Prenom;
                            ViewBag.All[i].Tel = ContactDuClientInfo[i].Tel;
                            ViewBag.All[i].Fonction = ContactDuClientInfo[i].Fonction;
                            ViewBag.All[i].Email = ContactDuClientInfo[i].Email;
                            ViewBag.All[i].Adresse1 = ContactDuClientInfo[i].Adresse1;
                            ViewBag.All[i].Adresse2 = ContactDuClientInfo[i].Adresse2;
                            ViewBag.All[i].CP = ContactDuClientInfo[i].CP;
                            ViewBag.All[i].Ville = ContactDuClientInfo[i].Ville;
                            ViewBag.All[i].Pays = ContactDuClientInfo[i].Pays;
                            ViewBag.All[i].Commentaire = ContactDuClientInfo[i].Commentaire;
                        }
                        ViewBag.NbOfElements = ContactDuClientInfo.Count - 1;
                    }
                    return View("ContactsDuClient");
                }
            }
            return View("ContactsDuClient");
        }
    }
}

