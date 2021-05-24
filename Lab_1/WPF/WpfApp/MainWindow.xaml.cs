using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Lab_2.Models.Collections;
using Lab_2.Models;
using System.Windows.Input;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private V5MainCollection Main = new V5MainCollection();
        ConnectWithDataOnGrid connect;
        public static RoutedCommand AddCustom = new RoutedCommand("Add", typeof(WpfApp.MainWindow));
        public MainWindow(){
            InitializeComponent();
            DataContext = Main;
            connect = new ConnectWithDataOnGrid(ref Main);
            AddCustomGrid.DataContext = connect;
        }
        private void ButtonNew(object sender, RoutedEventArgs e)
        {
            if (Main.Change == true) {
                UnsavedChanges();
            }
            Main = new V5MainCollection();
            DataContext = Main;
            ErrorMsg();
        }

        private void ButtonAddElement(object sender, RoutedEventArgs e)
        {
            try{
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if ((bool)dlg.ShowDialog() == true) Main.AddFromFile(dlg.FileName);
            }
            catch (Exception){
                MessageBox.Show("Add error");
            }
            finally{
                ErrorMsg();
            }
        }

        private void ButtonV5DataCollection(object sender, RoutedEventArgs e)
        {
            Main.AddDefaultDataCollection();
            ErrorMsg();
        }

        private void ButtonV5MainCollection(object sender, RoutedEventArgs e)
        {
            Main.AddDefaults();
            DataContext = Main;
            ErrorMsg();
        }

        private void ButtonV5DataOnGrid(object sender, RoutedEventArgs e)
        {
            Main.AddDefaultDataOnGrid();
            ErrorMsg();
        }

        private bool UnsavedChanges()
        {
            MessageBoxResult msg = MessageBox.Show("Save Changes?", "Save", MessageBoxButton.YesNoCancel);
            if (msg == MessageBoxResult.Cancel){
                return true;
            }
            else if (msg == MessageBoxResult.Yes) {
                Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                if ((bool)dialog.ShowDialog() == true) Main.Save(dialog.FileName);
            }
            return false;
        }

        private void AppClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Main.Change == true){
                e.Cancel = UnsavedChanges();
            }
            ErrorMsg();
        }

        private void FilterDataCollection(object sender, FilterEventArgs args)
        {
            var item = args.Item;
            if (item != null == true){
                if (item.GetType() == typeof(V5DataCollection)) args.Accepted = true;
                else args.Accepted = false;
            }
        }

        private void FilterDataOnGrid(object sender, FilterEventArgs args)
        {
            var item = args.Item;
            if (item != null == true){
                if (item.GetType() == typeof(V5DataOnGrid)) args.Accepted = true;
                else args.Accepted = false;
            }
        }

        public void ErrorMsg()
        {
            if (Main.ErrorMessage != null){
                MessageBox.Show(Main.ErrorMessage, "Error");
                Main.ErrorMessage = null;
            }
        }

        private void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (Main.Change == true)
                {
                    UnsavedChanges();
                }
                Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
                if ((bool)fd.ShowDialog() == true)
                {
                    Main = new V5MainCollection();
                    Main.Load(fd.FileName);
                    DataContext = Main;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ", ex.Message);
            }
            finally
            {
                ErrorMsg();
            }
        }

        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                if ((bool)dialog.ShowDialog() == true)
                    Main.Save(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ButtonSave" + ex.Message);
            }
            finally
            {
                ErrorMsg();
            }
        }

        private void CanSaveExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Main.Change;
        }

        private void DeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (Main != null)
                try
                {
                    Main.RemoveAt(lisBox_Main.SelectedIndex);
                }
                catch (Exception)
                {
                    MessageBox.Show("Выберите элемент");
                }
        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((lisBox_Main != null) && (lisBox_Main.Items.Contains(lisBox_Main.SelectedItem)))
            {
                e.CanExecute = true;
            }
            else { 
                e.CanExecute = false;
            }
        }

        private void AddDataOnGridExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                connect.Add();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CanAddDataOnGridExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TextBox_Step == null || TextBox_X_num == null || TextBox_Y_num == null || TextBox_Str == null)
            {
                e.CanExecute = false;
            }
            else if (Validation.GetHasError(TextBox_Step) || Validation.GetHasError(TextBox_X_num) ||
                     Validation.GetHasError(TextBox_Y_num) || Validation.GetHasError(TextBox_Str))
            {
                e.CanExecute = false;
            }
            else
                e.CanExecute = true;
        }
    }
}
