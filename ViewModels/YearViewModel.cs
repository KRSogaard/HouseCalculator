using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace HouseCalculator.ViewModels
{
    public class YearViewModel : BindableBase
    {
        private HouseSimulator.SimulationYear year;
        private HouseSimulator.SimulationResult result;

        public int Year => year.Year;
        public string MonthlyCIH => "$" + String.Format("{0:n}", Math.Round(year.CashInHand / 12, 2));
        public string MonthlyCIHPtc =>  Math.Round((year.CashInHand / result.OutOfPocket) * 100, 2) + "%";
        public string SalePtc => Math.Round(((year.ValueAtSale / result.OutOfPocket) * 100) / year.Year, 2) + "%";
        public string SaleValue => "$" + String.Format("{0:n}", Math.Round(year.ValueAtSale, 2));
        public string NetIncome => "$" + String.Format("{0:n}", Math.Round(year.NetIncome, 2));
        public string TaxPaid => "$" + String.Format("{0:n}", Math.Round(year.TaxPaid, 2));
        public string ValueGain => "$" + String.Format("{0:n}", Math.Round(year.ValueGain, 2));
        public string TotalValueGain => "$" + String.Format("{0:n}", Math.Round(year.TotalValueGain, 2));
        public string TotalNetIncome => "$" + String.Format("{0:n}", Math.Round(year.TotalNetIncome, 2));

        public static YearViewModel From(HouseSimulator.SimulationYear simulationYear, HouseSimulator.SimulationResult result)
        {
            return new YearViewModel()
            {
                year = simulationYear,
                result = result
            };
        }
    }
}
