using System.Threading.Tasks;
using Android.App;
using CustomBackIconDemo;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using CustomBackIconDemo.Pages;

using Xamarin.Forms.Platform.Android.AppCompat; // APPCOMP CODE 
using Xamarin.Forms.Platform.Android;
//using Xamarin.Forms.Platform.Android; // NON APPCOMP CODE

// -------------------------------------------------------------------------
// APPCOMP CODE - Instructions
// 1. uncomment the code below 
// 2. Comment out the 'NON APPCOMP CODE' below
// 3. Uncomment the 'APPCOMP CODE' in NavigationPageRendererDroid
// 4. Comment out the 'NON APPCOMP CODE' in NavigationPageRendererDroid
//
// -------------------------------------------------------------------------
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomBackIconDemo.Droid.NavigationPageRendererDroid))]
namespace CustomBackIconDemo.Droid
{
    public class NavigationPageRendererDroid : NavigationPageRenderer
    {
        static ActionBar actionBar;

        public NavigationPageRendererDroid() : base()
        {
            actionBar = ((Activity)Context).ActionBar;
        }


        protected new Task<bool> PopViewAsync(Page page, bool animated = true) 
		{
			var retVal = base.PopViewAsync(page, animated);

			var returnPage = ((INavigationPageController)base.Element).StackCopy.ToArray()[1];
			if (returnPage != null)
			{
				SetHeaderIcons(returnPage);
			}

			return retVal;
		}

		public new Task<bool> PushViewAsync(Page page, bool animated = true)
		{
			var retVal = base.PushViewAsync(page, animated);
			SetHeaderIcons(page);
			return retVal;
		}

		void SetHeaderIcons(Page view)
		{
			if (view is INavigationActionBarConfig incomingPage)
			{
				// select the correct back icon to show
				switch (incomingPage.BackButtonStyle)
				{
					case 0: // 0=default 
						SetDefaultBackButton();
						break;

					case 1: // 1=Hide 
						actionBar.SetHomeAsUpIndicator(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide back arrow
						actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
						view.Title = "";
						break;

					case 2: // 2=Image & Text 
						actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image
						actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
						break;

					case 3: // 3=Image only
						actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image 
						actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
						view.Title = "";
						break;

					case 4: // 4=Text only
						actionBar.SetHomeAsUpIndicator(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide back arrow
						actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
						view.Title = " " + view.Title;
						break;

					case 6: // 2=Image & Text 
						actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image
						actionBar.SetIcon(CustomBackIconDemo.Droid.Resource.Drawable.icon); // set Brand icon
						break;

					case 7: // 6=Hide Brand Icon
						actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
						break;

					default:
						SetDefaultBackButton();
						break;
				}
			}
			else
			{
				SetDefaultBackButton(); // set default behavior back 
			}

		}


		// Sets the default Back Button 
		// -------------------------------
		void SetDefaultBackButton()
		{
			actionBar.SetHomeAsUpIndicator(null);
			actionBar.SetIcon(CustomBackIconDemo.Droid.Resource.Drawable.icon);
		}
    }


}

/*
// -------------------------------------------------------------------------
// NON APPCOMP CODE - INSTRUCTIONS
// 1. uncomment the code below 
// 2. Comment out the 'APPCOMP CODE' above
// 3. Uncomment the 'NON APPCOMP CODE' in MainActivity
// 4. Comment out the 'APPCOMP CODE' in MainActivity
// -------------------------------------------------------------------------
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomBackIconDemo.Droid.NavigationPageRendererDroid))]
namespace CustomBackIconDemo.Droid
{
	public class NavigationPageRendererDroid : NavigationRenderer
    {
        static ActionBar actionBar;

        public NavigationPageRendererDroid() : base()
        {
            actionBar = ((Activity)Context).ActionBar;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var returnPage = ((INavigationPageController)base.Element).StackCopy.ToArray()[1];
            if (returnPage != null)
            {
                SetHeaderIcons(returnPage);
            }

            return base.OnPopViewAsync(page, false);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            var retVal = base.OnPushAsync(view, false);
            SetHeaderIcons(view);
            return retVal;
        }

        void SetHeaderIcons(Page view)
        {
            if (view is INavigationActionBarConfig incomingPage)
            {
                // select the correct back icon to show
                switch (incomingPage.BackButtonStyle)
                {
                    case 0: // 0=default 
                        SetDefaultBackButton();
                        break;

                    case 1: // 1=Hide 
                        actionBar.SetHomeAsUpIndicator(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide back arrow
                        actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
                        view.Title = "";
                        break;

                    case 2: // 2=Image & Text 
                        actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image
                        actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
                        break;

                    case 3: // 3=Image only
                        actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image 
                        actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
                        view.Title = "";
                        break;

                    case 4: // 4=Text only
                        actionBar.SetHomeAsUpIndicator(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide back arrow
                        actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
                        view.Title = " " + view.Title;
                        break;

                    case 6: // 2=Image & Text 
                        actionBar.SetHomeAsUpIndicator(CustomBackIconDemo.Droid.Resource.Drawable.backman_icon); // Show custom image
                        actionBar.SetIcon(CustomBackIconDemo.Droid.Resource.Drawable.icon); // set Brand icon
                        break;

                    case 7: // 6=Hide Brand Icon
                        actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid())); // Hide Icon
                        break;

                    default:
                        SetDefaultBackButton();
                        break;
                }
            }
            else
            {
                SetDefaultBackButton(); // set default behavior back 
            }

        }


        // Sets the default Back Button 
        // -------------------------------
        void SetDefaultBackButton()
        {
            actionBar.SetHomeAsUpIndicator(null);
            actionBar.SetIcon(CustomBackIconDemo.Droid.Resource.Drawable.icon);
        }

    }
}
*/