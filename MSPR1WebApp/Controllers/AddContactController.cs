using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace MSPR1WebApp.Controllers
{
    public class AddContactController : Controller
    {
        string xml = "";

        [HttpPost]
        public async Task<ActionResult> AllerSurLaPageMaj()
        {
            return View("AddContact");
        }

        string Baseurl = "https://localhost:44340/";
        public async Task<ActionResult> Index()
        {
            if (getInfoNewContact().RefContact != null)

                if (getInfoNewContact().RefContact != null)
                {
                    using (var client = new HttpClient())
                    {
                        Contact leContact = new Contact();
                        leContact = new Contact
                        {
                            RefContact = Request.Form["RefContact"].ToString().ToUpper(),
                            CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                            Nom = Request.Form["Nom"].ToString().ToUpper(),
                            Prenom = Request.Form["Prenom"].ToString().ToUpper(),
                            Adresse1 = Request.Form["Adresse1"].ToString().ToUpper(),
                            Adresse2 = Request.Form["Adresse2"].ToString().ToUpper(),
                            Ville = Request.Form["Ville"].ToString().ToUpper(),
                            CP = Request.Form["CP"].ToString().ToUpper(),
                            Pays = Request.Form["Pays"].ToString().ToUpper(),
                            Email = Request.Form["Email"].ToString().ToUpper(),
                            Tel = Request.Form["Tel"].ToString().ToUpper(),
                            Fonction = Request.Form["Tel"].ToString().ToUpper()

                        };


                        string json = JsonConvert.SerializeObject(leContact, Formatting.Indented);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");


                        var response = await client.PostAsync(Baseurl + "api/ContactDuClient/Add", data);

                        if (response.IsSuccessStatusCode)
                        {
                            return View("Client");
                        }
                    }


                    return View("AddClient");
                }

            return View("AddContact");
        }

        [HttpPost]
        public Contact getInfoNewContact()
        {
            Contact contact = new Contact();

            try
            {
                Request.Form["RefClient"].ToString().ToUpper();
            }
            catch (Exception ex)
            {
                return contact;
            }
            contact = new Contact
            {
                RefContact = Request.Form["RefContact"].ToString().ToUpper(),
                CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                Nom = Request.Form["Nom"].ToString().ToUpper(),
                Prenom = Request.Form["Prenom"].ToString().ToUpper(),
                Tel = Request.Form["Tel"].ToString().ToUpper(),
                Email = Request.Form["Email"].ToString().ToUpper(),
                Fonction = Request.Form["Fonction"].ToString().ToUpper(),
                Adresse1 = Request.Form["Adresse1"].ToString().ToUpper(),
                Adresse2 = "",
                Ville = Request.Form["Ville"].ToString().ToUpper(),
                CP = Request.Form["CP"].ToString().ToUpper(),
                Pays = Request.Form["Pays"].ToString().ToUpper(),
                Commentaire = ""
            };

            return contact;
            
        }
        public async Task<ActionResult> Delete(string RefContact)
        {


            using (var client = new HttpClient())
            {
                Contact leContact = new Contact();
                leContact = new Contact
                {
                    RefContact = RefContact,
                    CodeClient = "",
                    Nom = "",
                    Adresse1 = "",
                    Adresse2 = "",
                    Ville = "",
                    CP = "",
                    Pays = "",
                    Email = "",
                    Tel = "",
                    
                };


                string json = JsonConvert.SerializeObject(leContact, Formatting.Indented);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await client.PostAsync(Baseurl + "api/ContactDuClient/Delete", data);

                if (response.IsSuccessStatusCode)
                {
                    return View("Client");
                }
            }


            return View("Client");
        }

        public async Task<ActionResult> IndexUpdate(string RefContact, string CodeClient, string Nom, string Prenom,string Tel, string Fonction,   string Email,string Adresse1, string Adresse2, string Ville, string CP, string Pays)
        {
            ViewBag.RefContact = RefContact;
            ViewBag.CodeClient = CodeClient;
            ViewBag.Nom = Nom;
            ViewBag.Prenom = Prenom;
            ViewBag.Tel = Tel;
            ViewBag.Fonction = Fonction;
            ViewBag.Email = Email;
            ViewBag.Adresse1 = Adresse1;
            ViewBag.Adresse2 = Adresse2;
            ViewBag.Ville = Ville;
            ViewBag.CP = CP;
            ViewBag.Pays = Pays;
            return View("UpdateContact");
        }

        public async Task<ActionResult> Update()
        {

            using (var client = new HttpClient())
            {
                Contact leContact = new Contact();
                leContact = new Contact
                {
                    RefContact = Request.Form["RefContact"].ToString().ToUpper(),
                    CodeClient = Request.Form["CodeClient"].ToString().ToUpper(),
                    Nom = Request.Form["Nom"].ToString().ToUpper(),
                    Prenom = Request.Form["Prenom"].ToString(),
                    Tel = Request.Form["Tel"].ToString(),
                    Fonction = Request.Form["Fonction"].ToString(),
                    Email = Request.Form["Email"].ToString(),
                    Adresse1 = Request.Form["Adresse1"].ToString(),
                    Adresse2 = Request.Form["Adresse2"].ToString(),
                    Ville = Request.Form["Ville"].ToString(),
                    CP = Request.Form["CP"].ToString().ToUpper(),
                    Pays = Request.Form["Pays"].ToString()

                };

                string json = JsonConvert.SerializeObject(leContact, Formatting.Indented);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await client.PostAsync(Baseurl + "api/ContactDuClient/Update", data);

                if (response.IsSuccessStatusCode)
                {
                    return View("Client");
                }
            }


            return View("Client");
            
        }
    }
}

