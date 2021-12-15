using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace RockPaperScissors
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            view.TitleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            view.TitleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            MainFrame.Navigate(typeof(SinglePlayer));

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 500));

            if (!IsWindows11()) //Set Acrylic background for Windows 10 since it doesn't support Mica (which is enabled in XAML)
            {
                

                Windows.UI.Xaml.Media.AcrylicBrush myBrush = new Windows.UI.Xaml.Media.AcrylicBrush();
                myBrush.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                myBrush.TintOpacity = 0.6;

                MainFrame.Background = myBrush;
            }
        }

        public bool IsWindows11()
        {
            var v = Int64.Parse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            var build = (v & 0x00000000FFFF0000) >> 16;

            if (build >= 22000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                MainFrame.Navigate(typeof(Settings));
            }
            else
            {
                MUXC.NavigationViewItem item = args.SelectedItem as MUXC.NavigationViewItem;

                switch (item.Tag.ToString()) //This is a switch with just one case; it's here to be used if I want to add another page
                {
                    case "SinglePlayer":
                        MainFrame.Navigate(typeof(SinglePlayer));
                        break;
                    case "MultiPlayer":
                        MainFrame.Navigate(typeof(MultiPlayer));
                        break;
                }
            }
        }

    }
}
