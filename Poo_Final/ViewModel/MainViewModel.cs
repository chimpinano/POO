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
        private string login;
        private string hashtag;
        private bool editHashtag;
        private MainWindow mainWindow;
        private bool visibleLogin;
        private bool visibleSystem;

        private ObservableCollection<Publication> items;
        public ObservableCollection<Publication> Items
        {
            get { return items; }
            set
            {
                this.RaisePropertyChanged("Items");
                items = value;
            }
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
                else return "Editar Tag";
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

        public bool EditHashtag
        {
            get { return editHashtag; }
            set
            {
                this.RaisePropertyChanged("EditHashtag");
                this.RaisePropertyChanged("TextBtn");
                editHashtag = value;
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

        public MainViewModel(){}

        public MainViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Items = new ObservableCollection<Publication>();
            visibleLogin = true;
            EditHashtag = false;
            Hashtag = "";
        }

        #region Funcionalidades

        public void ExecuteLogin()
        {
            if (String.IsNullOrEmpty(Login)) Mensage("Login inválido!", "Verifique os seus dados e tente novamente!");
            else
            {
                VisibleLogin = false;
                VisibleLogin = false;
                VisibleSystem = true;
                VisibleSystem = true;

                var result = Backup.RestoreBackup(Login);
                if (result != null)
                {
                    foreach (var item in result.Items)
                    {
                        Items.Add(item);
                    }

                    Hashtag = result.Hashtag;
                }
                else ExecuteBackup();

                Mensage
                   ("Seja bem vindo!",
                    "Veja já o que está rolando no Instagram, pressione o butão " +
                    "de editar tag e defina a tag a ser buscada. \n \nDica: Defina a " + 
                    "tag como a palavra 'LOVE', ela é a tag mais utilizada no Instagram!");

                Task.Run(() => AlimentarInstagram());
            }
        }

        public void ExecuteBackup()
        {
            try
            {
                Backup.ExecuteBackup(this);
            }
            catch (Exception)
            { Mensage("Erro na operação", "Tente novamente realizar o backup"); }
        }

        private void AlimentarInstagram()
        {
            Instagram api = new Instagram();

            while (true)
            {
                try
                {
                    if (EditHashtag && !String.IsNullOrEmpty(hashtag))
                    {
                        var result = api.GetPublications(hashtag.Trim());

                        foreach (var item in result)
                        {
                            mainWindow.Dispatcher.Invoke(() =>
                            {
                                if(Items.Count(i=> i.Id.Equals(item.Id)) == 0)
                                    Items.Insert(0, item);
                            });
                        }
                        
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception) { Thread.Sleep(20000); }
            }
        }

        internal void EditTag()
        {
            EditHashtag = (EditHashtag) ? false : true;
        }

        private async void Mensage(string a, string b)
        {
            await mainWindow.ShowMessageAsync(a, b);
        }

        #endregion
    }
}
