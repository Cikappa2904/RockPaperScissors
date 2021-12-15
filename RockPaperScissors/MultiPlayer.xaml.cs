using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if(localSettings.Values["multiplayerSettings"].ToString() == "server")
            {
                TcpListener server = null;
                try
                {
                    // Set the TcpListener on port 13000.
                    Int32 port = 48765;
                    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                    // TcpListener server = new TcpListener(port);
                    server = new TcpListener(localAddr, port);

                    // Start listening for client requests.
                    server.Start();

                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];
                    String data = null;

                    // Enter the listening loop.
                    while (true)
                    {
                        Console.Write("Waiting for a connection... ");

                        // Perform a blocking call to accept requests.
                        // You could also use server.AcceptSocket() here.
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Connected!");

                        data = null;

                        // Get a stream object for reading and writing
                        NetworkStream stream = client.GetStream();

                        int i;

                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            playerStreak.Text = data;

                            // Process the data sent by the client.
                            data = data.ToUpper();

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);
                        }

                        // Shutdown and end connection
                        client.Close();
                    }
                }
                catch (SocketException f)
                {
                    Console.WriteLine("SocketException: {0}", f);
                }
                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                }

                Console.WriteLine("\nHit enter to continue...");
                Console.Read();
            }
            else
            {
                try
                {
                    // Create a TcpClient.
                    // Note, for this client to work you need to have a TcpServer
                    // connected to the same address as specified by the server, port
                    // combination.
                    Int32 port = 48765;
                    /*string server = localSettings.Values["ipAddress"].ToString();
                    TcpClient client = new TcpClient(server, port);*/

                    IPAddress server = IPAddress.Parse(localSettings.Values["ipAddress"].ToString());
                    IPEndPoint ipLocalEndPoint = new IPEndPoint(server, port);
                    TcpClient client = new TcpClient(ipLocalEndPoint);

                    string message = "";

                    switch(playerMove_RadioButtons.SelectedIndex)
                    {
                        case 0:
                            message = "Rock";
                            break;
                        case 1:
                            message = "Paper";
                            break;
                        case 2:
                            message = "Scissors";
                            break;
                    }

                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    //  Stream stream = client.GetStream();

                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);

                    Console.WriteLine("Sent: {0}", message);

                    // Receive the TcpServer.response.

                    // Buffer to store the response bytes.
                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    // Close everything.
                    stream.Close();
                    client.Close();
                }
                catch (ArgumentNullException f)
                {
                    Console.WriteLine("ArgumentNullException: {0}", f);
                }
                catch (SocketException f)
                {
                    Console.WriteLine("SocketException: {0}", f);
                }

                Console.WriteLine("\n Press Enter to continue...");
                Console.Read();
            }


            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            showComputerThought.Text = resourceLoader.GetString("I'm thinking");
            Random randNumber = new Random();
            int x = 0;
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(100);
                x = randNumber.Next(3);
                showComputerMove.Text = movesEmojis[x];
            }

            showComputerThought.Text = resourceLoader.GetString("I chose") + " " + resourceLoader.GetString(movesText[x]);

            switch(x)
            {
                case 0:
                    switch(playerMove_RadioButtons.SelectedIndex)
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
                    switch (playerMove_RadioButtons.SelectedIndex)
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
