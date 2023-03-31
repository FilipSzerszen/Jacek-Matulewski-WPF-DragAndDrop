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

namespace Jacek_Matulewski_WPF_DragAndDrop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox lbSender = sender as ListBox;
            ListBoxItem przenoszonyElement = lbSender.GetItemAt(e.GetPosition(lbSender));
            if(przenoszonyElement != null)
            {
                DataObject dane = new DataObject();
                //dane.SetData("Format_Lista", lbSender);
                //dane.SetData("Format_ElementListy", przenoszonyElement);
                dane.SetData("Format_EtykietaElementuListy", przenoszonyElement.Content as string);
                DragDrop.DoDragDrop(lbSender, dane, DragDropEffects.Copy | DragDropEffects.Move);

                if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Control)) lbSender.Items.Remove(przenoszonyElement);
            }
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey)) e.Effects = DragDropEffects.Copy;
            else e.Effects = DragDropEffects.Move;

        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox lbSender = sender as ListBox;

            //ListBox lbSource = e.Data.GetData("Format_Lista") as ListBox;
            //ListBoxItem przenoszonyElement = e.Data.GetData("Format_ElementListy") as ListBoxItem;

            //if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey)) lbSource.Items.Remove(przenoszonyElement);
            //else przenoszonyElement = new ListBoxItem() { Content = przenoszonyElement.Content };

            string etykietaPrzenoszonegoElementu = e.Data.GetData("Format_EtykietaElementuListy") as string;
            ListBoxItem przenoszonyElement = new ListBoxItem() { Content = etykietaPrzenoszonegoElementu };

            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey)) e.Effects = DragDropEffects.Copy;
            else e.Effects = DragDropEffects.Move;

            int indeks = lbSender.IndexFromPoint(e.GetPosition(lbSender));
            if (indeks < 0) lbSender.Items.Add(przenoszonyElement);
            else lbSender.Items.Insert(indeks, przenoszonyElement);
        }
    }
}
