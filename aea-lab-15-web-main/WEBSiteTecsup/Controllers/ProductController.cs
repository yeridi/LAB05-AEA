using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEBSiteTecsup.Models;
using Response;
using System.Text;

namespace WEBSiteTecsup.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<ProductModel> model = new List<ProductModel>();
            var client = new HttpClient();
            var urlBase = "https://localhost:44315";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/Api", "/Products", "/GetProducts");


            var response = client.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                //De JSON a Response
                var products = JsonConvert.DeserializeObject<List<ProductResponse>>(result);

                //De Response a Model
                model = (from c in products
                         select new ProductModel
                         {
                             FullName = string.Concat(c.Name, " S/.", c.Price)
                         }).ToList();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateModel model)
        {
            try
            {
                //Class a JSON
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var urlBase = "https://localhost:44315";
                client.BaseAddress = new Uri(urlBase);
                var url = string.Concat(urlBase, "/Api", "/Products", "/PostProduct");

                var response = client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    //Si sale algo con error, puedes enviar una alerta.
                    //var exito = JsonConvert.DeserializeObject<bool>(result);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}