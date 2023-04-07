using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Gestion_Doc
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Page
    {
        public Connexion()
        {
            InitializeComponent();
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = txtUsername.Text;
            string mdp = txtPassword.Password;
            DBConnection connection = new DBConnection();
            connection.Server = "127.0.0.1";
            connection.DatabaseName = "fond_doc";
            connection.UserName = "visualstudio";
            connection.Password = "ppevallade";

            if (connection.IsConnect())
            {
                string sql = "SELECT COUNT(*) as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                if (int.Parse(reader["RESULT"].ToString()) == 1)
                {
                    reader.Close();
                    sql = "SELECT id_role as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                    sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                    sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                    cmd = new MySqlCommand(sql, connection.Connection);
                    reader = cmd.ExecuteReader();

                    if(int.Parse(reader["RESULT"].ToString()) == 1)
                    {
                        Advert.Content = "Bonjour Gestionnaire";
                    }else Advert.Content = "Bonjour Operateur";
                }
                else Advert.Content = "Aucun résultat";
            }
        }
    }
}
