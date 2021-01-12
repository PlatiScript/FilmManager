using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CoursWPF.FirstApp.ViewModels
{
    /// <summary>
    ///     ViewModel pour gérer l'application.
    /// </summary>
    public class ViewModelMain : ObservableObject, IViewModelMain
    {
        #region Fields

        /// <summary>
        ///     ViewModel de la liste des personnes.
        /// </summary>
        private readonly IViewModelCollection _ViewModelCollection;

        /// <summary>
        ///     ViewModel de la liste des véhicules.
        /// </summary>
        private readonly IViewModelSearch _ViewModelSearch;

        /// <summary>
        ///     Commande pour sauvegarder les données.
        /// </summary>
        private readonly RelayCommand _Save;

        /// <summary>
        ///     Commande pour quitter l'application.
        /// </summary>
        private readonly RelayCommand _Exit;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le ViewModel de la liste des personnes.
        /// </summary>
        public IViewModelCollection ViewModelCollection => this._ViewModelCollection;

        /// <summary>
        ///     Obtient le ViewModel de la liste des véhicules.
        /// </summary>

        public IViewModelSearch ViewModelSearch => this._ViewModelSearch;



        /// <summary>
        ///     Obtient la commande pour sauvegarder les donnnées.
        /// </summary>
        public RelayCommand Save => this._Save;

        /// <summary>
        ///     Obtient la commande pour quitter l'application.
        /// </summary>
        public RelayCommand Exit => this._Exit;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelMain"/>.
        /// </summary>
        public ViewModelMain()
        {
            this._ViewModelCollection = App.ServiceProvider.GetService<IViewModelCollection>();
            this._ViewModelSearch = App.ServiceProvider.GetService<IViewModelSearch>();
            this._Save = new RelayCommand(this.ExecuteSave, this.CanExecuteSave);
            this._Exit = new RelayCommand(this.ExecuteExit, this.CanExecuteExit);
        }

        #endregion

        #region Methods

        #region Save

        /// <summary>
        ///     Test si la commande <see cref="Save"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteSave(object param) => true;

        /// <summary>
        ///     Exécute la commande <see cref="Save"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteSave(object param)
        {
            App.ServiceProvider.GetService<IDataStore>().Save();
        }

        #endregion

        #region Exit

        /// <summary>
        ///     Test si la commande <see cref="Exit"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteExit(object param) => true;

        /// <summary>
        ///     Exécute la commande <see cref="Exit"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteExit(object param) => Environment.Exit(0);

        #endregion

        #endregion
    }
}
