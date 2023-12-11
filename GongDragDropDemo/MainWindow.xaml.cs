using GongSolutions.Wpf.DragDrop;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GongDragDropDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDropTarget
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            
        }
    }
}