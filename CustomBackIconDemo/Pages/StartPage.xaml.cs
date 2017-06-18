using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CustomBackIconDemo.Pages
{
    public partial class StartPage : ContentPage 
    {
		public StartPage()
        {
			InitializeComponent();
		}

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage(9)); // Here we specify to hide the backbutton
        }
    }
}
