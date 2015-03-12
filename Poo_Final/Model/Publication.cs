using Poo_Final.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poo_Final.Model
{
    public class Publication : BaseViewModel
    {
        private string id;
        private string caption;
        private string image;
        private User user;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                this.RaisePropertyChanged("Id");
            }
        }

        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                this.RaisePropertyChanged("Caption");
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

        public User User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
