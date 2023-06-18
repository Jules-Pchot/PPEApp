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
    /// Logique d'interaction pour LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private ObservableCollection<Login> Afficheur_Login = new ObservableCollection<Login> { };

        public LogWindow()
        {
            InitializeComponent();
            DBConnection connection = new DBConnection();
            connection = DBConnection.Connection_auto();
            if (connection.IsConnect())
            {
                string sql = "SELECT ID, `Date` as DATE ,Heure, LeUser FROM login";

                var cmd = new MySqlCommand(sql, connection.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Afficheur_Login.Add(new Login(int.Parse(reader["ID"].ToString()), DateTime.Parse(reader["DATE"].ToString()), reader["Heure"].ToString(), reader["LeUser"].ToString()));
                }

                listlog.ItemsSource = Afficheur_Login;
            }
        }
    }
}
