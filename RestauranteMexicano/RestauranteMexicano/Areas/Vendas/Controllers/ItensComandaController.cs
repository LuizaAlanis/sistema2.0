using RestauranteMexicano.Areas.Vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestauranteMexicano.Areas.Vendas.Controllers
{
    public class ItensComandaController : Controller
    {
        // GET: Vendas/ItensComanda
        public ActionResult Index()
        {
            var objItens = new ItensComanda();
            var ListaItens = objItens.SelectItensComanda();
            return View(ListaItens);
        }

        public ActionResult AdicionarItens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarItens(ItensComanda itensComanda)
        {
            if (ModelState.IsValid)
            {
                var objItens = new ItensComanda();
                objItens.InsertItensComanda(itensComanda);
                objItens.Atualizar(itensComanda);
                return RedirectToAction("Index");
            }
            return View(itensComanda);
        }

        public ActionResult Edit(int id)
        {
            var itenscomanda = new ItensComanda() { id = id };
            var objItensComanda = new ItensComanda();
            itenscomanda = objItensComanda.SelectIdItensComanda(itenscomanda);
            return View(itenscomanda);
        }

        [HttpPost]
        public ActionResult Edit(ItensComanda itenscomanda)
        {
            if (ModelState.IsValid)
            {
                var objitensComanda = new ItensComanda();
                objitensComanda.UpdateItensComanda(itenscomanda);
                objitensComanda.Atualizar(itenscomanda);
                return RedirectToAction("Index");
            }
            return View(itenscomanda);
        }

        public ActionResult Delete(int id)
        {
            var Itenscomanda = new ItensComanda() { id = id };
            var objItensComanda = new ItensComanda();
            Itenscomanda = objItensComanda.SelectIdItensComanda(Itenscomanda);
            return View(Itenscomanda);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirma(int id)
        {
            var Itenscomanda = new ItensComanda() { id = id };
            var objItensComanda = new ItensComanda();
            objItensComanda.DeleteItensComanda(Itenscomanda);
            return RedirectToAction("index");
        }
    }
}