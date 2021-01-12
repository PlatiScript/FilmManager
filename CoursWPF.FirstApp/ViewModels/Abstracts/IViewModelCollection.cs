using CoursWPF.FirstApp.Models;
using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface d'un ViewModel pour gérer une liste de <see cref="MovieFavorite"/>.
    /// </summary>
    public interface IViewModelCollection : IViewModelList<MovieFavorite>
    {

    }
}
