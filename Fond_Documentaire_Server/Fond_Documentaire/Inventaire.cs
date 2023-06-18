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
using QRCoder;
using System.IO;

namespace Fond_Documentaire
{
    class Inventaire
    {
        private int _ISBN;
        private string _Etat;
        private string _Date_Modiff;
        private string _Qr;

        public int ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }
        public string Etat
        {
            get { return _Etat; }
            set { _Etat = value; }
        }
        public string Date_Modiff
        {
            get { return _Date_Modiff; }
            set { _Date_Modiff = value; }
        }
        public string Qr
        {
            get { return _Qr; }
            set { _Qr = value; }
        }
        public static void Qr_Generator(Inventaire UnOuvrage)
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            String QRBrut = "@ " + UnOuvrage.Qr;
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRBrut, QRCodeGenerator.ECCLevel.Q);

            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20);

            StreamWriter monStreamWriter = new StreamWriter(@"QrLivre.html");//Necessite using System.IO;

            String strImage = " <img src = \"data:image/png;base64," + qrCodeImageAsBase64 + "\">";
            monStreamWriter.WriteLine("<html>");
            monStreamWriter.WriteLine("<body>");

            String temptext = "<P>" + UnOuvrage.ISBN + "</P>";
            monStreamWriter.WriteLine(temptext);


            monStreamWriter.WriteLine(strImage);    //Ecriture de l'image base 64 dans le fichier
            monStreamWriter.WriteLine("</body>");
            monStreamWriter.WriteLine("</html>");

            // Fermeture du StreamWriter (Très important) 
            monStreamWriter.Close();
            Console.WriteLine("Le QRCode est généré. Appuyer sur une touche pour continuer");
            Console.ReadKey();
        }

        public static void CreationDir()
        {
            // Specify the directory you want to manipulate.
            string path = @"C:\Users\jules\source\repos\Jules-Pchot\PPEApp\ProjetVallade";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("Il existe déjà.");
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
    }
}
