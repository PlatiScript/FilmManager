using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace CoursWPF.FirstApp.ViewModels
{
    /// <summary>
    ///     ViewModel pour gérer une liste de <see cref="MovieFavorite"/>.
    /// </summary>
    public class ViewModelCollection : ViewModelList<MovieFavorite>, IViewModelCollection
    {
        #region Fields

        /// <summary>
        ///     Valeur du filtre de la collection
        /// </summary>
        private string filterText;

        /// <summary>
        ///     CollectionView pour le filtrage
        /// </summary>
        private CollectionViewSource moviesCollection;

        /// <summary>
        ///    Event de changement de filtre pour le filtrage
        /// </summary>
        public event PropertyChangedEventHandler PropertyChangedFilter;

        /// <summary>
        ///     Fonction pour ajouter un tag
        /// </summary
        private RelayCommand _AddTag;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelCollection"/>.
        /// </summary>
        public ViewModelCollection()
        {
            this.Title = "Collections";
            this.ItemsSource = App.ServiceProvider.GetService<IDataStore>().Collection;

            moviesCollection = new CollectionViewSource();
            moviesCollection.Source = this.ItemsSource;
            moviesCollection.Filter += usersCollection_Filter;

            this._AddTag = new RelayCommand(this.ExecuteAddTag);



        }
        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la fonction d'ajout de tag
        /// </summary
        public RelayCommand AddTag => this._AddTag;


        /// <summary>
        ///     Obtient ou définit la collection des films favoris
        /// </summary>
        public ICollectionView SourceCollection
        {
            get
            {
                return this.moviesCollection.View;
            }
        }

        /// <summary>
        ///     Obtient ou définit la valeur du filtre
        /// </summary>
        public string FilterText
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                this.moviesCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }

        #endregion

        #region Methods


        /// <summary>
        ///     Filtre  en fonction du nom du film la collection
        /// </summary>
        void usersCollection_Filter(object sender, FilterEventArgs e)
        {

            //Vérification si le filtre est null ou vide
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            //Réalise le filtre en fonction du titre
            MovieFavorite movie = e.Item as MovieFavorite;
            if (movie.Movie.Title.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        /// <summary>
        ///     Déclenche l'évenement de filtrage
        /// </summary>
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChangedFilter != null)
            {
                this.PropertyChangedFilter(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///     Execute la fonction d'ajout de tag
        /// </summary>
        protected virtual void ExecuteAddTag(object param)
        {
            this.SelectedItem.Tags.Add(new Tag(param.ToString()));
        }

        #endregion
    }
}
