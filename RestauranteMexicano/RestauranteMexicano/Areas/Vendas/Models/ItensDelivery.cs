using MySql.Data.MySqlClient;
using RestauranteMexicano.Areas.ADM.Models;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.Vendas.Models
{
    public class ItensDelivery
    {
        public conexaoDB db = new conexaoDB();

        /*
            Table: itensdelivery

            Columns:
            id int AI PK 
            idDelivery int 
            idProduto int 
            valorUnitario decimal(6,2) 
            quantidade int 
            subtotal decimal(6,2)
             
        */

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Id Delivery")]
        public int idDelivery { get; set; }
        public virtual Delivery Delivery { get; set; }

        [Display(Name = "Id Produto")]
        public int idProduto { get; set; }
        public virtual Produto produto { get; set; }

        [Display(Name = "Valor unitário")]
        public decimal valorUnitario { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        public ItensDelivery SelectIdItens(int id)
        {
            string StrQuery = string.Format("select * from itensDelivery where idPedido = {0};", id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objItens = new ItensDelivery();

            while (myReader.Read())
            {
                objItens.id = int.Parse(myReader["id"].ToString());
                objItens.idDelivery = int.Parse(myReader["idPedido"].ToString());
                objItens.idProduto = int.Parse(myReader["idProduto"].ToString());
                objItens.valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString());
                objItens.quantidade = int.Parse(myReader["quantidade"].ToString());
                objItens.subtotal = decimal.Parse(myReader["subtotal"].ToString());
            }
            myReader.Close();
            return objItens;
        }

        public void InsertItensDelivery(ItensDelivery itensdelivery)
        {
            string valorUnitario = ValorUnitario(itensdelivery);
            string subtotal = Subtotal(itensdelivery);

            string strQuery = string.Format("insert into itensDelivery(id, idDelivery, idProduto, valorUnitario, quantidade, subtotal) " +
                                         "values ({0},{1},{2},{3},{4},{5});", itensdelivery.id, itensdelivery.idDelivery, itensdelivery.idProduto, valorUnitario, itensdelivery.quantidade, subtotal);

            db.ExecutaComando(strQuery);
        }

        public List<ItensDelivery> SelectItensDelivery()
        {
            string StrQuery = "select * from itensDelivery;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaItensDelivery = new List<ItensDelivery>();

            while (myReader.Read())
            {
                var objItensDelivery = new ItensDelivery()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    idDelivery = int.Parse(myReader["idDelivery"].ToString()),
                    idProduto = int.Parse(myReader["idProduto"].ToString()),
                    valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString()),
                    quantidade = int.Parse(myReader["quantidade"].ToString()),
                    subtotal = decimal.Parse(myReader["subtotal"].ToString()),
                };
                ListaItensDelivery.Add(objItensDelivery);
            }
            myReader.Close();
            return ListaItensDelivery;
        }

        public void DeleteItensDelivery(ItensDelivery itensdelivery)
        {
            string StrQuery = string.Format("delete from itensDelivery where id = {0};", itensdelivery.id);
            db.ExecutaComando(StrQuery);
        }

        public ItensDelivery SelectIdItensDelivery(ItensDelivery itensdelivery)
        {
            string StrQuery = string.Format("select * from itensDelivery where id = {0};", itensdelivery.id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objItensDelivery = new ItensDelivery();

            while (myReader.Read())
            {
                objItensDelivery.id = int.Parse(myReader["id"].ToString());
                objItensDelivery.idDelivery = int.Parse(myReader["idDelivery"].ToString());
                objItensDelivery.idProduto = int.Parse(myReader["idProduto"].ToString());
                objItensDelivery.valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString());
                objItensDelivery.quantidade = int.Parse(myReader["quantidade"].ToString());
                objItensDelivery.subtotal = decimal.Parse(myReader["subtotal"].ToString());

            }
            myReader.Close();
            return objItensDelivery;
        }

        public void UpdateItensDelivery(ItensDelivery itensdelivery)
        {
            string comando = string.Format("select valor from Produto where id = {0};", itensdelivery.idProduto);
            itensdelivery.valorUnitario = decimal.Parse(db.RetornaDado(comando));
            string subtotal = Subtotal(itensdelivery);

            string StrQuery = "";
            StrQuery += "update itensDelivery set ";
            StrQuery += string.Format("idDelivery = {0}, ", itensdelivery.idDelivery);
            StrQuery += string.Format("idProduto = {0}, ", itensdelivery.idProduto);
            StrQuery += string.Format("quantidade = {0}, ", itensdelivery.quantidade);
            StrQuery += string.Format("subtotal = {0} ", subtotal);
            StrQuery += string.Format("where id = {0};", itensdelivery.id);

            db.ExecutaComando(StrQuery);
        }

        public string ValorUnitario(ItensDelivery itensdelivery)
        {
            string comando = string.Format("select valor from Produto where id = {0};", itensdelivery.idProduto);
            itensdelivery.valorUnitario = decimal.Parse(db.RetornaDado(comando));

            var ItenValorUnitario = itensdelivery.valorUnitario.ToString();
            ItenValorUnitario = ItenValorUnitario.Replace(",", ".");

            return ItenValorUnitario;
        }

        public string Subtotal(ItensDelivery itensdelivery)
        {
            itensdelivery.subtotal = itensdelivery.valorUnitario * itensdelivery.quantidade;

            var Itensubtotal = itensdelivery.subtotal.ToString();
            Itensubtotal = Itensubtotal.Replace(",", ".");

            return Itensubtotal;
        }

        public void Atualizar(ItensDelivery itensdelivery)
        {
            string comando = string.Format("select sum(subtotal) from itensDelivery where idDelivery = {0};", itensdelivery.idDelivery);
            var NovoTotal = db.RetornaDado(comando).ToString();
            NovoTotal = NovoTotal.Replace(",", ".");

            string update = "";
            update += "update Delivery set ";
            update += string.Format("total = {0} ", NovoTotal);
            update += string.Format("where id = {0};", itensdelivery.idDelivery);

            db.ExecutaComando(update);

        }
    }
}