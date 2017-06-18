using Xamarin.Forms;

namespace CustomBackIconDemo.Pages
{
    public partial class ResultPage : ContentPage, INavigationActionBarConfig
    {
        public int BackButtonStyle { get; set; } // implementing INavigationActionBarConfig

		public ResultPage()
        {
            InitializeComponent();
        }

		public ResultPage(int backButtonStyle) : this()
        {
			BackButtonStyle = backButtonStyle; // see INavigationActionBarConfig for possible values
		}

		async void Handle_ClickedAsync(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync(true); // Here we specify to hide the backbutton
		}
    }
}
