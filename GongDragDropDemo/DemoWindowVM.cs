using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace GongDragDropDemo
{
    public partial class DemoWindowVM : ObservableObject
    {

        [ObservableProperty]
        private int _index = 0;

        public ICommand ShowTextCommand => new RelayCommand(ShowMain);

        private void ShowMain()
        {
            MainWindow textDisplayWindow = new MainWindow();
            textDisplayWindow.ShowDialog();
        }

        [RelayCommand]
        private async Task GoodClick()
        {
            await Task.Delay(100);
            ++Index;
            //MainWindow textDisplayWindow = new MainWindow();
            //textDisplayWindow.ShowDialog();
        }
    }
}
