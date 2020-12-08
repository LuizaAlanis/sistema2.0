using MySql.Data.MySqlClient;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.Vendas.Models
{
    public class Delivery
    {
        public conexaoDB db = new conexaoDB();

        /*
            Table: delivery

            Columns:
            id int PK 
            dataEntrega date 
            total decimal(6,2) 
            destinatario varchar(50) 
            endereco varchar(150)
         */

        // Atributos

        [Display(Name = "Id")]
        [Key]
        public int id { get; set; }

        [Display(Name = "Data")]
        public DateTime data { get; set; }

        [Display(Name = "Total")]
        public decimal total { get; set; }

        [Display(Name = "Destinatário")]
        public string destinatario { get; set; }

        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        public ICollection<ItensDelivery> ItensDelivery { get; set; }

        // Métodos

        public void InsertDelivery(Delivery delivery)
        {
            string strQuery = string.Format("insert into Delivery(id, dataEntrega, total, destinatario, endereco) " +
                                         "values ({0},'{1}',{2},'{3}','{4}');", delivery.id, delivery.data.ToString("yyyy-MM-dd"), delivery.total, delivery.destinatario, delivery.endereco);

            db.ExecutaComando(strQuery);
        }

        public List<Delivery> SelectDelivery()
        {
            string StrQuery = "select * from Delivery;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaDelivery = new List<Delivery>();

            while (myReader.Read())
            {
                var objDelivery = new Delivery()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    data = DateTime.Parse(myReader["dataEntrega"].ToString()),
                    total = decimal.Parse(myReader["total"].ToString()),
                    destinatario = myReader["destinatario"].ToString(),
                    endereco = myReader["endereco"].ToString()
                };
                ListaDelivery.Add(objDelivery);
            }
            myReader.Close();
            return ListaDelivery;
        }

        public void DeleteDelivery(Delivery delivery)
        {
            string comando = string.Format("delete from ItensDelivery where idDelivery = {0};", delivery.id);
            db.ExecutaComando(comando);

            string StrQuery = string.Format("delete from Delivery where id = {0};", delivery.id);
            db.ExecutaComando(StrQuery);
        }

        public Delivery SelectIdDelivery(Delivery delivery)
        {
            string StrQuery = string.Format("select * from Delivery where id = {0};", delivery.id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objDelivery = new Delivery();

            while (myReader.Read())
            {
                objDelivery.id = int.Parse(myReader["id"].ToString());
                objDelivery.data = DateTime.Parse(myReader["dataEntrega"].ToString());
                objDelivery.total = decimal.Parse(myReader["total"].ToString());
                objDelivery.destinatario = myReader["destinatario"].ToString();
                objDelivery.endereco = myReader["endereco"].ToString();
            }
            myReader.Close();
            return objDelivery;
        }

        public void UpdateDelivery(Delivery delivery)
        {
            string StrQuery = "";
            StrQuery += "update Delivery set ";
            StrQuery += string.Format("dataEntrega = '{0}', ", delivery.data.ToString("yyyy-MM-dd"));
            StrQuery += string.Format("total = {0}, ", delivery.total);
            StrQuery += string.Format("destinatario = '{0}', ", delivery.destinatario);
            StrQuery += string.Format("endereco = '{0}' ", delivery.endereco);
            StrQuery += string.Format("where id = {0};", delivery.id);

            db.ExecutaComando(StrQuery);
        }
    }
}