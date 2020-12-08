using RestauranteMexicano.Areas.Vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestauranteMexicano.Areas.Vendas.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: Vendas/Delivery
        public ActionResult Index()
        {
            var objviewDelivery = new viewDelivery();
            var ListaviewDelivery = objviewDelivery.SelectViewDelivery();
            return View(ListaviewDelivery);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                var objDelivery = new Delivery();
                objDelivery.InsertDelivery(delivery);
                return RedirectToAction("AdicionarItens", "ItensDelivery");
            }
            return View(delivery);
        }

        public ActionResult Edit(int id)
        {
            var delivery = new Delivery() { id = id };
            var objDelivery = new Delivery();
            delivery = objDelivery.SelectIdDelivery(delivery);
            return View(delivery);
        }

        [HttpPost]
        public ActionResult Edit(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                var objDelivery = new Delivery();
                objDelivery.UpdateDelivery(delivery);
                return RedirectToAction("Index");
            }
            return View(delivery);
        }

        public ActionResult Delete(int id)
        {
            var delivery = new Delivery() { id = id };
            var objDelivery = new Delivery();
            delivery = objDelivery.SelectIdDelivery(delivery);
            return View(delivery);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirma(int id)
        {
            var Delivery = new Delivery() { id = id };
            var objDelivery = new Delivery();
            objDelivery.DeleteDelivery(Delivery);
            return RedirectToAction("index");
        }
    }
}