using MSPR1WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace MSPR1WebApp.Controllers
{
    public class FactureController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44340/";
        public async Task<ActionResult> Index()
        {
            List<Facture> FactureInfo = new List<Facture>();
            ViewBag.CodeClient = getCodeFacture();
            if (ViewBag.CodeClient != null)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("v1/factures/" + ViewBag.CodeClient.Content);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var ClientResponse = Res.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                        //Deserializing the response recieved from web api and storing into the Client list
                        FactureInfo = JsonConvert.DeserializeObject<List<Facture>>(ClientResponse.Replace("rn", ""));

                        ViewBag.All = FactureInfo;
                        for (int i = 0; i < FactureInfo.Count; i++)
                        { //returning the Client list to view
                            ViewBag.All[i].RefFacture = FactureInfo[i].RefFacture;
                            ViewBag.All[i].DateCrea = FactureInfo[i].DateCrea;
                            ViewBag.All[i].RefUtilisateur = FactureInfo[i].RefUtilisateur;
                            ViewBag.All[i].ObjetFacture = FactureInfo[i].ObjetFacture;
                            ViewBag.All[i].CodeFacture = FactureInfo[i].CodeClient;
                            ViewBag.All[i].CiviliteContact = FactureInfo[i].CiviliteContact;
                            ViewBag.All[i].CodeDossier = FactureInfo[i].NomContact;
                            ViewBag.All[i].PrenomContact = FactureInfo[i].PrenomContact;
                            ViewBag.All[i].RaisonSociale = FactureInfo[i].RaisonSociale;
                            ViewBag.All[i].Adresse1 = FactureInfo[i].Adresse1;
                            ViewBag.All[i].Adresse2 = FactureInfo[i].Adresse2;
                            ViewBag.All[i].CP = FactureInfo[i].CP;
                            ViewBag.All[i].Ville = FactureInfo[i].Ville;
                            ViewBag.All[i].Pays = FactureInfo[i].Pays;
                            ViewBag.All[i].Reglement = FactureInfo[i].Reglement;
                            ViewBag.All[i].CodeTva = FactureInfo[i].CodeTva;
                            ViewBag.All[i].TotHT = FactureInfo[i].TotHT;
                            ViewBag.All[i].TotTTC = FactureInfo[i].TotTTC;
                            ViewBag.All[i].DateEcheance = FactureInfo[i].DateEcheance;
                        }
                        ViewBag.NbOfElements = FactureInfo.Count - 1;
                    }
                    return View("Facture");
                }
            }
            return View("Facture");
        }
        [HttpPost]
        public ActionResult getCodeFacture()
        {
            string codeClient = null;
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
    }
}
