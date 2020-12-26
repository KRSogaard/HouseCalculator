using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using Prism.Commands;
using Prism.Mvvm;

namespace HouseCalculator
{
    /// <summary>
    /// Interaction logic for InputItemUserControl.xaml
    /// </summary>
    public partial class InputItemUserControl : UserControl
    {
        // public InputItemViewModel ViewModel { get; private set; }
        
        public InputItemUserControl()
        {
            InitializeComponent();
            // ViewModel = new InputItemViewModel();
            // DataContext = ViewModel;
        }

        public static readonly DependencyProperty OnEnterProperty =
            DependencyProperty.Register("OnEnter", typeof(ICommand), typeof(InputItemUserControl), new FrameworkPropertyMetadata(
                new DelegateCommand(() => { })));

        public ICommand OnEnter
        {
            get
            {
                return this.GetValue(OnEnterProperty) as ICommand;
            }
            set
            {
                this.SetValue(OnEnterProperty, value);
            }
        }


        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(InputItemUserControl));

        public string Label
        {
            get
            {
                return this.GetValue(LabelProperty) as string;
            }
            set
            {
                this.SetValue(LabelProperty, value);
            }
        }

        public static readonly DependencyProperty PrefixProperty =
            DependencyProperty.Register("Prefix", typeof(string), typeof(InputItemUserControl));

        public string Prefix
        {
            get
            {
                return this.GetValue(LabelProperty) as string;
            }
            set
            {
                this.SetValue(LabelProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(InputItemUserControl), new FrameworkPropertyMetadata(
                null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Value
        {
            get
            {
                return this.GetValue(ValueProperty) as string;
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty HelpTextProperty =
            DependencyProperty.Register("Help", typeof(string), typeof(InputItemUserControl));

        public string Help
        {
            get
            {
                return this.GetValue(HelpTextProperty) as string;
            }
            set
            {
                this.SetValue(HelpTextProperty, value);
            }
        }

        public static readonly DependencyProperty InputSizeProperty =
            DependencyProperty.Register("InputSize", typeof(string), typeof(InputItemUserControl), new PropertyMetadata("75px"));

        public string InputSize
        {
            get
            {
                return this.GetValue(InputSizeProperty) as string;
            }
            set
            {
                this.SetValue(InputSizeProperty, value);
            }
        }

        // private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        // {
        //     InputItemUserControl uc = d as InputItemUserControl;
        //     switch (e.Property.Name)
        //     {
        //         case "InputSize":
        //             uc.ViewModel.InputSize = uc.GetValue(InputSizeProperty) as string;
        //             break;
        //         case "Help":
        //             uc.ViewModel.Help = uc.GetValue(HelpTextProperty) as string;
        //             break;
        //         case "Value":
        //             uc.ViewModel.Value = uc.GetValue(ValueProperty) as string;
        //             break;
        //         case "Prefix":
        //             uc.ViewModel.Prefix = uc.GetValue(PrefixProperty) as string;
        //             break;
        //         case "Label":
        //             uc.ViewModel.Label = uc.GetValue(LabelProperty) as string;
        //             break;
        //     }
        // }
        //
        // public class InputItemViewModel : BindableBase
        // {
        //     public string Label { get; set; }
        //     public string Prefix { get; set; }
        //     public string Value { get; set; }
        //     public string Help { get; set; }
        //     public string InputSize { get; set; }
        // }
    }
}
