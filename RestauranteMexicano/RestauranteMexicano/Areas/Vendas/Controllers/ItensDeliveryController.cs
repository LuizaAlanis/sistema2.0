using RestauranteMexicano.Areas.Vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestauranteMexicano.Areas.Vendas.Controllersitensdelivery
{
    public class ItensDeliveryController : Controller
    {
        // GET: Vendas/ItensDelivery
        public ActionResult Index()
        {
            var objitensdelivery = new ItensDelivery();
            var listaitensdelivery = objitensdelivery.SelectItensDelivery();
            return View(listaitensdelivery);
        }

        public ActionResult AdicionarItens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarItens(ItensDelivery itensdelivery)
        {
            if (ModelState.IsValid)
            {
                var objitensdelivery = new ItensDelivery();
                objitensdelivery.InsertItensDelivery(itensdelivery);
                objitensdelivery.Atualizar(itensdelivery);
                return RedirectToAction("Index");
            }
            return View(itensdelivery);
        }

        public ActionResult Edit(int id)
        {
            var itensdelivery = new ItensDelivery() { id = id };
            var objitensdelivery = new ItensDelivery();
            itensdelivery = objitensdelivery.SelectIdItensDelivery(itensdelivery);
            return View(itensdelivery);
        }

        [HttpPost]
        public ActionResult Edit(ItensDelivery itensDelivery)
        {
            if (ModelState.IsValid)
            {
                var objitensDelivery = new ItensDelivery();
                objitensDelivery.UpdateItensDelivery(itensDelivery);
                objitensDelivery.Atualizar(itensDelivery);
                return RedirectToAction("Index");
            }
            return View(itensDelivery);
        }

        public ActionResult Delete(int id)
        {
            var itensdelivery = new ItensDelivery() { id = id };
            var objitensdelivery = new ItensDelivery();
            itensdelivery = objitensdelivery.SelectIdItensDelivery(itensdelivery);
            return View(itensdelivery);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirma(int id)
        {
            var itensdelivery = new ItensDelivery() { id = id };
            var objitensdelivery = new ItensDelivery();
            objitensdelivery.DeleteItensDelivery(itensdelivery);
            return RedirectToAction("index");
        }
    }
}