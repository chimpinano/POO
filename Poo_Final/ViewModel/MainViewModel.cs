using Poo_Final.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poo_Final.Utilities;
using Poo_Final.API;
using System.Threading;

namespace Poo_Final.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private Publication current;
        private string login;
        private string hashtag;
        private string textBtn;
        private bool editHashtag;
        private MainWindow mainWindow;
        private bool visibleLogin;
        private bool visibleSystem;
       
        public ObservableCollection<Publication> Items { get; set; }

        public Publication Current
        {
            get { return current; }
            set { current = value; }
        }

        public string Login
        {
            get { return login; }
            set
            {
                this.RaisePropertyChanged("Login");
                login = value;
            }
        }

        public string Hashtag
        {
            get { return hashtag; }
            set
            {
                this.RaisePropertyChanged("Hashtag");
                hashtag = value;
            }
        }

        public string TextBtn
        {
            get 
            {
                if (editHashtag) return "Salvar";
                else return "Editar";
            }

        }

        public bool VisibleLogin
        {
            get { return visibleLogin; }
            set
            {
                this.RaisePropertyChanged("VisibleLogin");
                visibleLogin = value;
            }
        }

        public bool VisibleSystem
        {
            get { return visibleSystem; }
            set
            {
                this.RaisePropertyChanged("VisibleSystem");
                visibleSystem = value;
            }
        }

        public bool EditHashtag
        {
            get { return editHashtag; }
            set
            {
                this.RaisePropertyChanged("EditHashtag");
                editHashtag = value;
            }
        }

        public MainViewModel(){}
        public MainViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            visibleLogin = true;
        }

        #region Funcionalidades

        public void ExecuteLogin()
        {
            if (String.IsNullOrEmpty(Login)) LoginError();
            else
            {
                var result = Backup.RestoreBackup(Login);
                if (result != null) Items = result.Items;
                else Backup.ExecuteBackup(this);

                VisibleLogin = false;
                VisibleSystem = true;

                Task.Run(() => AlimentarInstagram());
            }
        }

        private void AlimentarInstagram()
        {
            Instagram api = new Instagram();

            while (true)
            {
                try
                {
                    if (!EditHashtag)
                    {
                        var result = api.GetPublications(hashtag);

                        foreach (var item in result)
                        {
                            mainWindow.Dispatcher.Invoke(() =>
                            {
                                Items.Add(item);
                            });
                        }
                        
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception) { Thread.Sleep(20000); }
            }
        }

        private async void LoginError()
        {
            await mainWindow.ShowMessageAsync("Login inválido!", "Verifique os seus dados e tente novamente!");
        }

        #endregion


    }
}
