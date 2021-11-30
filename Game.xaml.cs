using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Navigation;
// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace RockPaperScissors
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    /// 


    

    public sealed partial class Game : Page
    {

        string[] movesEmojis = new string[3] { "🪨", "📄", "✂️" };
        string[] movesText = new string[3] { "Rock", "Paper", "Scissors" };
        int winStreak = 0;

        public Game()
        {
            this.InitializeComponent();

        }

        
        

        private void PlayerMove_Changed(object sender, SelectionChangedEventArgs e)
        {
            int playerMove = playerMove_RadioButtons.SelectedIndex;
            switch (playerMove)
            {
                case 0: //Rock
                    showPlayerMove.Text = "🪨";
                    break;
                case 1: //Paper
                    showPlayerMove.Text = "📄";
                    break;
                case 2: //Scissors
                    showPlayerMove.Text = "✂️";
                    break;
            }


        }

        private async void DisplayResultDialog(string title, string content)
        {
            ContentDialog1.Title = title;
            ContentDialog1.Content = content;
            ContentDialog1.CloseButtonText = "Ok";
            ContentDialog1.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult result = await ContentDialog1.ShowAsync();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            showComputerThought.Text = "I'm thinking...";
            Random randNumber = new Random();
            int x = 0;
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(100);
                x = randNumber.Next(3);
                showComputerMove.Text = movesEmojis[x];
            }

            showComputerThought.Text = "I chose " + movesText[x];

            switch(x)
            {
                case 0:
                    switch(playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            DisplayResultDialog("Draw", "I chose rock, so it's a draw");
                            winStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog("Victory", "I chose rock, so you won");
                            winStreak++;
                            break;
                        case 2:
                            DisplayResultDialog("Lost", "I chose rock, so I won");
                            winStreak = 0;
                            break;

                    }
                    break;
                case 1:
                    switch (playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            DisplayResultDialog("Lost", "I chose paper, so I won");
                            winStreak = 0;
                            break;
                        case 1:
                            DisplayResultDialog("Draw", "I chose paper, it's a draw");
                            winStreak = 0;
                            break;
                        case 2:
                            DisplayResultDialog("Victory", "I chose paper, so you won");
                            winStreak++;
                            break;

                    }
                    break;
                case 2:
                    switch (playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            DisplayResultDialog("Victory", "I chose scissors, so you won");
                            winStreak++;
                            break;
                        case 1:
                            DisplayResultDialog("Lost", "I chose scissors, so I won");
                            winStreak = 0;
                            break;
                        case 2:
                            DisplayResultDialog("Draw", "I chose scissors, so it's a draw");
                            winStreak = 0;
                            break;

                    }
                    break;
            }
            
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            streak.Text = winStreak.ToString();
        }
    }
}
