using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace RockPaperScissors
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    /// 


    //TODO: Pareggio carta

    public sealed partial class MultiPlayer : Page
    {

        string[] movesEmojis = new string[3] { "🪨", "📄", "✂️" };
        string[] movesText = new string[3] { "Rock", "Paper", "Scissors" };
        int playerWinStreak = 0, pcWinStreak = 0;
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings; //Local storage


        public MultiPlayer()
        {
            this.InitializeComponent();
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(); //Localization
            if(localSettings.Values["playerStreak"]==null) //Initializing to 0 localstorage variables
            {
                localSettings.Values["playerStreak"] = 0;
            }
            if (localSettings.Values["pcStreak"] == null)
            {
                localSettings.Values["pcStreak"] = 0;
            }

            if (!IsWindows11())
            {
                movesEmojis[0] = "✊";
            }



            rockRadioButton.Content = resourceLoader.GetString("Rock");
            
            paperRadioButton.Content = resourceLoader.GetString("Paper");
            scissorsRadioButton.Content = resourceLoader.GetString("Scissors");
            showComputerThought.Text = resourceLoader.GetString("I'm thinking");
            button.Content = resourceLoader.GetString("Play");
            playerStreak.Text = resourceLoader.GetString("Your streak") + ": " + localSettings.Values["playerStreak"];
            pcStreak.Text = resourceLoader.GetString("My streak") + ": " + localSettings.Values["pcStreak"];

        }




        private void PlayerMove_Changed(object sender, SelectionChangedEventArgs e)
        {
            int playerMove = playerMove_RadioButtons.SelectedIndex;
            switch (playerMove)
            {
                case 0: //Rock
                    if(IsWindows11())
                    {
                        showPlayerMove.Text = "🪨";
                    }
                    else
                    {
                        showPlayerMove.Text = "✊";
                    }
                    break;
                case 1: //Paper
                    showPlayerMove.Text = "📄";
                    break;
                case 2: //Scissors
                    showPlayerMove.Text = "✂️";
                    break;
            }


        }

        private async void DisplayResultDialog(string title, string content, string move, string content2)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string tempContent = content + " " + resourceLoader.GetString(move) + content2;

            ContentDialog1.Title = title;
            ContentDialog1.Content = tempContent;
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }

        async static Task<string> HttpGetAsync(string uri)
        {
            string content = "";
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }

        async static Task<string> HttpPostAsync(string uri, string line)
        {
            var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "thing1", line },
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(uri, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        async Task PlayGame(string get_request, int player_move)
        {
            string opponent_move = " ";
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            while (true)
            {
                opponent_move = HttpGetAsync(get_request).ToString();
                if(opponent_move != "-1")
                {
                    break;
                }
            }

            int x = int.Parse(opponent_move);

            switch (x)
            {
                case 0:
                    switch (player_move)
                    {

                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so it's a draw"));
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so you won"));
                            pcWinStreak = 0;
                            playerWinStreak++;
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Rock", resourceLoader.GetString(", so you lost"));
                            playerWinStreak = 0;
                            pcWinStreak++;
                            break;

                    }
                    break;
                case 1:
                    switch (player_move)
                    {
                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so you lost"));
                            pcWinStreak++;
                            playerWinStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so it's a draw"));
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Paper", resourceLoader.GetString(", so you won"));
                            pcWinStreak = 0;
                            playerWinStreak++;
                            break;

                    }
                    break;
                case 2:
                    switch (playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            DisplayResultDialog(resourceLoader.GetString("Victory"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so you won"));
                            playerWinStreak++;
                            pcWinStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog(resourceLoader.GetString("Lost"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so you lost"));
                            pcWinStreak++;
                            playerWinStreak = 0;
                            break;
                        case 2:
                            DisplayResultDialog(resourceLoader.GetString("Draw"), resourceLoader.GetString("I chose"), "Scissors", resourceLoader.GetString(", so it's a draw"));
                            break;

                    }
                    break;
            }

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        { 



            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            showComputerThought.Text = resourceLoader.GetString("I'm thinking");

            string post_request = localSettings.Values["ipAddress"].ToString() + "/send_result";
            string get_request = localSettings.Values["ipAddress"].ToString() + "/get_result";


            await HttpPostAsync(post_request, playerMove_RadioButtons.SelectedIndex.ToString());

            int playerMove = playerMove_RadioButtons.SelectedIndex;

            await PlayGame(get_request, playerMove);



            showComputerThought.Text = resourceLoader.GetString("I chose") + " " + resourceLoader.GetString(movesText[x]);

            
            
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["playerStreak"] = playerWinStreak.ToString();
            localSettings.Values["pcStreak"] = pcWinStreak.ToString();

            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            playerStreak.Text = resourceLoader.GetString("Your streak") + ": " + playerWinStreak.ToString();
            pcStreak.Text = resourceLoader.GetString("My streak") + ": " + pcWinStreak.ToString();
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
    }


}
