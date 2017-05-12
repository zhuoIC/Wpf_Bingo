using System;
using System.Collections.Generic;
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

namespace Wpf_Bingo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<TextBlock> arrayBingo = new List<TextBlock>();
        static Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            PintarGrid();
            tbxBolasQuedan.Text = arrayBingo.Count.ToString();
        }

        void PintarGrid()
        {
            int contador = 1;
            for (int i = 0; i < grdBingo.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grdBingo.ColumnDefinitions.Count; j++)
                {

                    TextBlock tmp = new TextBlock();
                    tmp.Text = contador++.ToString();
                    tmp.Background = new SolidColorBrush(Colors.White);
                    tmp.FontSize = 18;
                    tmp.Margin = new Thickness(1, 1, 1, 1);
                    tmp.TextAlignment = TextAlignment.Center;
                    Grid.SetColumn(tmp, j);
                    Grid.SetRow(tmp, i);
                    grdBingo.Children.Add(tmp);
                    arrayBingo.Add(tmp);
                }
            }
        }

        private void btnExtraer_Click(object sender, RoutedEventArgs e)
        {
            bool exito = false;
            int candidato = 0;
            do
            {
                candidato = rnd.Next(1, 91);
                for (int i = 0; i < arrayBingo.Count; i++)
                {
                    if (arrayBingo[i].Text == candidato.ToString())
                    {
                        ExtraerBola(i);
                        AnadirNumeroTextBlock(candidato);
                        AnadirItemComboBox(candidato);
                        exito = true;
                    }
                } 
            } while (!exito);
            if (arrayBingo.Count == 0)
            {
                btnExtraer.IsEnabled = false;
            }
        }

        private void ExtraerBola(int i)
        {
            arrayBingo[i].Background = new SolidColorBrush(Colors.Red);
            arrayBingo.Remove(arrayBingo[i]);
            tbxBolasQuedan.Text = arrayBingo.Count.ToString();
        }

        private void AnadirNumeroTextBlock(int candidato)
        {
            tblNumero.Text = candidato.ToString();
            tblNumero.FontSize = 30;
            tblNumero.Background = new SolidColorBrush(Colors.Pink);
        }

        private void AnadirItemComboBox(int candidato)
        {
            ComboBoxItem n = new ComboBoxItem();
            n.Content = candidato.ToString();

            cbxBolasSacadas.Items.Add(n);
        }

        private void mnuNuevo_Click(object sender, RoutedEventArgs e)
        {
            grdBingo.Background = new SolidColorBrush(Colors.Black);
            arrayBingo.Clear();
            cbxBolasSacadas.Items.Clear();
            tblNumero.Text = "";
            PintarGrid();
            tbxBolasQuedan.Text = arrayBingo.Count.ToString();
            btnExtraer.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quiere salir?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }
    }
}
