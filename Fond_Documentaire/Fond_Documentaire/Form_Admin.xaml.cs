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
    /// Logique d'interaction pour Form_Admin.xaml
    /// </summary>
    public partial class Form_Admin : Window
    {
        private ObservableCollection<User> Afficheur_User = new ObservableCollection<User> { };

        public Form_Admin()
        {
            InitializeComponent();
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();


            if (connection.IsConnect())
            {
                string sql = "SELECT id as ID, pseudo as PSEUDO,mdp as MDP,id_role AS ID_ROLE FROM user";

                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Afficheur_User.Add(new User(int.Parse(reader["ID"].ToString()),reader["pseudo"].ToString(), reader["mdp"].ToString(), int.Parse(reader["ID_ROLE"].ToString())));
                }

                _listView.ItemsSource = Afficheur_User;

            }

           
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            int index = Afficheur_User.IndexOf((User)_listView.SelectedItem);
            User oldUser = Afficheur_User[index];
            Afficheur_User[index] = new User(oldUser.ID, TextPseudo.Text, oldUser.MDP, int.Parse(ID_ROLEbox.Text)) ;
            Afficheur_User[index].Modify(connection, TextPseudo.Text, int.Parse(ID_ROLEbox.Text),oldUser.ID);
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            int index = Afficheur_User.IndexOf((User)_listView.SelectedItem);
            User oldUser = Afficheur_User[index];
            Afficheur_User[index].Modify(connection, TextPseudo.Text, int.Parse(ID_ROLEbox.Text), oldUser.ID);
            Afficheur_User.Remove(Afficheur_User[index]);

        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            string newPseudo = TextPseudo.Text;
            string newMDP = SHA.GenerateSHA512String(TextMDP.Text);
            int idRole = int.Parse(ID_ROLEbox.Text);
            User newUser = new User(newPseudo, newMDP, idRole);
            newUser.CreateUser(connection,newPseudo, newMDP, idRole);
            Afficheur_User.Add(newUser);
        }
    }
}
