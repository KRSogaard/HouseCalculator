using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HouseCalculator.ViewModels
{
    public class DownViewModel : BindableBase
    {
        public DownViewModel()
        {
            Procentages = new ObservableCollection<DownPrcViewModel>();
        }

        private ObservableCollection<DownPrcViewModel> _procentages;
        public ObservableCollection<DownPrcViewModel> Procentages
        {
            get { return _procentages; }
            set { SetProperty(ref _procentages, value); }
        }
    }

    public class DownPrcViewModel : BindableBase
    {
        private HouseSimulator.SimulationYear year;
        private HouseSimulator.SimulationResult result;
        private double _pct;

        public DownPrcViewModel(double pct, HouseSimulator.SimulationResult result, HouseSimulator.SimulationYear year)
        {
            _pct = pct;
            this.year = year;
            this.result = result;
        }
        public String PCT => Math.Round(_pct * 100, 2) + "%";
        public String CIHROIText => Math.Round((year.CashInHand / result.OutOfPocket) * 100, 2) + "%";
        public String AtSaleText => Math.Round(((year.ValueAtSale / result.OutOfPocket) * 100) / year.Year, 2) + "%";
        public String IntrestPaid => "$" + String.Format("{0:n}", Math.Round(year.IntrestPaid));
        public String TaxPaid => "$" + String.Format("{0:n}", Math.Round(year.TaxPaid));
        public String MonthlyCIH => "$" + String.Format("{0:n}", Math.Round(year.CashInHand / 12));
    }
}
