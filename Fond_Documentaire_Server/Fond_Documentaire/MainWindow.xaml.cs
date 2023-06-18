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

namespace Fond_Documentaire
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = txtUsername.Text;
            string mdp = txtPassword.Password;
            DBConnection connection = new DBConnection();
            connection.Server = "13.81.251.33";
            connection.DatabaseName = "julesp_ppe";
            connection.UserName = "developer";
            connection.Password = "cerfal1313";

            if (connection.IsConnect())
            {
                string sql = "SELECT COUNT(*) as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (int.Parse(reader["RESULT"].ToString()) == 1)
                {
                    reader.Close();
                    sql = "SELECT id_role as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                    sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                    sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                    cmd = new MySqlCommand(sql, connection.Connection);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    MainWindow endForm = new MainWindow();
                    switch (int.Parse(reader["RESULT"].ToString()))
                    {
                        case 1:
                            reader.Close();
                            Advert.Content = "Bonjour Gestionnaire";
                            Form_Admin Form = new Form_Admin();
                            Form.Show();
                            endForm.Close();
                            connection.Close();
                            break;
                        case 2:
                            Advert.Content = "Bonjour Operateur";
                            reader.Close();
                            Form_Op Form2 = new Form_Op();
                            Form2.Show();
                            endForm.Close();
                            connection.Close();
                            break;
                        default:
                            Advert.Content = "Aucun résultat";
                            break;


                    }
                    /*if (int.Parse(reader["RESULT"].ToString()) == 1)
                    {
                        reader.Close();
                        Advert.Content = "Bonjour Gestionnaire";
                        Form_Admin Form = new Form_Admin();
                        Form.Show();
                    }
                   else (int.Parse(reader["RESULT"].ToString()) == 2)
                    {
                        Advert.Content = "Bonjour Operateur";
                        reader.Close();
                        Form_Op Form2 = new Form_Op();
                        Form2.Show();
                    }*/
                }


            }
        }
        private void EnterUser()
        {
            // Information de Connexion
            string pseudo = txtUsername.Text;
            string mdp = txtPassword.Password;
            DBConnection connection = new DBConnection();
            connection.Server = "13.81.251.33";
            connection.DatabaseName = "julesp_ppe";
            connection.UserName = "developer";
            connection.Password = "cerfal1313";


            // Connexion
            string sql = "SELECT COUNT(*) as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
            sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
            sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
            var cmd = new MySqlCommand(sql, connection.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (int.Parse(reader["RESULT"].ToString()) == 1)
            {
                reader.Close();
                string sqlCon = "SELECT EstActiver as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                sqlCon = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                sqlCon = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                var cmdCon = new MySqlCommand(sql, connection.Connection);
                var readerCon = cmdCon.ExecuteReader();
                if (int.Parse(reader["RESULT"].ToString()) == 1)
                {
                    reader.Close();
                    sql = "SELECT id_role as RESULT from user WHERE pseudo = @pseudo AND mdp=@mdp";
                    sql = Tools.PrepareLigne(sql, "@pseudo", Tools.PrepareChamp(pseudo, "Chaine"));
                    sql = Tools.PrepareLigne(sql, "@mdp", Tools.PrepareChamp(SHA.GenerateSHA512String(mdp), "Chaine"));
                    cmd = new MySqlCommand(sql, connection.Connection);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    enterlogInfo();
                    MainWindow endForm = new MainWindow();
                    switch (int.Parse(reader["RESULT"].ToString()))
                    {
                        case 1:
                            reader.Close();
                            Advert.Content = "Bonjour Gestionnaire";
                            Form_Admin Form = new Form_Admin();
                            Form.Show();
                            endForm.Close();
                            connection.Close();
                            break;
                        case 2:
                            Advert.Content = "Bonjour Operateur";
                            reader.Close();
                            Form_Op Form2 = new Form_Op();
                            Form2.Show();
                            endForm.Close();
                            connection.Close();
                            break;
                        default:
                            Advert.Content = "Aucun résultat";
                            break;


                    }
                }
            }
        }
        private void enterlogInfo()
        {
            // Information de Connexion
            DBConnection connection = new DBConnection();
            connection.Server = "13.81.251.33";
            connection.DatabaseName = "julesp_ppe";
            connection.UserName = "developer";
            connection.Password = "cerfal1313";

            //Envoi des logs
            string Lepseudo = txtUsername.Text;
            DateTime dateActuelle = DateTime.Now.Date;
            DateTime heureActuelle = DateTime.Now;
            int heures = heureActuelle.Hour;
            int minutes = heureActuelle.Minute;
            string HorraireDeConnexion = heures.ToString() + ':' + minutes.ToString();
            Login newLogin = new Login(dateActuelle, HorraireDeConnexion, Lepseudo);
            newLogin.CreateLogin(connection, dateActuelle, HorraireDeConnexion, Lepseudo);




        }
    }
}
