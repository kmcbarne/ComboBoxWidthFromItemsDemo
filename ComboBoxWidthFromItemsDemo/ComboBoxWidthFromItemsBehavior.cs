using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ComboBoxWidthFromItemsDemo
{
    /// <summary>
    /// Code originally sourced from:  https://stackoverflow.com/questions/1034505/how-can-i-make-a-wpf-combo-box-have-the-width-of-its-widest-element-in-xaml
    /// </summary>
    public static class ComboBoxWidthFromItemsBehavior
    {
        public static readonly DependencyProperty ComboBoxWidthFromItemsProperty = DependencyProperty.RegisterAttached("ComboBoxWidthFromItems", typeof(bool),
            typeof(ComboBoxWidthFromItemsBehavior), new UIPropertyMetadata(false, OnComboBoxWidthFromItemsPropertyChanged));

        public static bool GetComboBoxWidthFromItems(DependencyObject obj)
        {
            return (bool)obj.GetValue(ComboBoxWidthFromItemsProperty);
        }

        public static void SetComboBoxWidthFromItems(DependencyObject obj, bool value)
        {
            obj.SetValue(ComboBoxWidthFromItemsProperty, value);
        }

        private static void OnComboBoxWidthFromItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox box = d as ComboBox;

            if(box != null)
            {
                if((bool)e.NewValue == true)
                    box.Loaded += OnComboBoxLoaded;
                else
                    box.Loaded -= OnComboBoxLoaded;
            }
        }

        private static void OnComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            Action action = () => { box.SetWidthFromItems(); };
            box.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
        }
    }
}
