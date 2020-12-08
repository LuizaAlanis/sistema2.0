using MySql.Data.MySqlClient;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.Vendas.Models
{
    public class Pagamento
    {
        public conexaoDB db = new conexaoDB();

        /*
            Table: pagamento

            Columns:
            id int AI PK 
            idComanda int 
            formaPagamento varchar(30)
         */

        // Atributos

        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Comanda")]
        public int idComanda { get; set; }
        public virtual Comanda comanda { get; set; }

        [Required]
        [Display(Name = "Forma de pagamento")]
        public String formaPagamento { get; set; }

        // Métodos

        public void InsertPagamento(Pagamento pagamento)
        {
            string strQuery = string.Format("insert into Pagamento(id, idComanda, formaPagamento) " +
                                         "values ({0},{1},'{2}');", pagamento.id, pagamento.idComanda, pagamento.formaPagamento);

            db.ExecutaComando(strQuery);
        }

        public List<Pagamento> SelectPagamento()
        {
            string StrQuery = "select * from Pagamento;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaPagamento = new List<Pagamento>();

            while (myReader.Read())
            {
                var objPagamento = new Pagamento()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    idComanda = int.Parse(myReader["idComanda"].ToString()),
                    formaPagamento = myReader["formaPagamento"].ToString()
                };
                ListaPagamento.Add(objPagamento);
            }
            myReader.Close();
            return ListaPagamento;
        }

        public void DeletePagamento(Pagamento pagamento)
        {
            string StrQuery = string.Format("delete from Pagamento where id = {0};", pagamento.id);
            db.ExecutaComando(StrQuery);
        }

        public Pagamento SelectIdPagamento(Pagamento pagamento)
        {
            string StrQuery = string.Format("select * from Pagamento where id = {0};", pagamento.id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objPagamento = new Pagamento();

            while (myReader.Read())
            {
                objPagamento.id = int.Parse(myReader["id"].ToString());
                objPagamento.idComanda = int.Parse(myReader["idComanda"].ToString());
                objPagamento.formaPagamento = myReader["id"].ToString();
            }
            myReader.Close();
            return objPagamento;
        }
    }
}