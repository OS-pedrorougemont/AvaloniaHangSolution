﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaHangProject.Views;
using ReactiveUI;

namespace AvaloniaHangProject.ViewModels {
    public class MainWindowViewModel : ReactiveObject {
        private BaseGuidedTourDialogWindow guidedTourDialogWindow;

        public MainWindowViewModel(Window parent) {
            OpenGuidedTourExampleCommand = ReactiveCommand.Create(
                () => {
                    guidedTourDialogWindow?.Close();

                    // To fix "Cannot re-show a closed window" we create a new window
                    guidedTourDialogWindow = new BaseGuidedTourDialogWindow(parent) { MinHeight = 100,  MinWidth = 100,};
                    
                    var innerContent = new Border() { BorderBrush = new SolidColorBrush(Colors.Red), BorderThickness = new Thickness(3)};
                    Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith((t) => {
                        Dispatcher.UIThread.InvokeAsync(() => {
                            ((Border)guidedTourDialogWindow.Content).Width = 500;
                            ((Border)guidedTourDialogWindow.Content).Height = 500;
                        });
                    });
                    
                    innerContent.LayoutUpdated += (sender, args) => {
                        guidedTourDialogWindow.SetDialogStartupLocation();
                        
                        Debug.WriteLine("MainWindowViewModel#LayoutUpdated");
                    };

                    guidedTourDialogWindow.Content = innerContent;
                    guidedTourDialogWindow.ShowWindow();
                    guidedTourDialogWindow.SetDialogStartupLocation();
                }
            );

            CloseGuidedTourExampleCommand = ReactiveCommand.Create(
                () => {
                    guidedTourDialogWindow.Close();
                }
            );
        }

        public ICommand OpenGuidedTourExampleCommand { get; }
        public ICommand CloseGuidedTourExampleCommand { get; }
    }
}