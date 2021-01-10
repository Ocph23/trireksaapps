﻿using System;
using System.Collections.Generic;
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

namespace TrireksaApp.Contents.Users
{
    /// <summary>
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : UserControl
    {
        public AddEditUser()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as AddEditViewModel;
            context.Password = ((PasswordBox)sender).Password;
        }

        private void Confirmpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as AddEditViewModel;
            context.ConfirmPassword = ((PasswordBox)sender).Password;
        }
    }
}
