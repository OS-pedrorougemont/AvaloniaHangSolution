using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaMenuPoC.Views;
using ReactiveUI;

namespace AvaloniaMenuPoC.ViewModels {
    public class MainWindowViewModel : ReactiveObject {
        private MainWindow _mainWindow;

        public MainWindowViewModel(Window parent) {
            OpenGuidedTourExampleCommand = ReactiveCommand.Create(
                () => {
                   _mainWindow = new MainWindow() { Background = Brushes.White };
                    
                    var innerContent = new Border() { BorderBrush = new SolidColorBrush(Colors.Red), BorderThickness = new Thickness(3)};
                    
                    _mainWindow.Content = innerContent;
                }
            );

            ReactiveCommand.Create(
                () => {
                    _mainWindow.Close();
                }
            );
        }

        public MainWindowViewModel() {
            
        }

        public ICommand OpenGuidedTourExampleCommand { get; }
    }
}