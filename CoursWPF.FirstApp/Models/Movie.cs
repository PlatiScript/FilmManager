using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsManager.Models
{

    /// <summary>
    ///     Représente un film
    /// </summary>
    public class Movie : ObservableObject
    {
        #region Fields
        /// <summary>
        ///     Titre du fim
        /// </summary>
        private string _Title;


        /// <summary>
        ///     Année de sortie du film
        /// </summary>
        private string _Year;

        /// <summary>
        ///     ID du film dans l'API imbd
        /// </summary>
        private string _ImdbID;

        /// <summary>
        ///     Type du film
        /// </summary>
        private string _Type;

        /// <summary>
        ///    URL de l'affiche du film
        /// </summary>
        private Uri _Poster;


        #endregion

        #region Properties
        /// <summary>
        ///     Obtient ou définit le titre du film
        /// </summary>
        public string Title { get => this._Title; set => this._Title = value; }

        /// <summary>
        ///     Obtient ou définit l'annnée du film
        /// </summary>
        public string Year { get => this._Year; set => this._Year = value; }

        /// <summary>
        ///     Obtient ou définit l'ID de l'API Imbd.
        /// </summary>
        public string ImdbID { get => this._ImdbID; set => this._ImdbID = value; }

        /// <summary>
        ///     Obtient ou définit le type du film
        /// </summary>
        public string Type { get => this._Type; set => this._Type = value; }

        /// <summary>
        ///     Obtient ou définit l'URL de l'affiche du film
        /// </summary>
        public Uri Poster { get => this._Poster; set => this._Poster = value; }
        #endregion

    }
}
