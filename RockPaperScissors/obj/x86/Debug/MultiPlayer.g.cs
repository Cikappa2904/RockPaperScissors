﻿#pragma checksum "C:\Users\Windows\Documents\GitHub\RockPaperScissors\RockPaperScissors\MultiPlayer.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FC7216BCDAF2D763411B1DAAE688A9159735D4A366FCAB2836EB1ED3091461AC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RockPaperScissors
{
    partial class MultiPlayer : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MultiPlayer.xaml line 13
                {
                    this.VisualStateGroup = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                }
                break;
            case 3: // MultiPlayer.xaml line 14
                {
                    this.Small = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4: // MultiPlayer.xaml line 43
                {
                    this.Base = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5: // MultiPlayer.xaml line 123
                {
                    this.ContentDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.ContentDialog1).CloseButtonClick += this.GameContentDialog_CloseButtonClick;
                }
                break;
            case 6: // MultiPlayer.xaml line 124
                {
                    this.ContentDialog2 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.ContentDialog2).CloseButtonClick += this.ContentDialog_CloseButtonClick;
                }
                break;
            case 7: // MultiPlayer.xaml line 95
                {
                    this.addressIP = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // MultiPlayer.xaml line 96
                {
                    this.stackPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 9: // MultiPlayer.xaml line 97
                {
                    this.playerMove_RadioButtons = (global::Microsoft.UI.Xaml.Controls.RadioButtons)(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButtons)this.playerMove_RadioButtons).SelectionChanged += this.PlayerMove_Changed;
                }
                break;
            case 10: // MultiPlayer.xaml line 110
                {
                    this.button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.button).Click += this.Button_Click;
                }
                break;
            case 11: // MultiPlayer.xaml line 118
                {
                    this.showComputerThought = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // MultiPlayer.xaml line 112
                {
                    this.border1 = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 13: // MultiPlayer.xaml line 115
                {
                    this.enemyStreak = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // MultiPlayer.xaml line 113
                {
                    this.showComputerMove = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // MultiPlayer.xaml line 104
                {
                    this.border = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 16: // MultiPlayer.xaml line 107
                {
                    this.playerStreak = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 17: // MultiPlayer.xaml line 105
                {
                    this.showPlayerMove = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18: // MultiPlayer.xaml line 98
                {
                    this.rockRadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 19: // MultiPlayer.xaml line 99
                {
                    this.paperRadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 20: // MultiPlayer.xaml line 100
                {
                    this.scissorsRadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

