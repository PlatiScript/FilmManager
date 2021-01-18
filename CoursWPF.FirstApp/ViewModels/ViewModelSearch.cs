using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmsManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels
{

    public class ViewModelSearch : ObservableObject, IViewModelSearch
    {

        #region Fields

        /// <summary>
        ///     Titre du ViewModel;
        /// </summary>
        private string _Title;

        /// <summary>
        ///     Source de données de la liste.
        /// </summary>
        private ObservableCollection<Movie> _ItemsSource;

        /// <summary>
        ///     Élément sélectionné de la liste.
        /// </summary>
        private Movie _SelectedItem;

        /// <summary>
        ///     Fonction pour lancer la recherche
        /// </summary
        private RelayCommand _Search;

        /// <summary>
        ///     Fonction pour ajouter un film dans la collection
        /// </summary
        private RelayCommand _AddFavorite;

        /// <summary>
        ///     Fonction pour aller à la page précédente
        /// </summary
        private RelayCommand _PreviousPage;

        /// <summary>
        ///     Fonction pour aller à la page Suivante
        /// </summary
        private RelayCommand _NextPage;

        /// <summary>
        ///     Numéro de page active dans la pagination 
        /// </summary
        private int _CurrentPage = 1;

        /// <summary>
        ///     Nombre de page de la recherche actuelle
        /// </summary
        private int _CurrentPagesNumber;

        /// <summary>
        ///     Recherche en cours
        /// </summary
        private Search _CurrentSearch;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le titre du ViewModel.
        /// </summary>
        public string Title
        {
            get => this._Title;
            set => this.SetProperty(nameof(this.Title), ref this._Title, value);
        }

        /// <summary>
        ///     Obtient la source de données de la liste.
        /// </summary>
        public ObservableCollection<Movie> ItemsSource
        {
            get => this._ItemsSource;
            protected set => this.SetProperty(nameof(this.ItemsSource), ref this._ItemsSource, value);
        }

        /// <summary>
        ///     Obtient ou définit l'élément sélectionné de la liste.
        /// </summary>
        public Movie SelectedItem
        {
            get => this._SelectedItem;
            set => this.SetProperty(nameof(this.SelectedItem), ref this._SelectedItem, value);
        }
        /// <summary>
        ///     Obtient la fonction de recherche
        /// </summary
        public RelayCommand Search => this._Search;

        /// <summary>
        ///    Obtient la fonction d'ajout aux favoris
        /// </summary
        public RelayCommand AddFavorite => this._AddFavorite;

        /// <summary>
        ///     Obtient la fonction de page précente
        /// </summary
        public RelayCommand PreviousPage => this._PreviousPage;

        /// <summary>
        ///     Obtient la fonction de page suivante
        /// </summary
        public RelayCommand NextPage => this._NextPage;

        /// <summary>
        ///     Obtient le numero de page active de la pagination
        /// </summary
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set => this.SetProperty(nameof(this.CurrentPage), ref this._CurrentPage, value);
        }

        /// <summary>
        ///     Obtient ou défini le nombre de page de la recherche en cours
        /// </summary
        public int CurrentPagesNumber
        {
            get { return _CurrentPagesNumber; }
            set => this.SetProperty(nameof(this.CurrentPagesNumber), ref this._CurrentPagesNumber, value);

        }

        /// <summary>
        ///     Obtient ou défini la recherche en cours
        /// </summary
        public Search CurrentSearch {

            get { return _CurrentSearch; }
            set => this.SetProperty(nameof(this.CurrentSearch), ref this._CurrentSearch, value);

        }
        #endregion

        #region Constructor

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelSearch"/>.
        /// </summary>
        public ViewModelSearch()
        {
            this.Title = "Recherche";
            this.ItemsSource = new ObservableCollection<Movie>();
            this._Search = new RelayCommand(this.ExecuteSearch);
            this._AddFavorite = new RelayCommand(this.ExecuteAddFavorite, this.canExecuteAddFavorite);
            this._PreviousPage = new RelayCommand(this.ExecutePreviousPage, this.canExecutePreviousPage);
            this._NextPage = new RelayCommand(this.ExecuteNextPage, this.canExecuteNextPage);

        }

        #endregion

        #region Methods

        /// <summary>
        ///     Réalise une recherche sur l'API d'imbd
        ///     <param name="param">Titre du film recherché</param>
        /// </summary>
        protected virtual void ExecuteSearch(object param)
        {
            object result;
            this.ItemsSource.Clear();

            //Création du requête HTTP
            using (HttpClient client = new HttpClient())
            {

                //Définition des paramètres de la requête HTTP (url, header,...)
                Uri url = new Uri("http://www.omdbapi.com/?apikey=10036dbf&s=" + param+"&page="+this.CurrentPage);
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync(url).Result;
                bool isSuccessStatusCode = response.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                {
                    //Récupération des donnnées JSON pour les caster dans l'object Search
                    var dataobj = response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject(dataobj.Result);
                    this.CurrentSearch = JsonConvert.DeserializeObject<Search>(dataobj.Result);
                    this.CurrentPagesNumber =(this.CurrentSearch.TotalResults - (this.CurrentSearch.TotalResults % 10)) / 10 + 1;
                    if (this.CurrentSearch.TotalResults > 0)
                    {
                        //Récupération des films de l'objet Search puis ajout dans l'itemsource
                        foreach (Movie m in this.CurrentSearch.Movies)
                        {
                            this.ItemsSource.Add(m);
                        }
                    }
                }
                //En cas d'erreur
                else if (Convert.ToString(response.StatusCode) != "InternalServerError")
                {
                    result = JsonConvert.DeserializeObject("{ \"APIMessage\":\"" + response.ReasonPhrase + "\" }");
                }
                else
                {
                    result = JsonConvert.DeserializeObject("{ \"APIMessage\":\"InternalServerError\" }");
                }

            }


        }

        /// <summary>
        ///     Execute la fonction de page suivante
        /// </summary>
        protected virtual void ExecuteNextPage(object param)
        {
            this.CurrentPage++;
            this.ExecuteSearch(param);
        }
        /// <summary>
        ///     Execute la fonction de page précédente
        /// </summary>
        protected virtual void ExecutePreviousPage(object param)
        {
            this.CurrentPage--;
            this.ExecuteSearch(param);

        }
        /// <summary>
        ///     Vérifie qu'il y a une page précédente
        /// </summary>
        protected virtual bool canExecutePreviousPage(object param) => this.CurrentPage > 1;

        /// <summary>
        ///     Vérifie qu'il y a une page suivante
        /// </summary>
        protected virtual bool canExecuteNextPage(object param) => this.CurrentPage < this.CurrentPagesNumber;
        /// <summary>
        ///     Execute la fonction d'ajout d'un film dans la collection
        /// </summary>
        protected virtual void ExecuteAddFavorite(object param) => App.ServiceProvider.GetService<IDataStore>().Collection.Add(new MovieFavorite(SelectedItem));

        /// <summary>
        ///     Vérifie que le film n'est pas déjà dans la collection
        /// </summary>
        protected virtual bool canExecuteAddFavorite(object param) => App.ServiceProvider.GetService<IDataStore>().Collection.Where(m => m.Movie?.ImdbID == SelectedItem?.ImdbID).ToList().Count <= 0;

        /// <summary>
        ///     Créer une instance de la classe <see cref="Movie"/>.
        /// </summary>

        #endregion

    }
}
