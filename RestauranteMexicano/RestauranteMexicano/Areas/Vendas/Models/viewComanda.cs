using MySql.Data.MySqlClient;
using RestauranteMexicano.Areas.ADM.Models;
using RestauranteMexicano.Areas.Vendas.Models;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.Vendas.Models
{
    public class viewComanda
    {
        public conexaoDB db = new conexaoDB();

        /*
            View: viewdelivery

            Columns:
	        Comanda.id,
            Comanda.idMesa,
            Comanda.dataPedido,
            Produto.produto,
            Produto.valor,
            itensComanda.quantidade,
            itensComanda.subtotal,
            Comanda.total
        */

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Mesa")]
        public int idMesa { get; set; }

        [Display(Name = "Data")]
        public DateTime dataPedido { get; set; }

        [Display(Name = "Produto")]
        public string produto { get; set; }

        [Display(Name = "Valor")]
        public decimal valor { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        [Display(Name = "Total")]
        public decimal total { get; set; }

        public List<viewComanda> SelectViewComanda()
        {
            string StrQuery = "select * from viewComanda order by id desc;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaviewComanda = new List<viewComanda>();

            while (myReader.Read())
            {
                var objviewComanda = new viewComanda()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    idMesa = int.Parse(myReader["idMesa"].ToString()),
                    dataPedido = DateTime.Parse(myReader["dataPedido"].ToString()),
                    produto = myReader["produto"].ToString(),
                    valor = decimal.Parse(myReader["valor"].ToString()),
                    quantidade = int.Parse(myReader["quantidade"].ToString()),
                    subtotal = decimal.Parse(myReader["subtotal"].ToString()),
                    total = decimal.Parse(myReader["total"].ToString())
                };
                ListaviewComanda.Add(objviewComanda);
            }
            myReader.Close();
            return ListaviewComanda;
        }

        public List<viewComanda> SelectPesquisa(string pesquisa)
        {
            string StrQuery = string.Format("select * from vwComanda where id = {0};", pesquisa);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaviewComanda = new List<viewComanda>();

            while (myReader.Read())
            {
                var objviewComanda = new viewComanda()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    idMesa = int.Parse(myReader["idMesa"].ToString()),
                    dataPedido = DateTime.Parse(myReader["dataPedido"].ToString()),
                    produto = myReader["produto"].ToString(),
                    valor = decimal.Parse(myReader["valor"].ToString()),
                    quantidade = int.Parse(myReader["quantidade"].ToString()),
                    subtotal = decimal.Parse(myReader["subtotal"].ToString()),
                    total = decimal.Parse(myReader["total"].ToString())
                };
                ListaviewComanda.Add(objviewComanda);
            }
            myReader.Close();
            return ListaviewComanda;
        }

    }
}