using CoursWPF.MVVM;
using FilmsManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.FirstApp.Models
{
    /// <summary>
    ///     Réprésente élement de la collection privé
    /// </summary>
    public class MovieFavorite : ObservableObject
    {
        #region Fields


        /// <summary>
        ///     Film en favori
        /// </summary>
        private Movie _Movie;

        /// <summary>
        ///     Si le film a été vu
        /// </summary>
        private bool _Seen;

        /// <summary>
        ///     Les tags donnés pour le film
        /// </summary>
        private ObservableCollection<Tag> _Tags;

        /// <summary>
        ///     La note donnée au film
        /// </summary>
        private double _Note;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le film
        /// </summary>
        public Movie Movie
        {
            get => this._Movie;
            set => this.SetProperty(nameof(this._Movie), ref this._Movie, value);
        }
        /// <summary>
        ///     Obtient ou définit si le film a été vu
        /// </summary>
        public bool Seen
        {
            get => this._Seen;
            set => this.SetProperty(nameof(this._Seen), ref this._Seen, value);
        }

        /// <summary>
        ///     Obtient ou définit les tags du film
        /// </summary>
        public ObservableCollection<Tag> Tags
        {
            get => this._Tags;
            set => this.SetProperty(nameof(this._Tags), ref this._Tags, value);
        }

        /// <summary>
        ///     Obtient ou définit la note donnée au film
        /// </summary>
        public double Note
        {
            get => this._Note;
            set => this.SetProperty(nameof(this._Note), ref this._Note, value);
        }

        #endregion

        #region Constructor
        public MovieFavorite(Movie m)
        {
            this.Movie = m;
            this.Tags = new ObservableCollection<Tag>();

        }
        #endregion

    }
}
