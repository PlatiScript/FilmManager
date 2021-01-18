using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.FirstApp.Models
{
    public class Tag : ObservableObject
    {
        #region Fields
        /// <summary>
        ///     Nom du tag
        /// </summary>
        private string _Name;

        #endregion

        #region Properties
        /// <summary>
        ///     Obtient ou définit le nom du tag
        /// </summary>
        public string Name { get => this._Name; set => this._Name = value; }
        #endregion
      
        #region Constructor
        /// <summary>
        ///     Obtient ou définit le nom du tag
        /// </summary>
        public Tag(string pName)
        {
            this.Name = pName;
        }
        #endregion
    }
}
