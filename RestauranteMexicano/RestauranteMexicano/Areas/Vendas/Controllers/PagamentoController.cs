using RestauranteMexicano.Areas.Vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestauranteMexicano.Areas.Vendas.Controllers
{
    public class PagamentoController : Controller
    {
        // GET: Vendas/Pagamento
        public ActionResult Index()
        {
            var objPagamento = new Pagamento();
            var ListaPagamento = objPagamento.SelectPagamento();
            return View(ListaPagamento);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                var objPagamento = new Pagamento();
                objPagamento.InsertPagamento(pagamento);
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        public ActionResult Delete(int id)
        {
            var pagamento = new Pagamento() { id = id };
            var objPagamento = new Pagamento();
            pagamento = objPagamento.SelectIdPagamento(pagamento);
            return View(pagamento);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirma(int id)
        {
            var pagamento = new Pagamento() { id = id };
            var objPagamento = new Pagamento();
            objPagamento.DeletePagamento(pagamento);
            return RedirectToAction("index");
        }
    }
}