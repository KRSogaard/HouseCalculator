using System;
using System.Collections.Generic;
using System.Text;
using HouseCalculator.Models;
using Prism.Mvvm;

namespace HouseCalculator.ViewModels
{
    public class SavedInputViewModel : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private Input _input;
        public Input Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }
    }
}
