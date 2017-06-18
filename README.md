# Custom Back Icon Demo
Simple Custom Navigation Back Icon Demo for Xamarin Forms

## INTRODUCTION
A simple demo application demonstrating how to change the Back button Icon and Style per individual page using Xamarin Forms and Custom Renderers.

![Demo](https://raw.githubusercontent.com/bbl-Laobu/SlideMenuDemo/master/SimpleSlideMenuDemo.gif)

## DESCRIPTION
Changing the Back Button directly from within the Xamarin.Forms is not possible. Therefore, to be able to change the icon and style of the Back button, a custom renderer per platform is needed.

As the Navigation Bar is part of the NavigationPage, we therefore need to override this class to be able to change the Back Button style as we render it. 

To do this, we need to firstly create a CustomNavigationPage in our Xamarin common project which will inhirit from NavigationPage and which we will use as a reference for our renderers.
```csharp
public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page startupPage) : base(startupPage)
        {
        }
    }
```

On startup, this is the page we will asign to our MainPage.

```csharp
public App()
        {
            InitializeComponent();

            MainPage = new CustomNavigationPage(new StartPage()); // note that we are calling a standard content page here;
		}
```

Now, somehow we need to intercept the rendering of the CustomNavigationPage therebye allowing us to change the Back Button. This is done by creating a NavigationPageRenderer class per platform and declare that the class will be called when the CustomNavigationPage is being rendered. We do this as follows in the iOS project (Android is similar):
```csharp
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(NavigationPageRendererIOS))]
namespace CustomBackIconDemo.iOS
{
    public class NavigationPageRendererIOS : NavigationRenderer
    {
```

See the 'assembly' line that links CustomNavigationPage to the NavigationPageRendererIOS class? 
Also, notice of course that our NavigationPageRendererIOS inherits from the real NavigationRenderer class (Again, Android is similar).

Next, as we now have a class that is called everytime we render the a page, we need to implement some methods to inject our own code.

We use the OnPop and OnPush methods for that. Meaning, the OnPush method is called as a page is pushed onto the stack of the NavigationPage and OnPop is called when the page is removed from the stack. 
The reason we are doing this way is because a NavigationPage has only 1 instance of the navigation bar at the top. Whenever we render the page, we need to decide what Back Button we would like to display both as we add a page(Push) or when we remove a page (Pop).

For iOS this looks something like this: 
```csharp
protected override Task<bool> OnPushAsync(Page page, bool animated)
	{
            var retVal = base.OnPushAsync(page, true); // IMPORTANT: First call base
            SetBackButtonBasedOnInterface(page);
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
```

Simply put, we call the base methos, then overwrite the back button with whatever we decided and then return with the result of calling our base method.

Important to know is that the Page provided in the OnPushAsync is the page we are about to show. However, the Page provided for the OnPopViewAsync is not the page about to be displayed but the page to be removed. If we want the page that is about to be displayed, we need to retrieve it from the stack first.
```csharp
var returnPage = ((INavigationPageController)base.Element).StackCopy.ToArray()[1]; // get the page we are returning to
```

Retrieving the correct page to be displayed is needed as per page, we want to be able to decided what Back Button Style to show. To do this, we set a property per page that allows us to instruct the renderer which Back Button Style to show.

As you can see in bot POP and PUSH methods, we are calling the metthod SetBackButtonBasedOnInterface(PAGE);
```csharp
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
		...
```

As you can see, this method basicaly takes a property from the Page coming in and decides on its value which Back Button Style to show.

I am pretty sure there are different ways to pass data from the Common Xamarin project to the individual platform Projects, but in our case we choose to define an Interface with the property which we make our pages inherit from whenever we want to influence the style of the Back Button. 

The pages implement the property and whenever we create a new page, we inject the requested style into the constructor of our page.
```csharp
public partial class MenuPage : ContentPage, INavigationActionBarConfig
{
	public int BackButtonStyle { get; set; } // implementing INavigationActionBarConfig

	public MenuPage(int backButtonStyle) : this()
	{
		BackButtonStyle = backButtonStyle; // see INavigationActionBarConfig for possible values
	}
	...
```
This is handy as for the same page, we are able to request different Back Button Styles, simply by injecting different values into the constructor ar creation time.

Check MenuPage.xaml.cs to see this in action, as in our demo our Result Page simply changes according to what we pass along.
```csharp
public partial class MenuPage : ContentPage, INavigationActionBarConfig
{
	...
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
		...
```

What each value does to the Style is up to you in the platform specific Renderer. Each platform can be different or you can decided to implement something different in one platform and the default for the same incoming value on another platform.

## RENDERING





In conclusion; using this system should allow us to easily create any type of slide menu/form regardless of side, animation and size. We use grid to determine upfront where we want the menu to be displayed for interaction, and then use an animated version to move it to that exact location before switching between both. 

Enjoy and any question or improvements, please let me know.

Laobu!


## REFERENCES
- ‘Grid - Present views in grids.’ By Xamarin @ [https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/grid/](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/grid/ )
- ‘Carousel View’ by alexrainman @ [https://github.com/alexrainman/CarouselView](https://github.com/alexrainman/CarouselView)
- ‘Creating Animations with Xamarin.Forms’ by David Britch @ [https://blog.xamarin.com/creating-animations-with-xamarin-forms/](https://blog.xamarin.com/creating-animations-with-xamarin-forms/)


Author
Laobu – Bernard Blanckaert
