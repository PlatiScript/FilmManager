using CoursWPF.MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.FirstApp.Models
{
    /// <summary>
    ///     Jeux de données.
    /// </summary>
    public class DataStore : IDataStore
    {
        #region Fields

        /// <summary>
        ///     Chemin du fichier de données.
        /// </summary>
        private string _FilePath;


        /// <summary>
        ///     Liste des films de la collection privée.
        /// </summary>
        private readonly ObservableCollection<MovieFavorite> _Collection;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la liste des films de la collection privée.
        /// </summary>
        public ObservableCollection<MovieFavorite> Collection => this._Collection;

        /// <summary>
        ///     Obtient le chemin du fichier de données.
        /// </summary>
        public string FilePath { get => this._FilePath; private set => this._FilePath = value; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="DataStore"/>.
        /// </summary>
        /// <param name="filePath">Chemin du fichier de données.</param>
        public DataStore(string filePath)
        {
            this._FilePath = filePath;
            this._Collection = new ObservableCollection<MovieFavorite>();

        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sauvegarde le jeux de données dans un fichier.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(this.FilePath, JsonConvert.SerializeObject(this));
        }

        /// <summary>
        ///     Charge le jeux de données depuis le chemin spécifié ou retourne un jeux de données vide.
        /// </summary>
        /// <typeparam name="T">Type du jeux de données.</typeparam>
        /// <param name="filePath">Chemin du fichier de données.</param>
        /// <param name="defaultContext">Instance à utiliser si le chemin ne peut pas être ouvert.</param>
        /// <returns>Instance du jeux de données.</returns>
        public static DataStore Load(string filePath)
        {
            DataStore dataContext;

            try
            {
                dataContext = JsonConvert.DeserializeObject<DataStore>(File.ReadAllText(filePath));
            }
            catch
            {
                dataContext = new DataStore(filePath);
            }

            dataContext.FilePath = filePath;

            return dataContext;
        }

        #endregion
    }
}
