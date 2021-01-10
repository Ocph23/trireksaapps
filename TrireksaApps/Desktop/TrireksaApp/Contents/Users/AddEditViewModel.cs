using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Users
{
    public class AddEditViewModel : ViewModelBase
    {

        public AddEditViewModel()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidate, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction =x=> true, ExecuteAction = x=>WindowClose() };
        }

        private void SaveAction(object obj)
        {
            ModelValid = true;
            WindowClose();
        }

        private bool SaveValidate(object obj)
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrEmpty(UserName))
            {
                Message = "Lengkapi Data !";
                return false;
            }
            if (Password != ConfirmPassword)
            {
                Message = "Password dan Conform Password Harus Sama !";
                return false;
            }
            return true;
        }

        private string email;

        public string Email
        {
            get => email; 
            set => SetProperty(ref email , value); 
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }


        private string message;

        public string Message
        {
            get { return message; }
            set {SetProperty(ref message , value); }
        }


        public CommandHandler SaveCommand { get; }
        public CommandHandler CancelCommand { get; }
        public Action WindowClose { get; internal set; }
        public bool ModelValid { get; private set; }
    }
}
