using Poo_Final.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poo_Final.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string login;

        public string Login
        {
            get { return login; }
            set
            {
                this.RaisePropertyChanged("Login");
                login = value;
            }
        }

        public ObservableCollection<Publication> Items;

        private Publication atual;

        public Publication Atual
        {
            get { return atual; }
            set { atual = value; }
        }
        

    }
}
