using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using CoreGraphics;
using CustomBackIconDemo;
using CustomBackIconDemo.Pages;
using CustomBackIconDemo.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(NavigationPageRendererIOS))]
namespace CustomBackIconDemo.iOS
{
    public class NavigationPageRendererIOS : NavigationRenderer
    {

        public NavigationPageRendererIOS() : base()
        {
        }

		// To be able to custom replace the navigation back button we are using the OnPushAsync and OnPopViewAsync methods  
		// to replace the buttons according to what is set in the page in the common code project.
		// Both methods are needed to correctly replace the Back button in the Navigation Bar for the page we are about to display
		// In those methods we are calling the 'SetBackButtonBasedOnInterface' which chooses the type of button to display.

		protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            var retVal = base.OnPushAsync(page, true); // IMPORTANT: First call base

            SetBackButtonBasedOnInterface(page);

			//CreateRightToolbarButtons();  // uncomment to see an example of a platform specific right toolbar created

			return retVal;
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var retVal = base.OnPopViewAsync(page, true); // IMPORTANT: First call base
			
            var returnPage = ((INavigationPageController)base.Element).StackCopy.ToArray()[1]; // get the page we are returning to
            if (returnPage != null)
            {
                SetBackButtonBasedOnInterface(returnPage);
            }

            return retVal;
        }

        // The method chooses which type of button to show based on a property set in the page
        // If no property was found, then the default behavior is set
        void SetBackButtonBasedOnInterface(Page page)
        {
            if (page is INavigationActionBarConfig incomingPage)
            {
                switch (incomingPage.BackButtonStyle)
                {
                    case 0:
                        SetDefaultBackButton();
                        break;

                    case 1:
                        HideBackButton();
                        break;

                    case 2: // 2=Image & Text
						SetImageTitleBackButton("Down", "Close", -15);
                        break;

					case 3: // 3=Image only
						SetImageBackButton("Down");
						break;

					case 4: // 4=Text only
						SetTitleBackButton("Close", 0);
						break;

					case 5: // 5=System Icon
						SetSystemBarButton(UIBarButtonSystemItem.Rewind);
						break;

					case 9: // 9=System Stop Icon Used for Menu Page
						SetSystemBarButton(UIBarButtonSystemItem.Stop);
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
            this.TopViewController.NavigationItem.LeftBarButtonItems = null;
        }

        // Hide the Back Button 
        // -------------------------------
        void HideBackButton()
        {
            this.TopViewController.NavigationItem.SetHidesBackButton(true, false);
        }

        // Sets a Image + Title Back Button
        // -------------------------------
        void SetImageTitleBackButton(string imageBundleName, string buttonTitle, int horizontalOffset)
        {
            var topVC = this.TopViewController;

            // create the image back button
            var backButtonImage = new UIBarButtonItem(
                    UIImage.FromBundle(imageBundleName), // see https://developer.xamarin.com/guides/ios/application_fundamentals/working_with_images/displaying-an-image/
                    UIBarButtonItemStyle.Plain,
                    (sender, args) =>
                    {
                        topVC.NavigationController.PopViewController(true); // event when tapped = go back
                    });

            // create the Text Back Button 
            var backButtonText = new UIBarButtonItem(
                    buttonTitle,
                    UIBarButtonItemStyle.Plain,
                    (sender, args) =>
                    {
                        topVC.NavigationController.PopViewController(true); // event when tapped = go back
                    });
            backButtonText.SetTitlePositionAdjustment(new UIOffset(horizontalOffset, 0), UIBarMetrics.Default); // 

            // Add the buttons to the Top Bar
            UIBarButtonItem[] buttons = new UIBarButtonItem[2];
            buttons[0] = backButtonImage;
            buttons[1] = backButtonText;

			// Add the buttons to the Top Bar
			topVC.NavigationItem.LeftBarButtonItems = buttons; // we add 2 buttons (notice property name '...ItemS')
        }

        // Sets an Image only Back Button
        // -------------------------------
        void SetImageBackButton(string imageBundleName)
        {
            // create the image back button
            var backButtonImage = new UIBarButtonItem(
                    UIImage.FromBundle(imageBundleName),
                    UIBarButtonItemStyle.Plain,
                    (sender, args) =>
                    {
                        this.TopViewController.NavigationController.PopViewController(true);
                    });

            this.TopViewController.NavigationItem.LeftBarButtonItem = backButtonImage; // we add 2 buttons (notice property name '...IteM')
        }

        // Sets a Text only Back button
        // -------------------------------
        void SetTitleBackButton(string buttonTitle, int horizontalOffset)
        {
            // create the image back button
            var backButtonText = new UIBarButtonItem(
                    buttonTitle,
                    UIBarButtonItemStyle.Plain,
                    (sender, args) =>
                    {
                        this.TopViewController.NavigationController.PopViewController(true);
                    });

            backButtonText.SetTitlePositionAdjustment(new UIOffset(horizontalOffset, 0), UIBarMetrics.Default);

            this.TopViewController.NavigationItem.LeftBarButtonItem = backButtonText;
        }


        // Sets a IOS specific System Icon
        // -------------------------------
        void SetSystemBarButton(UIBarButtonSystemItem systemButton = UIBarButtonSystemItem.Stop)
        {
            var topVC = this.TopViewController;
            // create the image back button
            var backButton = new UIBarButtonItem(
                    systemButton,
                    (sender, args) =>
                    {
                        topVC.NavigationController.PopViewController(true);
                    });

            topVC.NavigationItem.LeftBarButtonItem = backButton;
        }

        // --------------------------------------------------------------------------------------
        // Other Methods created during the research
        // Note: these are not used in the solution but left here for reference


        // This is an alternative way to create buttons on the left side of the navigation bar
        // Using this gives ou probably greater layout control BUT
        // the TINT Color functionality stops wotring with simple UIButton
        // 
        void CreateLeftToolbarButtons()
        {
            this.TopViewController.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(CreateButtonToolbar()), true);
        }


        // an example of how to render ya right toolbar buttons
        // again, greater layout control but TintColor stops working
        void CreateRightToolbarButtons()
        {
            this.TopViewController.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(CreateButtonToolbar()), true);
        }

        // creates Toolbar with:  '>''image''image''<'
		UIView CreateButtonToolbar()
		{
			var topVC = this.TopViewController;
            int btnLength = 20;

			// Create Text Button
			UIButton backButtonLeftText = new UIButton();
			backButtonLeftText.Frame = new CGRect(0, 0, 10, btnLength);
			backButtonLeftText.SetTitle(">", UIControlState.Normal);
			backButtonLeftText.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			backButtonLeftText.TouchUpInside += delegate
			{
				topVC.NavigationController.PopViewController(true);
			};

			// create Fish buttons
			UIButton fishButton = new UIButton();
			fishButton.Frame = new CGRect(10, 0, btnLength, btnLength);
			fishButton.SetImage(UIImage.FromBundle("Red-Fish-icon"), UIControlState.Normal);
			fishButton.TouchUpInside += delegate
			{
				topVC.NavigationController.PopViewController(true);
			};
			// create Starfish buttons
			UIButton startfishButton = new UIButton();
			startfishButton.Frame = new CGRect(30, 0, btnLength, btnLength);
			startfishButton.SetImage(UIImage.FromBundle("Starfish"), UIControlState.Normal);
			startfishButton.TouchUpInside += delegate
			{
				topVC.NavigationController.PopViewController(true);
			};

			// Create Text Button
			UIButton backButtonRightText = new UIButton();
			backButtonRightText.Frame = new CGRect(50, 0, 10, btnLength);
			backButtonRightText.SetTitle("<", UIControlState.Normal);
			backButtonRightText.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			backButtonRightText.TouchUpInside += delegate
			{
				topVC.NavigationController.PopViewController(true);
			};

			//Use CustomView with 3 UIButton as a right bar item
			UIView toolbar = new UIView(new CGRect(0, 0, 60, btnLength));
			toolbar.BackgroundColor = UIColor.Yellow;

			toolbar.AddSubview(backButtonLeftText);
			toolbar.AddSubview(fishButton);
			toolbar.AddSubview(startfishButton);
			toolbar.AddSubview(backButtonRightText);

			return toolbar;
		}


		// Helper Method to transform a LEFTBarButtonItem into a customView for better Layout control 
		// -------------------------------
		void TransformBarButtonsToCustom()
		{
			var topVC = this.TopViewController;
			// Customize the layout
			foreach (var leftItem in topVC.NavigationItem.LeftBarButtonItems)
			{
				var button = new UIButton(new CGRect(0, 0, 15, 0));
				button.SetImage(leftItem.Image, UIControlState.Normal);
				button.SetTitle(leftItem.Title, UIControlState.Normal);
				//button.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				button.TintColor = UIColor.Blue;

				FieldInfo fi = leftItem.GetType().GetField("clicked", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				Delegate del = (Delegate)fi.GetValue(leftItem);
				button.TouchUpInside += (EventHandler)del;

				leftItem.CustomView = button;
				leftItem.TintColor = UIColor.Orange;
				leftItem.CustomView.SizeToFit();
			}
		}
    }
}