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

Now, somehow we need to intercept the rendering of the CustomNavigationPage therebye allowing us to change the Back Button.

This is done by creating a NavigationPageRenderer class per platform and declare that the class will be called when the CustomNavigationPage is being rendered. We do this as follows in the IOS project (Android is similar):
```csharp
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(NavigationPageRendererIOS))]
namespace CustomBackIconDemo.iOS
{
    public class NavigationPageRendererIOS : NavigationRenderer
    {
```

See the 'assembly' line that links CustomNavigationPage to the NavigationPageRendererIOS class?

Now, notice of course that our NavigationPageRendererIOS from the real NavigationRenderer class. Again, Android is similar. 






In conclusion; using this system should allow us to easily create any type of slide menu/form regardless of side, animation and size. We use grid to determine upfront where we want the menu to be displayed for interaction, and then use an animated version to move it to that exact location before switching between both. 

Enjoy and any question or improvements, please let me know.

Laobu!


## REFERENCES
- ‘Grid - Present views in grids.’ By Xamarin @ [https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/grid/](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/grid/ )
- ‘Carousel View’ by alexrainman @ [https://github.com/alexrainman/CarouselView](https://github.com/alexrainman/CarouselView)
- ‘Creating Animations with Xamarin.Forms’ by David Britch @ [https://blog.xamarin.com/creating-animations-with-xamarin-forms/](https://blog.xamarin.com/creating-animations-with-xamarin-forms/)


Author
Laobu – Bernard Blanckaert
