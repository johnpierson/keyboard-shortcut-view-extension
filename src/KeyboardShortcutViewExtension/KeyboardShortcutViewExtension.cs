using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Dynamo.Controls;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;
using KeyboardShortcutViewExtension.NodeShortcuts;
using Brushes = System.Windows.Media.Brushes;

namespace KeyboardShortcutViewExtension
{
    public class KeyboardShortcutViewExtension : IViewExtension
    {
        private MenuItem ksMenuItem;

        public void Dispose()
        {
        }
        static DynamoView view;
        public void Startup(ViewStartupParams p) { }

        public void Loaded(ViewLoadedParams p)
        {
            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            view = p.DynamoWindow as DynamoView;
            ksMenuItem = new MenuItem { Header = "Keyboard Shortcuts" };



            #region Shortcuts

            ksMenuItem.Click += (sender, args) =>
                {
                    var viewModel = new NodeShortcutsViewModel(p);
                    var window = new NodeShortcutsWindow()
                    {
                        // Set the data context for the main grid in the window.
                        sp = { DataContext = viewModel },

                        // Set the owner of the window to the Dynamo window.
                        Owner = p.DynamoWindow
                    };

                    window.Left = window.Owner.Left + 400;
                    window.Top = window.Owner.Top + 200;

                    // Show a modeless window.
                    window.Show();

                };
            p.dynamoMenu.Items.Add(ksMenuItem);
            #endregion
        }

        public void Shutdown()
        {
        }

        public string UniqueId
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public string Name
        {
            get { return "Keyboard Shortcuts View Extension"; }

        }
        public static DynamoViewModel dynView
        {
            get { return view.DataContext as DynamoViewModel; }
        }

    }
}
