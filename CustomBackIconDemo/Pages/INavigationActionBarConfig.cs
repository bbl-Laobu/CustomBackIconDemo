using System;

namespace CustomBackIconDemo.Pages
{
    public interface INavigationActionBarConfig
    {
        // This interface is used to pass a BackButtonStyle property to the NavgiationPage Custom renderer.
        // We can pass properties per content page and thus show a different 'Back Button' per page
        // 
        // The platform specific renderer uses the choosen style to display our own custom styled back button
        // See our Custom Navigation Page Renderers 'NavigationPageRendererDroid.cs' & 'NavigationPageRendereriOS.cs'


        // All Platforms
        int BackButtonStyle { get; set; }
            //Possible Values
            // 0=default 
            // 1=Hide 
            // 2=Image & Text
            // 3=Image only
            // 4=Text only
            // 5=System Icon
            // 6=Image + Icon + Text
            // 7=Hide Brand Icon
            // 9=System Stop Icon Used for Menu Page

            // .... (define yourself) 


    }
}

