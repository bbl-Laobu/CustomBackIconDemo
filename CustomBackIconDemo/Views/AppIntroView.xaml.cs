using Xamarin.Forms;
using CustomBackIconDemo.Pages;

namespace CustomBackIconDemo.Views
{
    public partial class AppIntroView : ContentView
	{
		public AppIntroView ()
		{
			InitializeComponent ();
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new BBLGitHubWebPage());
		}
	}
}