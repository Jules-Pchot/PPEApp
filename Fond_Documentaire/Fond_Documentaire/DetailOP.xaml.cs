using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour DetailOP.xaml
    /// </summary>
    public partial class DetailOP : Window
    {
        ObservableCollection<Ouvrage> Afficheur_Ouvrage = new ObservableCollection<Ouvrage> { };
        public DetailOP()
        {
            InitializeComponent();
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            int compte = 0;
            if (connection.IsConnect())
            {
                string sql = "SELECT ISBN as ID, Titre as titre,Auteur as Auteur,Identifiant as Identifiant,`Type de notice` as Type_notice,`Type de document` as Type_doc,Localisation as Localisation,nbExemplaire as nbExemplaire,Contributeur as Contributeur,Editeur as Editeur,`Date` as Datee,`Description` as Descriptionn,Sujet as Sujet,Couverture as Couverture,Langue as Langue,`Format`as Formatt,MyUnknownColumn as MyUnknownColumn FROM fond_doc.ouvrage; ";
                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read() && compte < 30)
                {
                    Afficheur_Ouvrage.Add(new Ouvrage(int.Parse(reader["ID"].ToString()),
                        reader["Titre"].ToString(),
                        reader["Auteur"].ToString(),
                        reader["Identifiant"].ToString(),
                        reader["type_notice"].ToString(),
                        reader["type_doc"].ToString(),
                        reader["Localisation"].ToString(),
                        int.Parse(reader["nbExemplaire"].ToString()),
                        reader["Contributeur"].ToString(),
                        reader["Editeur"].ToString(),
                        int.Parse(reader["Datee"].ToString()),
                        reader["Descriptionn"].ToString(),
                        reader["Sujet"].ToString(),
                        reader["Couverture"].ToString(),
                        reader["Langue"].ToString(),
                        reader["Formatt"].ToString(),
                        reader["MyUnknownColumn"].ToString()));
                    compte++;
                }

                listView.ItemsSource = Afficheur_Ouvrage;
            }
        }
    }
}
