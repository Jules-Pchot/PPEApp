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
    class Ouvrage
    {
        private int _ISBN;
        private string _Titre;
        private string _Auteur;
        private string _Identifiant;
        private string _Type_notice;
        private string _Type_doc;
        private string _Localisation;
        private int _nbExemplaire;
        private string _Contributeur;
        private string _Editeur;
        private int _Date;
        private string _Description;
        private string _Sujet;
        private string _Couverture;
        private string _Langue;
        private string _Format;
        private string _UnknowColumn;

        [XmlElement()]
        public int ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }
        [XmlElement()]
        public string Titre
        {
            get { return _Titre; }
            set { _Titre = value; }
        }
        [XmlElement()]
        public string Auteur
        {
            get { return _Auteur; }
            set { _Auteur = value; }
        }
            [XmlElement()]
            public string identifiant
        {
            get { return _Identifiant; }
            set { _Identifiant = value; }
        }
        [XmlElement()]
        public string Type_notice
        {
            get { return _Type_notice; }
            set { _Type_notice = value; }
        }
        [XmlElement()]
        public string Type_doc
        {
            get { return _Type_doc; }
            set { _Type_doc = value; }
        }
        [XmlElement()]
        public string Localisation
        {
            get { return _Localisation; }
            set { _Localisation = value; }
        }
        [XmlElement()]
        public int nbExemplaire
        {
            get { return _nbExemplaire; }
            set { _nbExemplaire = value; }
        }
        [XmlElement()]
        public string Contributeur
        {
            get { return _Contributeur; }
            set { _Contributeur = value; }
        }
        [XmlElement()]
        public string Editeur
        {
            get { return _Editeur; }
            set { _Editeur = value; }
        }
        [XmlElement()]
        public int Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        [XmlElement()]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [XmlElement()]
        public string Sujet
        {
            get { return _Sujet; }
            set { _Sujet = value; }
        }
        [XmlElement()]
        public string Couverture
        {
            get { return _Couverture; }
            set { _Couverture = value; }
        }
        [XmlElement()]
        public string Langue
        {
            get { return _Langue; }
            set { _Langue = value; }
        }
        [XmlElement()]
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }
        [XmlElement()]
        public string UnknowColumn
        {
            get { return _UnknowColumn; }
            set { _UnknowColumn = value; }
        }
       
        public Ouvrage(int ID, string titre, string auteur)
        {
            _ISBN = ID;
            _Titre = titre;
            _Auteur = auteur;
        }
        public Ouvrage (int ID, string titre, string auteur, string identifiant, string type_notice, string type_doc, string localisation, int nbexemplaire, string contributeur,
          string editeur, int date, string description, string sujet, string couverture, string langue, string format, string unknowncolumn)
        {
          _ISBN = ID ;
          _Titre = titre;
          _Auteur = auteur;
         _Identifiant = identifiant;
         _Type_notice = type_notice;
         _Type_doc = type_doc;
         _Localisation = localisation;
         _nbExemplaire = nbexemplaire;
         _Contributeur = contributeur;
         _Editeur = editeur;
         _Date = date;
         _Description = description;
         _Sujet = sujet;
         _Couverture = couverture;
         _Langue = langue;
         _Format = format;
         _UnknowColumn = unknowncolumn;
    }
}
}
