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
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Fond_Documentaire
{
    /// <summary>
    /// Logique d'interaction pour Form_Op.xaml
    /// </summary>
    public partial class Form_Op : Window
    {
        List<Ouvrage> Afficheur_Ouvrage = new List<Ouvrage> { };

        public Form_Op()
        {
            InitializeComponent();
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            int compte = 0;
            if (connection.IsConnect())
            {
                string sql = "SELECT ISBN as ID, Titre as titre,Auteur as Auteur FROM ouvrage";

                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read()|| compte <30)
                {
                    Afficheur_Ouvrage.Add(new Ouvrage(int.Parse(reader["ID"].ToString()), reader["titre"].ToString(), reader["Auteur"].ToString()));
                    compte++;
                }

                listView.ItemsSource = Afficheur_Ouvrage;
            }
        }

        private void AfficheDetail(object sender, RoutedEventArgs e)
        {
            DetailOP DetailForm = new DetailOP();
            DetailForm.Show();
        }
    }
    }

