using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;

namespace CoursWPF.FirstApp
{
    /// <summary>
    ///     <see cref="MarkupExtension"/> pour permettre l'utilisation du <see cref="App.ServiceProvider"/> depuis le xaml.
    /// </summary>
    class ProvideService : MarkupExtension
    {
        #region Properties

        /// <summary>
        ///     Type du service à récupérer.
        /// </summary>
        public Type ServiceType { get; set; }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider) => App.ServiceProvider.GetService(this.ServiceType);
    }
}
