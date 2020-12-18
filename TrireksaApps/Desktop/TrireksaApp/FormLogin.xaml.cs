using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrireksaApp.Common;

namespace TrireksaApp
{
    /// <summary>
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin :ModernWindow
    {
        private FormLoginVM viewmodel;

        public FormLogin()
        {
            InitializeComponent();
            
            Style = (Style)App.Current.Resources["BlankWindow"];
           this.viewmodel = new FormLoginVM() { CloseWindow = Close };
            this.DataContext = viewmodel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var obj = (PasswordBox)sender;
            viewmodel.Password = obj.Password;

        }
        
    }

    public class FormLoginVM : NotifyPropertyChanged, IDataErrorInfo
    {
        private string _username;
        private string _password;
        private string _message;
        private bool _isActive;

        public bool ProgressIsActive
        {
            get { return _isActive; }
            set
            {

                _isActive = value;
                OnPropertyChanged("ProgressIsActive");
            }
        }

        public FormLoginVM()
        {
            Login = new CommandHandler { CanExecuteAction = LoginValidate, ExecuteAction = LoginAction };
            Close = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CloseAction() };
            var config = new Common.AppConfiguration();

            UserName = config.GetUserName();
            //Password = "Sony@77";
        }

        private bool LoginValidate(object obj)
        {
            if (UserName != string.Empty && Password != string.Empty && !ProgressIsActive)
            {
                return true;
            }
            else
                return false;
        }

        private void CloseAction()
        {
            CloseWindow();
        }

        private async void LoginAction(object obj)
        {
            ProgressIsActive = true;
            try
            {
                var strcontent = new { UserName = UserName, Password = Password };
                using (var client = new Client())
                {
                    var response = await client.ClientContext.PostAsync("api/user/login", client.GetContent(strcontent));
                    if (response.IsSuccessStatusCode)
                    {
                        this.Token = JsonConvert.DeserializeObject<AuthenticationToken>(response.Content.ReadAsStringAsync().Result);
                        if (Token.Token != null)
                        {
                            ResourcesBase.Token = Token;
                            ResourcesBase.UserIsLogin = new ModelsShared.Models.Userprofile { Email = this.UserName };
                            var form = new MainWindow();
                            form.Show();
                            this.CloseWindow();
                        }
                        else
                        {

                            throw new SystemException("User Or Password Invalid !..");
                        }

                    }
                    else
                    {
                        throw new SystemException("You Not Connect to Server");
                    }
                }
            }
            catch (Exception ex)
            {

                this.Message = ex.Message;
            }


            //try
            //{
            //    var MainVM = new UserPorfileCollection();
            //    if (await MainVM.Login(UserName, Password))
            //    {
            //        ResourcesBase.UserIsLogin = new ModelsShared.Models.userprofile { Email = this.UserName };
            //        var form = new MainWindow();
            //        form.Show();
            //        this.CloseWindow();
            //    }
            //    else
            //    {
            //        this.Message = "You Not Connect to Server";
            //    }
            //}
            //catch (Exception)
            //{

            //    this.Message = "You Not Connect to Server";
            //}

            ProgressIsActive = false;



        }


        public CommandHandler Login { get; set; }
        public CommandHandler Close { get; set; }

        public Action CloseWindow { get; set; }

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                Message = string.Empty;
                OnPropertyChanged("UserName");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                Message = string.Empty;
                OnPropertyChanged("Password");
            }
        }

        public AuthenticationToken Token { get; private set; }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "UserName")
                {
                    return string.IsNullOrEmpty(this._username) ? "Required value" : null;
                }
                if (columnName == "Password")
                {
                    return string.IsNullOrEmpty(this._password) ? "Required value" : null;
                }
                return null;
            }
        }


    }
}
