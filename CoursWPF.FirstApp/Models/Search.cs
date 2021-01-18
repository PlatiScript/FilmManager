using CoursWPF.MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsManager.Models
{
    /// <summary>
    ///     Réprésente le résultat de la recherche sur l'API Imbd
    /// </summary>
    public class Search : ObservableObject
    {
        #region Fields

        /// <summary>
        ///      Liste des films retournés
        /// </summary>
        [JsonProperty("Search")]
        private List<Movie> _Movies;

        /// <summary>
        ///     Nombre de films retournés
        /// </summary>
        [JsonProperty("totalResults")]
        private int _TotalResults;

        /// <summary>
        ///    L'état de la requête
        /// </summary>
        [JsonProperty("Response")]
        private Boolean _Success;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit les films retournés
        /// </summary>
        public List<Movie> Movies { get => this._Movies; set => this._Movies = value; }

        /// <summary>
        ///     Obtient ou définit le nombre de films retournés
        /// </summary>
        public int TotalResults { get => this._TotalResults; set => this._TotalResults = value; }

        /// <summary>
        ///     Obtient ou définit l'état de la requête
        /// </summary>
        public Boolean Success { get => this._Success; set => this._Success = value; }

        #endregion

    }
}
