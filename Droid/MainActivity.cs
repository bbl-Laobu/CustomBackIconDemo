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
	// APPCOMP CODE - Instructions
	// 1. uncomment the code below 
	// 2. Comment out the 'NON APPCOMP CODE' below
	// 3. Uncomment the 'APPCOMP CODE' in NavigationPageRendererDroid
	// 4. Comment out the 'NON APPCOMP CODE' in NavigationPageRendererDroid
	//
	// -------------------------------------------------------------------------
	
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

	// -------------------------------------------------------------------------
	// NON APPCOMP CODE - INSTRUCTIONS
	// 1. uncomment th code below 
	// 2. Comment out the 'APPCOMP CODE' above
	// 3. Uncomment the 'NON APPCOMP CODE' in NavigationPageRendererDroid
	// 4. Comment out the 'APPCOMP CODE' in NavigationPageRendererDroid
	// -------------------------------------------------------------------------
	/*
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
    */
}
