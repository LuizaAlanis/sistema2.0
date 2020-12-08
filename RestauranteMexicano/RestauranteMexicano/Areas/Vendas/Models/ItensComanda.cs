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
    public class ItensComanda
    {
        public conexaoDB db = new conexaoDB();

        /*
            Table: itenscomanda

            Columns:
            id int AI PK 
            idComanda int 
            idProduto int 
            valorUnitario decimal(6,2) 
            quantidade int 
            subtotal decimal(6,2)
             
        */

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Id Comanda")]
        public int idComanda { get; set; }
        public virtual Comanda comanda { get; set; }

        [Display(Name = "Id Produto")]
        public int idProduto { get; set; }
        public virtual Produto produto { get; set; }

        [Display(Name = "Valor unitário")]
        public decimal valorUnitario { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        public ItensComanda SelectIdItens(int id)
        {
            string StrQuery = string.Format("select * from itensComanda where idPedido = {0};", id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objItens = new ItensComanda();

            while (myReader.Read())
            {
                objItens.id = int.Parse(myReader["id"].ToString());
                objItens.idComanda = int.Parse(myReader["idPedido"].ToString());
                objItens.idProduto = int.Parse(myReader["idProduto"].ToString());
                objItens.valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString());
                objItens.quantidade = int.Parse(myReader["quantidade"].ToString());
                objItens.subtotal = decimal.Parse(myReader["subtotal"].ToString());
            }
            myReader.Close();
            return objItens;
        }

        public void InsertItensComanda(ItensComanda itenscomanda)
        {
            string valorUnitario = ValorUnitario(itenscomanda);
            string subtotal = Subtotal(itenscomanda);

            string strQuery = string.Format("insert into itensComanda(id, idComanda, idProduto, valorUnitario, quantidade, subtotal) " +
                                         "values ({0},{1},{2},{3},{4},{5});", itenscomanda.id, itenscomanda.idComanda, itenscomanda.idProduto, valorUnitario, itenscomanda.quantidade, subtotal);

            db.ExecutaComando(strQuery);
        }

        public List<ItensComanda> SelectItensComanda()
        {
            string StrQuery = "select * from itensComanda;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaItensComanda = new List<ItensComanda>();

            while (myReader.Read())
            {
                var objItensComanda = new ItensComanda()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    idComanda = int.Parse(myReader["idComanda"].ToString()),
                    idProduto = int.Parse(myReader["idProduto"].ToString()),
                    valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString()),
                    quantidade = int.Parse(myReader["quantidade"].ToString()),
                    subtotal = decimal.Parse(myReader["subtotal"].ToString()),
                };
                ListaItensComanda.Add(objItensComanda);
            }
            myReader.Close();
            return ListaItensComanda;
        }

        public void DeleteItensComanda(ItensComanda itenscomanda)
        {
            string StrQuery = string.Format("delete from itensComanda where id = {0};", itenscomanda.id);
            db.ExecutaComando(StrQuery);
        }

        public ItensComanda SelectIdItensComanda(ItensComanda itenscomanda)
        {
            string StrQuery = string.Format("select * from itensComanda where id = {0};", itenscomanda.id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objItensComanda = new ItensComanda();

            while (myReader.Read())
            {
                objItensComanda.id = int.Parse(myReader["id"].ToString());
                objItensComanda.idComanda = int.Parse(myReader["idComanda"].ToString());
                objItensComanda.idProduto = int.Parse(myReader["idProduto"].ToString());
                objItensComanda.valorUnitario = decimal.Parse(myReader["valorUnitario"].ToString());
                objItensComanda.quantidade = int.Parse(myReader["quantidade"].ToString());
                objItensComanda.subtotal = decimal.Parse(myReader["subtotal"].ToString());

            }
            myReader.Close();
            return objItensComanda;
        }

        public void UpdateItensComanda(ItensComanda itenscomanda)
        {
            string comando = string.Format("select valor from Produto where id = {0};", itenscomanda.idProduto);
            itenscomanda.valorUnitario = decimal.Parse(db.RetornaDado(comando));
            string subtotal = Subtotal(itenscomanda);

            string StrQuery = "";
            StrQuery += "update itensComanda set ";
            StrQuery += string.Format("idComanda = {0}, ", itenscomanda.idComanda);
            StrQuery += string.Format("idProduto = {0}, ", itenscomanda.idProduto);
            StrQuery += string.Format("quantidade = {0}, ", itenscomanda.quantidade);
            StrQuery += string.Format("subtotal = {0} ", subtotal);
            StrQuery += string.Format("where id = {0};", itenscomanda.id);

            db.ExecutaComando(StrQuery);
        }

        public string ValorUnitario(ItensComanda itenscomanda)
        {
            string comando = string.Format("select valor from Produto where id = {0};", itenscomanda.idProduto);
            itenscomanda.valorUnitario = decimal.Parse(db.RetornaDado(comando));

            var ItenValorUnitario = itenscomanda.valorUnitario.ToString();
            ItenValorUnitario = ItenValorUnitario.Replace(",", ".");

            return ItenValorUnitario;
        }

        public string Subtotal(ItensComanda itenscomanda)
        {
            itenscomanda.subtotal = itenscomanda.valorUnitario * itenscomanda.quantidade;

            var Itensubtotal = itenscomanda.subtotal.ToString();
            Itensubtotal = Itensubtotal.Replace(",", ".");

            return Itensubtotal;
        }

        public void Atualizar(ItensComanda itenscomanda)
        {
            string comando = string.Format("select sum(subtotal) from itensComanda where idComanda = {0};", itenscomanda.idComanda);
            var NovoTotal = db.RetornaDado(comando).ToString();
            NovoTotal = NovoTotal.Replace(",", ".");

            string update = "";
            update += "update Comanda set ";
            update += string.Format("total = {0} ", NovoTotal);
            update += string.Format("where id = {0};", itenscomanda.idComanda);

            db.ExecutaComando(update);

        }
    }
}