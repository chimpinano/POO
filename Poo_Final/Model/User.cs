using Poo_Final.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poo_Final.Model
{
    public class User : BaseViewModel
    {
        private string id;
        private string username;
        private string image;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                this.RaisePropertyChanged("Id");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                this.RaisePropertyChanged("Username");
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                this.RaisePropertyChanged("Image");
            }
        }

        
    }
}
