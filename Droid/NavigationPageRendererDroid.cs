using System.Threading.Tasks;
using Android.App;
using CustomBackIconDemo;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using CustomBackIconDemo.Pages;

// IMPORTANT NOTE: 
// The APPCOMP solution can not work at this stage due to a bug in the Xamarin Forms Base class NavigationPageRenderer
// 2017-06-18 waiting on a Pull REquest to be approved and released so we can activate this code: see https://bugzilla.xamarin.com/show_bug.cgi?id=57578
// The code is ready for when the fix has been released


// -------------------------------------------------------------------------
// To Switch between APPCOMP CODE and NON APPCOMP CODE Instructions
// simple uncomment and comment where indicated.
// ------------------------------------------------------------------------- 

using Xamarin.Forms.Platform.Android.AppCompat; // APPCOMP CODE 
using Xamarin.Forms.Platform.Android;


// [assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomBackIconDemo.Droid.NavigationPageRendererDroid))] // APPCOMP
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomBackIconDemo.Droid.NavigationPageRendererDroid))] // NON APPCOMP
namespace CustomBackIconDemo.Droid
{

    //public class NavigationPageRendererDroid : NavigationPageRenderer // APPCOMP
    public class NavigationPageRendererDroid : NavigationRenderer // NON APPCOMP
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

            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            var retVal = base.OnPushAsync(view, animated);
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