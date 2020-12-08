using MySql.Data.MySqlClient;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.Vendas.Models
{
    public class viewDelivery
    {
        public conexaoDB db = new conexaoDB();

        /*
            View: viewdelivery

            Columns:
            id int 
            dataEntrega date 
            destinatario varchar(50) 
            endereco varchar(150) 
            produto varchar(50) 
            valor decimal(6,2) 
            quantidade int 
            subtotal decimal(6,2) 
            total decimal(6,2)
        */

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Data")]
        public DateTime dataEntrega { get; set; }

        [Display(Name = "Destinatário")]
        public string destinatario { get; set; }

        [Display(Name = "Enredeço")]
        public string endereco { get; set; }

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

        public List<viewDelivery> SelectViewDelivery()
        {
            string StrQuery = "select * from viewDelivery order by id desc;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaviewDelivery = new List<viewDelivery>();

            while (myReader.Read())
            {
                var objviewDelivery = new viewDelivery()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    dataEntrega = DateTime.Parse(myReader["dataEntrega"].ToString()),
                    destinatario = myReader["destinatario"].ToString(),
                    endereco = myReader["endereco"].ToString(),
                    produto = myReader["produto"].ToString(),
                    valor = decimal.Parse(myReader["valor"].ToString()),
                    quantidade = int.Parse(myReader["quantidade"].ToString()),
                    subtotal = decimal.Parse(myReader["subtotal"].ToString()),
                    total = decimal.Parse(myReader["total"].ToString())
                };
                ListaviewDelivery.Add(objviewDelivery);
            }
            myReader.Close();
            return ListaviewDelivery;
        }
    }
}