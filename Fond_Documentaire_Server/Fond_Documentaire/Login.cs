using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Fond_Documentaire
{
    class Login
    {
        private int _ID;
        private DateTime _Date;
        private string _Time;
        private string _Pseudo;

        [XmlElement()]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [XmlElement()]
        public string DATE
        {
            get { return _Date.ToString(); }
            set { _Date = DateTime.Parse(value); }
        }
        [XmlElement()]
        public string TIME
        {
            get { return _Time; }
            set { _Time = value; }
        }
        [XmlElement()]
        public string PSEUDO
        {
            get { return _Pseudo; }
            set { _Pseudo = value; }
        }
        public Login(int ID, DateTime Date, string Time, string Pseudo)
        {
            _ID = ID;
            _Date = Date;
            _Time = Time;
            _Pseudo= Pseudo;
        }
        public Login( DateTime Date, string Time, string Pseudo)
        {
            _Date = Date;
            _Time = Time;
            _Pseudo = Pseudo;
        }
        public void CreateLogin(DBConnection connection, DateTime newDate, string newHeure, string pseudo)
        {
            string sql;
            try
            {
                var cmd = new MySqlCommand();
                sql = "INSERT INTO login (Date,Heure,LeUser) VALUES (@Date,@Heure ,@pseudo)";// ajouter une colonne timestamp qui contient la date et l'heure courante
                sql = Tools.PrepareLigne(sql, "@Date", Tools.PrepareChamp(newDate.ToString(), "Chaine"));
                sql = Tools.PrepareLigne(sql, "@Heure", Tools.PrepareChamp(newHeure, "Chaine"));
                sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo.ToString(), "Nombre"));
                cmd = new MySqlCommand(sql, connection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception b)
            {

            }
        }
    }


   
}

