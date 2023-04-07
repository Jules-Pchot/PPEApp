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
    [XmlRoot()]
    class User
    {
        private int _ID;
        private string _Pseudo;
        private string _Mdp;
        private int _ID_role;
        private bool _isGest;
        private bool _isOp;

        [XmlElement()]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [XmlElement()]
        public string PSEUDO
        {
            get { return _Pseudo; }
            set { _Pseudo = value; }
        }
        [XmlElement()]
        public string MDP
        {
            get { return _Mdp; }
            set { _Mdp = value; }
        }
        [XmlElement()]
        public int ID_ROLE
        {
            get { return _ID_role; }
            set { _ID_role = value; }
        }
        /* [XmlElement()]
         public bool IsGest
         {
             get { return _isGest; }
             set { _isGest = value;
                 OnPropertyChanged("IsManagerSelected");
                 UpdateRoleId();

             }
         }
         [XmlElement()]
         public bool IsOp
         {
             get { return _isOp; }
             set { _isOp = value; }
         }*/

        public User(int ID, string pseudo, string mdp, int ID_ROLE)
        {
            _ID = ID;
            _Pseudo = pseudo;
            _Mdp = mdp;
            _ID_role = ID_ROLE;
        }
        public User(string pseudo,string mdp, int ID_ROLE)
        {
            _Pseudo = pseudo;
            _Mdp = mdp;
            _ID_role = ID_ROLE;
        }

        public static string Affiche_pseudo(DBConnection connection)
        {
            string sql = "SELECT pseudo from user";
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string pseudo = reader["pseudo"].ToString();
            reader.Close();
            return pseudo;
        }
        public static string Affiche_mdp(DBConnection connection)
        {
            string sql = "SELECT mdp from user";
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string mdp = reader["mdp"].ToString();
            reader.Close();
            return mdp;
        }
        public static int Affiche_id(DBConnection connection)
        {
            string sql = "SELECT id from user";
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int id = int.Parse(reader["id"].ToString());
            reader.Close();
            return id;
        }
        public static int Affiche_Role_ID(DBConnection connection)
        {
            string sql = "SELECT id_role from user";
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int Role_ID = int.Parse(reader["id_role"].ToString());
            reader.Close();
            return Role_ID;
        }
        public static int CountUser(DBConnection connection)
        {
            string sql = "SELECT COUNT(*) as res from user";
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int Count = int.Parse(reader["res"].ToString());
            reader.Close();
            return Count;
        }
        public static User Afficheur(DBConnection connection)
        {
           User User = new User(Affiche_id(connection), Affiche_pseudo(connection), Affiche_mdp(connection), Affiche_Role_ID(connection));

            return User;
        }
        public void Modify(DBConnection connection,string pseudo, int id_role, int id)
        {
            string sql;
            var cmd = new MySqlCommand();
            sql = "UPDATE user SET pseudo = @pseudo AND id_role = @id_role WHERE id = @id";
            sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
            sql = Tools.PrepareLigne(sql, "@id", Tools.PrepareChamp(id_role.ToString(), "Nombre"));
            sql = Tools.PrepareLigne(sql, "@id", Tools.PrepareChamp(id.ToString(), "Nombre"));
            cmd = new MySqlCommand(sql, connection.Connection);
            cmd.ExecuteNonQuery();
            
        }
        public void Suppr(DBConnection connection, int id)
        {
            string sql;
            var cmd = new MySqlCommand();
            sql = "DELETE FROM user WHERE id = @id";
            sql = Tools.PrepareLigne(sql, "@id", Tools.PrepareChamp(id.ToString(), "Nombre"));
            cmd = new MySqlCommand(sql, connection.Connection);
            cmd.ExecuteNonQuery();
        }
        public void CreateUser(DBConnection connection, string newPseudo, string newMDP, int idRole)
        {
            string sql;
            var cmd = new MySqlCommand();
            sql = "INSERT INTO user VALUES (null, @pseudo, @mdp , @id_role)";
            sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(newPseudo, "Chaine"));
            sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(newMDP, "Chaine"));
            sql = Tools.PrepareLigne(sql, "@id", Tools.PrepareChamp(idRole.ToString(), "Nombre"));
            cmd = new MySqlCommand(sql, connection.Connection);
            cmd.ExecuteNonQuery();
        }


    }
}
