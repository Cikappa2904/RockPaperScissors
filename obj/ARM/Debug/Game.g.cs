﻿#pragma checksum "D:\File Personali\Github\RockPaperScissors\RockPaperScissors\Game.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EF76B64738D0884870F71DC25C89EA5E51A8217177F94A66A789EB60AD026278"
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
    partial class Game : 
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
            case 2: // Game.xaml line 13
                {
                    this.VisualStateGroup = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                }
                break;
            case 3: // Game.xaml line 14
                {
                    this.Small = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4: // Game.xaml line 43
                {
                    this.Base = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5: // Game.xaml line 95
                {
                    this.stackPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 6: // Game.xaml line 121
                {
                    this.ContentDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.ContentDialog1).CloseButtonClick += this.ContentDialog_CloseButtonClick;
                }
                break;
            case 7: // Game.xaml line 96
                {
                    this.playerMove_RadioButtons = (global::Microsoft.UI.Xaml.Controls.RadioButtons)(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButtons)this.playerMove_RadioButtons).SelectionChanged += this.PlayerMove_Changed;
                }
                break;
            case 8: // Game.xaml line 109
                {
                    this.button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.button).Click += this.Button_Click;
                }
                break;
            case 9: // Game.xaml line 117
                {
                    this.showComputerThought = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Game.xaml line 111
                {
                    this.border1 = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 11: // Game.xaml line 114
                {
                    this.pcStreak = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Game.xaml line 112
                {
                    this.showComputerMove = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Game.xaml line 103
                {
                    this.border = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 14: // Game.xaml line 106
                {
                    this.playerStreak = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // Game.xaml line 104
                {
                    this.showPlayerMove = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16: // Game.xaml line 97
                {
                    this.radioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 17: // Game.xaml line 98
                {
                    this.radioButton1 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 18: // Game.xaml line 99
                {
                    this.radioButton2 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
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

