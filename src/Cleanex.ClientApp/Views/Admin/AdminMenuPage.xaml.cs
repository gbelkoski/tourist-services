﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class AdminMenuPage : ContentPage
{
    public AdminMenuPage()
	{
		InitializeComponent();
    }

    async void btnManageCustomers_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//adminmenu//managecustomers");
    }

    async void btnManageItems_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//adminmenu//manageitems");
    }

    async void btnManageSettings_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//adminmenu//managesettings");
    }
}
