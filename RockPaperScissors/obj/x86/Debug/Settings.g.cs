﻿#pragma checksum "C:\Users\Windows\Documents\GitHub\RockPaperScissors\RockPaperScissors\Settings.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "328FB2705DF282313A4FA8337A6480858CEC13940769706B83D95C756F0C657A"
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
    partial class Settings : 
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
            case 2: // Settings.xaml line 30
                {
                    this.ContentDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 3: // Settings.xaml line 26
                {
                    this.serverIP = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Settings.xaml line 27
                {
                    this.confirmIP = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.confirmIP).Click += this.Button_Click;
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

