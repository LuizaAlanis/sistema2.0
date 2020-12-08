using MySql.Data.MySqlClient;
using RestauranteMexicano.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteMexicano.Areas.ADM.Models
{
    public class Celular
    {
        public conexaoDB db = new conexaoDB();

        /*
            Table: celular

            Columns:
            id int AI PK 
            proprietario varchar(50) 
            observacao varchar(50) 
            ddd int 
            numero int

         */

        // Atributos

        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Proprietario")]
        public String proprietario { get; set; }

        [Required]
        [Display(Name = "Observação")]
        [StringLength(50, ErrorMessage = "O limite é de 50 caracteres.")]
        public String observacao { get; set; }

        [Required]
        [Display(Name = "DDD")]
        public int ddd { get; set; }

        [Required]
        [Display(Name = "Numero")]
        public int numero { get; set; }

        // Métodos

        public void InsertCelular(Celular celular)
        {
            string strQuery = string.Format("insert into Celular(id, proprietario, observacao, ddd, numero) " +
                                         "values ({0},'{1}','{2}',{3},{4});", celular.id, celular.proprietario, celular.observacao, celular.ddd, celular.numero);

            db.ExecutaComando(strQuery);
        }

        public List<Celular> SelectCelular()
        {
            string StrQuery = "select * from Celular;";
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);
            var ListaCelular = new List<Celular>();

            while (myReader.Read())
            {
                var objCelular = new Celular()
                {
                    id = int.Parse(myReader["id"].ToString()),
                    proprietario = myReader["id"].ToString(),
                    observacao = myReader["id"].ToString(),
                    ddd = int.Parse(myReader["id"].ToString()),
                    numero = int.Parse(myReader["id"].ToString())
                };
                ListaCelular.Add(objCelular);
            }
            myReader.Close();
            return ListaCelular;
        }

        public void DeleteCelular(Celular celular)
        {
            string StrQuery = string.Format("delete from Celular where id = {0};", celular.id);
            db.ExecutaComando(StrQuery);
        }

        public Celular SelectIdCelular(Celular celular)
        {
            string StrQuery = string.Format("select * from Celular where id = {0};", celular.id);
            MySqlDataReader myReader = db.RetornaRegistro(StrQuery);

            var objCelular = new Celular();

            while (myReader.Read())
            {
                objCelular.id = int.Parse(myReader["id"].ToString());
                objCelular.proprietario = myReader["id"].ToString();
                objCelular.observacao = myReader["id"].ToString();
                objCelular.ddd = int.Parse(myReader["id"].ToString());
                objCelular.numero = int.Parse(myReader["id"].ToString());
            }
            myReader.Close();
            return objCelular;
        }

        public void UpdateCelular(Celular celular)
        {
            string StrQuery = "";
            StrQuery += "update Celular set ";
            StrQuery += string.Format("proprietario = '{0}', ", celular.proprietario);
            StrQuery += string.Format("observacao = '{0}', ", celular.observacao);
            StrQuery += string.Format("ddd = {0}, ", celular.ddd);
            StrQuery += string.Format("numero = {0} ", celular.numero);
            StrQuery += string.Format("where id = {0};", celular.id);

            db.ExecutaComando(StrQuery);
        }
    }
}