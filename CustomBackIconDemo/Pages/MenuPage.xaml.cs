using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CustomBackIconDemo.Pages
{
    public partial class MenuPage : ContentPage, INavigationActionBarConfig
    {
		public int BackButtonStyle { get; set; } // implementing INavigationActionBarConfig

		public MenuPage()
        {
            InitializeComponent();
        }

		public MenuPage(int backButtonStyle) : this()
        {
			BackButtonStyle = backButtonStyle; // see INavigationActionBarConfig for possible values
		}

		async void Handle_ClickedAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage());
		}

        async void Handle_DefaultAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ResultPage(0));
        }

		async void Handle_HideAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(1));
		}

		async void Handle_ImageAndTextAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(2));
		}

		async void Handle_ImageOnlyAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(3));
		}

		async void Handle_TextOnlyAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(4));
		}

		async void Handle_SystemIconAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(5));
		}

		async void Handle_ImgIconTextAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(6));
		}

		async void Handle_HideBrandAsync(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ResultPage(7));
		}
    }
}
