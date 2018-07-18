using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Dynamo.Models;
using Dynamo.ViewModels;

namespace KeyboardShortcutViewExtension.NodeShortcuts
{
    /// <summary>
    /// Interaction logic for NodeShortcutsWindow.xaml
    /// </summary>
    public partial class NodeShortcutsWindow : Window
    {
        public NodeShortcutsWindow()
        {
            InitializeComponent();

            string shortcutsFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DynamoViewExtension",
                "dynamoShortcuts.txt");
            StreamReader sr = new StreamReader(shortcutsFileName);
            string[] allLines = sr.ReadToEnd().Split('\n');

            string[] lines = allLines.Where(x => x != "").ToArray();
            sr.Close();


            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitLine= lines[i].Split('|');
                System.Windows.Controls.Button newBtn = new Button();
                newBtn.Content = splitLine[0];
                newBtn.Name = "Button" + i.ToString();
                newBtn.Tag = splitLine[1];
                newBtn.Click += Button_Click_1;
                newBtn.Height = 34;
                newBtn.Width = 184;
                newBtn.Background = Brushes.White;
                sp.Children.Add(newBtn);
            }

            


                

            



        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string nodeName = button.Tag as string;
            KeyboardShortcutViewExtension.dynView.Model.ExecuteCommand(new DynamoModel.CreateNodeCommand(Guid.NewGuid().ToString(), nodeName, 0,0,false,false));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DynamoViewModel vm = KeyboardShortcutViewExtension.dynView;
            Button button = sender as Button;
            string shortcutsFileName = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DynamoViewExtension",
                "dynamoShortcuts.txt");
            StreamWriter sw = new StreamWriter(shortcutsFileName,true);
            if (vm.CurrentSpaceViewModel.HasSelection)
            {
                
                //string name = vm.CurrentSpace.CurrentSelection.First().Name;
                string longName = vm.CurrentSpace.CurrentSelection.First().CreationName;
                var node=vm.SearchViewModel.Model.SearchEntries.First(s => s.CreationName == longName);
                string name = node.Name;

                sw.WriteLine("\n"+name +'|' + longName);
                sw.Close();
            }
        }
        

    }
}
