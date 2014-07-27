using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace UmbracoDiff.Extensions
{
    public static class WindowManagerExtensions
    {
        public static MetroWindow GetMetroWindow(this IWindowManager windowManager)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            return window;
        }
    }
}
