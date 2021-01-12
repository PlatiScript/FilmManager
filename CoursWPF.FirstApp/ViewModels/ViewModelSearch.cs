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

    public class ViewModelSearch : ViewModelList<Movie>, IViewModelSearch
    {

        #region Fields
        /// <summary>
        ///     Fonction pour lancer la recherche
        /// </summary
        private RelayCommand _Search;

        /// <summary>
        ///     Fonction pour ajouter un film dans la collection
        /// </summary
        private RelayCommand _AddFavorite;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la fonction de recherche
        /// </summary
        public RelayCommand Search => this._Search;

        /// <summary>
        ///    Obtient la fonction d'ajout aux favoris
        /// </summary
        public RelayCommand AddFavorite => this._AddFavorite;

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
            Search searchResult = new Search();
            this.ItemsSource.Clear();

            //Création du requête HTTP
            using (HttpClient client = new HttpClient())
            {

                //Définition des paramètres de la requête HTTP (url, header,...)
                Uri url = new Uri("http://www.omdbapi.com/?apikey=10036dbf&s=" + param);
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync(url).Result;
                bool isSuccessStatusCode = response.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                {
                    //Récupération des donnnées JSON pour les caster dans l'object Search
                    var dataobj = response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject(dataobj.Result);
                    searchResult = JsonConvert.DeserializeObject<Search>(dataobj.Result);
                    if (searchResult.TotalResults > 0)
                    {
                        //Récupération des films de l'objet Search puis ajout dans l'itemsource
                        foreach (Movie m in searchResult.Movies)
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
        protected override Movie CreateInstance(object param) => new Movie();

        #endregion
    }
}
