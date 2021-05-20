﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Project;

namespace Simulate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CommandExit = new RoutedCommand();

        string Filter = "纵断面文件(*.project)|*.project";
        bool CanClose = true;

        private ObservableCollection<TreeViewNode> Nodes;
        private ProjectFile ProjectFile { get; set; }
        private Project Project { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Project = new Project();
            Nodes = new ObservableCollection<TreeViewNode>();
            Project.Name = "Test";
            Nodes.Add(Project.Node);
            ProjectTreeView.ItemsSource = Nodes;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;
            if (target != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void OpenFile_Execute(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Show!", "Message");
        }

        private void Save_Execute(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            return false;
        }

        private void SaveAs_Execute(object sender, RoutedEventArgs e)
        {

        }

        public bool SaveAs()
        {
            //TODO: 完善另存profile文件。
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Filter;
            if (sfd.ShowDialog() == true)
            {
                return true;
            }
            return false;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CanClose)
            {
                e.Cancel = false;
                return;
            }
            switch (MessageBox.Show("项目数据已修改，现在退出会丢失数据！是否保存？", "警告！", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    if (SaveAs())
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                    break;
                case MessageBoxResult.No:
                    e.Cancel = false;
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void ProjectTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*
            if (((TreeViewItem)sender).GetType() == typeof(ProfileViewModel))
            {
                switch (((TreeViewItem)sender).PersistId)
                {
                    case 1:
                        break;
                }

            }
            */
        }
    }
}
