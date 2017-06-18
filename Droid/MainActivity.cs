using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CustomBackIconDemo.Droid
{
	// -------------------------------------------------------------------------
	// NON APPCOMP CODE - INSTRUCTIONS
	// 1. uncomment the code below 
	// 2. Comment out the 'APPCOMP CODE'
	// 3. Uncomment the 'NON APPCOMP CODE' in NavigationPageRendererDroid
	// 4. Comment out the 'APPCOMP CODE' in NavigationPageRendererDroid
	// -------------------------------------------------------------------------
	[Activity(Label = "CustomBackIcon.Droid", 
              Icon = "@drawable/icon", 
              Theme = "@android:style/Theme.Holo.Light",
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }


	// IMPORTANT NOTE: 
	// The APPCOMP solution can not work at this stage due to a bug in the Xamarin Forms Base class NavigationPageRenderer
	// 2017-06-18 waiting on a Pull REquest to be approved and released so we can activate this code: see https://bugzilla.xamarin.com/show_bug.cgi?id=57578
	// -------------------------------------------------------------------------
	// APPCOMP CODE - Instructions
	// 1. uncomment the code 
	// 2. Comment out the 'NON APPCOMP CODE'
	// 3. Uncomment the 'APPCOMP CODE' in NavigationPageRendererDroid
	// 4. Comment out the 'NON APPCOMP CODE' in NavigationPageRendererDroid
	//
	// -------------------------------------------------------------------------
	/*
    [Activity(Label = "CustomBackIconDemo.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
    */
}
