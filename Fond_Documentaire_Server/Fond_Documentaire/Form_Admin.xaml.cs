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
                string sql = "SELECT id as ID, pseudo as PSEUDO, mdp as MDP, id_role AS ID_ROLE, `EstActiver` AS Est_Activer FROM user";

                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Afficheur_User.Add(new User(int.Parse(reader["ID"].ToString()),reader["pseudo"].ToString(), reader["mdp"].ToString(), int.Parse(reader["ID_ROLE"].ToString()), int.Parse(reader["Est_Activer"].ToString())));
                }

                _listView.ItemsSource = Afficheur_User;
                reader.Close();
                connection.Close();

            }

           
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {
            try
            {
                

                DBConnection connection = new DBConnection("Connection_auto()");
                if (connection.IsConnect())
                {

                    int index = Afficheur_User.IndexOf((User)_listView.SelectedItem);
                    User oldUser = Afficheur_User[index];
                    Console.WriteLine("Pseudo : " + TextPseudo.Text);
                    string nouveauPseudo = TextPseudo.Text;
                    int nouvelID = int.Parse(ID_ROLEbox.Text);
                    Afficheur_User[index] = new User(oldUser.ID, nouveauPseudo, oldUser.MDP, nouvelID,Active());
                    Afficheur_User[index].Modify(connection, nouveauPseudo, nouvelID, oldUser.ID,Active());
                    connection.Close();
                }
            }
            catch (Exception b)
            {

            }
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConnection connection = new DBConnection("Connection_auto()");
                if (connection.IsConnect())
                {
                    int index = Afficheur_User.IndexOf((User)_listView.SelectedItem);
                    User oldUser = Afficheur_User[index];
                    Afficheur_User[index].Suppr(connection, oldUser.ID);
                    Afficheur_User.Remove(Afficheur_User[index]);
                    connection.Close();
                }
            }catch(Exception b)
            {

            }
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            try
            {
                
                DBConnection connection = new DBConnection("Connection_auto()");
                if (connection.IsConnect())
                {
                    string newPseudo = TextPseudo.Text;
                    string newMDP = SHA.GenerateSHA512String(TextMDP.Text);
                    int idRole = int.Parse(ID_ROLEbox.Text);
                    User newUser = new User(newPseudo, newMDP, idRole, Active());
                    newUser.CreateUser(connection, newPseudo, newMDP, idRole, Active());
                    Afficheur_User.Add(newUser);
                    connection.Close();
                }
            }
            catch (Exception b)
            {

            }
        }
        private int Active() {
            if (Check_Active.IsChecked == true)
            {
                return 1;
            }
            else return 0;
        }
        private bool Checked()
        {
            if (Active() == 1)
            {
                return true;
            }
            else return false;
        }

        private void OpenLog(object sender, RoutedEventArgs e)
        {
            LogWindow DetailForm = new LogWindow();
            DetailForm.Show();
        }
    }
}
