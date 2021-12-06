using APITecsup.Context;
using APITecsup.Models;
using Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITecsup.Controllers
{
    public class InvoicesController : ApiController
    {
        private ExampleContext db = new ExampleContext();
        
        [HttpPost]
        public InvoiceResponse Insert(Invoice request)
        {           
            db.Invoices.Add(request);
            db.SaveChanges();
            var response = new InvoiceResponse
            {
                InvoiceID = request.InvoiceID,
                Number = request.Number
            };
            return response;
        }

        [HttpGet]
        public List<InvoiceResponseV2> GetByPrice(int MinPrice, int MaxPrice )
        {
            var invoices = db.Invoices.
                Where(x => x.Total > MinPrice && x.Total < MaxPrice)
                .ToList();

            var response = (from c in invoices
                           select new InvoiceResponseV2
                           {
                               Number = c.Number,
                               Total = c.Total
                           }).ToList();
            return response;
        }

    }
}
