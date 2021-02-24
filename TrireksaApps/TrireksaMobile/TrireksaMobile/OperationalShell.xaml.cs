﻿using Accounts;
using System;
using System.Collections.Generic;
using TrireksaMobile.ViewModels;
using TrireksaMobile.Views;
using Xamarin.Forms;

namespace TrireksaMobile
{
    public partial class OperationalShell : Xamarin.Forms.Shell
    {
        public OperationalShell()
        {
            InitializeComponent();
            var data =Account.GetProfile().Result;
            user.Text = data.Name;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
