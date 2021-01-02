using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using HouseCalculator.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace HouseCalculator.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        public double PurchasePrice { get; set; }
        public double LandValuePtc { get; set; }
        public double DownPaymentPtc { get; set; }
        public double LoanFee { get; set; }
        public double EscrowPtc { get; set; }
        public double LoanPtc { get; set; }
        public int LoanTerms { get; set; }
        public double RentIncome { get; set; }
        public double RemodelCost { get; set; }
        public double RemodelValueIncrease { get; set; }
        public double ManagementFeePtc { get; set; }
        public double MaintenanceCostPtc { get; set; }
        public double TaxRatePtc { get; set; }
        public double PropertyTaxPtc { get; set; }
        public double VacancyRatePtc { get; set; }
        public double AnnualAppreciationPtc { get; set; }
        public double AnnualRentPtc { get; set; }
        public double AnnualUtilities { get; set; }
        public double AnnualInsurance { get; set; }
        public double AnnualOtherCost { get; set; }
        public double SalesFeePtc { get; set; }

        public string TotalClosing => "$" + String.Format("{0:n0}", Math.Round(PurchasePrice * EscrowPtc + LoanFee + PurchasePrice * DownPaymentPtc + RemodelCost, 2));
        public string ManagementFeeText => "$" + String.Format("{0:n0}", Math.Round(RentIncome * ManagementFeePtc, 2));
        public string PropertyTaxText => "$" + String.Format("{0:n0}", Math.Round(PurchasePrice * PropertyTaxPtc, 2));
        public string MaintenanceCostText => "$" + String.Format("{0:n0}", Math.Round(RentIncome * MaintenanceCostPtc, 2));
        public string EscrowText => "$" + String.Format("{0:n0}", Math.Round(PurchasePrice * EscrowPtc, 2));
        public string DownPaymentText => "$" + String.Format("{0:n0}", Math.Round(PurchasePrice * DownPaymentPtc, 2));
        public string LandValueText => "$" + String.Format("{0:n0}", Math.Round(PurchasePrice * LandValuePtc, 2));

        private DownViewModel _downPaymentAnalyst;
        public DownViewModel DownPaymentAnalyst
        {
            get { return _downPaymentAnalyst; }
            set
            {
                SetProperty(ref _downPaymentAnalyst, value);
            }
        }

        private ObservableCollection<YearViewModel> _years;
        public ObservableCollection<YearViewModel> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MonthViewModel> _months;

        public ObservableCollection<MonthViewModel> Months
        {
            get { return _months; }
            set
            {
                _months = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<SavedInputViewModel> _savedInputs;

        public ObservableCollection<SavedInputViewModel> SavedInputs
        {
            get => _savedInputs;
            set => SetProperty(ref _savedInputs, value);
        }

        private SavedInputViewModel _selectedInputModel;

        public SavedInputViewModel SelectedInputModel
        {
            get => _selectedInputModel;
            set => SetProperty(ref _selectedInputModel, value);
        }

        public ICommand _loadInputCommand;
        public ICommand LoadInputCommand
        {
            get
            {
                if (_loadInputCommand == null)
                {
                    _loadInputCommand = new DelegateCommand(() =>
                    {
                        if (SelectedInputModel == null)
                        {
                            return;
                        }

                        From(SelectedInputModel.Input);
                    });
                }

                return _loadInputCommand;
            }
        }

        public ICommand _deleteInputCommand;
        public ICommand DeleteInputCommand
        {
            get
            {
                if (_deleteInputCommand == null)
                {
                    _deleteInputCommand = new DelegateCommand(() =>
                    {
                        if (SelectedInputModel == null)
                        {
                            return;
                        }

                        SavedInputs.Remove(SelectedInputModel);
                        SaveInput();
                    });
                }

                return _deleteInputCommand;
            }
        }



        private string _savedName;
        public string SavedName
        {
            get => _savedName;
            set
            {
                SetProperty(ref _savedName, value);
                RaisePropertyChanged(nameof(CanSaveInput));
            }
        }

        public ICommand _saveInputCommand;
        public ICommand SaveInputCommand
        {
            get
            {
                if (_saveInputCommand == null)
                {
                    _saveInputCommand = new DelegateCommand(() =>
                    {
                        if (!CanSaveInput)
                        {
                            return;
                        }

                        SavedInputViewModel saved = new SavedInputViewModel()
                        {
                            Name = SavedName,
                            Input = toInput()
                        };
                        SavedInputs.Add(saved);
                        SelectedInputModel = saved;
                        SaveInput();
                        SavedName = "";
                    });
                }

                return _saveInputCommand;
            }
        }

        private void SaveInput()
        {
            Dictionary<string, Input> models = new Dictionary<string, Input>();
            foreach (SavedInputViewModel model in SavedInputs)
            {
                String name = model.Name.Trim();
                if (!models.ContainsKey(name))
                {
                    models.Add(model.Name.Trim(), model.Input);
                }
            }

            string jsonString = JsonSerializer.Serialize(models);
            File.WriteAllText("SavedInput.json", jsonString);
        }

        private void LoadInput()
        {
            SavedInputs.Clear();
            if (!File.Exists("SavedInput.json"))
            {
                return;
            }
            string jsonString = File.ReadAllText("SavedInput.json");
            Dictionary<string, Input> models = JsonSerializer.Deserialize<Dictionary<string, Input>>(jsonString);
            foreach (var key in models.Keys)
            {
                SavedInputs.Add(new SavedInputViewModel()
                {
                    Name = key,
                    Input = models[key]
                });
            }
        }

        public bool CanSaveInput => SavedName != null && SavedName.Trim().Length > 2 && !SavedInputs.Any(i => i.Name.Equals(SavedName.Trim()));


        public ICommand _calculateCommand;

        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                {
                    _calculateCommand = new DelegateCommand(() =>
                    {
                        HouseSimulator.HouseSimulatorInput input = getCurrentInput();
                        HouseSimulator simulator = new HouseSimulator();
                        HouseSimulator.SimulationResult result = simulator.Simulate(input, 30);

                        Years.Clear();
                        result.Years.ForEach(y => Years.Add(YearViewModel.From(y, result)));
                        Months.Clear();
                        result.Months.ForEach(m => Months.Add(MonthViewModel.From(m, result)));


                        DownPaymentAnalyst.Procentages.Clear();
                        for (double down = 0.01; down <= 1.0001; down = down + 0.01)
                        {
                            input.DownPayment = PurchasePrice * down;
                            result = simulator.Simulate(input, 5);

                            var year5 = result.Years.Where(x => x.Year == 5).First();
                            DownPaymentAnalyst.Procentages.Add(new DownPrcViewModel(down, result, year5));
                        }
                    });
                }

                return _calculateCommand;
            }
        }

        private HouseSimulator.HouseSimulatorInput getCurrentInput()
        {
            HouseSimulator.HouseSimulatorInput input = new HouseSimulator.HouseSimulatorInput();
            input.PurchasePrice = PurchasePrice;
            input.LandValue = PurchasePrice * LandValuePtc;
            input.DownPayment = PurchasePrice * DownPaymentPtc;
            input.ClosingCost = LoanFee + PurchasePrice * EscrowPtc;
            input.RemodelCost = RemodelCost;
            input.RemodelValueIncrease = RemodelValueIncrease;
            input.LoanRatePtc = LoanPtc;
            input.LoanTermYears = LoanTerms;
            input.ManagementFeePtc = ManagementFeePtc;
            input.MaintenanceCostPtc = MaintenanceCostPtc;
            input.MonthlyRentIncomePerMonth = RentIncome;
            input.TaxRatePtc = TaxRatePtc;
            input.PropertyTaxPtc = PropertyTaxPtc;
            input.DepreciationOverYears = 27.5;
            input.VacancyRatePtc = VacancyRatePtc;
            input.ExpensesMonthly = (AnnualUtilities + AnnualInsurance + AnnualOtherCost) / 12;
            input.FeesAtSalePtc = SalesFeePtc;
            input.AnnualAppreciationPtc = AnnualAppreciationPtc;
            input.AnnualRentIncreasePtc = AnnualRentPtc;

            return input;
        }

        public MainViewModel()
        {
            PurchasePriceField = "1500000";
            LandValuePtcField = "60";
            DownPaymentPtcField = "20";
            LoanFeeField = "5000";
            EscrowPtcField = "1";
            LoanPtcField = "3.5";
            LoanTermsField = "30";
            RentIncomeField = "10800";
            RemodelCostField = "80000";
            RemodelValueIncreaseField = "300000";
            ManagementFeePtcField = "0";
            MaintenanceCostPtcField = "5";
            TaxRatePtcField = "25";
            PropertyTaxPtcField = "1.25";
            VacancyRatePtcField = "5";
            AnnualAppreciationPtcField = "1";
            AnnualRentPtcField = "1";
            AnnualUtilitiesField = "2700";
            AnnualInsuranceField = "2300";
            AnnualOtherCostField = "1200";
            SalesFeePtcField = "3";

            Years = new ObservableCollection<YearViewModel>();
            Months = new ObservableCollection<MonthViewModel>();
            SavedInputs = new ObservableCollection<SavedInputViewModel>();
            LoadInput();
            if (SavedInputs.Count != 0)
            {
                SelectedInputModel = SavedInputs[0];
            }
            DownPaymentAnalyst = new DownViewModel();
        }

        public Input toInput()
        {
            return new Input()
            {
                PurchasePrice = PurchasePrice,
                LandValuePtc = LandValuePtc,
                DownPaymentPtc = DownPaymentPtc,
                LoanFee = LoanFee,
                EscrowPtc = EscrowPtc,
                LoanPtc = LoanPtc,
                LoanTerms = LoanTerms,
                RentIncome = RentIncome,
                RemodelCost = RemodelCost,
                RemodelValueIncrease = RemodelValueIncrease,
                ManagementFeePtc = ManagementFeePtc,
                MaintenanceCostPtc = MaintenanceCostPtc,
                TaxRatePtc = TaxRatePtc,
                PropertyTaxPtc = PropertyTaxPtc,
                VacancyRatePtc = VacancyRatePtc,
                AnnualAppreciationPtc = AnnualAppreciationPtc,
                AnnualRentPtc = AnnualRentPtc,
                AnnualUtilities = AnnualUtilities,
                AnnualInsurance = AnnualInsurance,
                AnnualOtherCost = AnnualOtherCost,
                SalesFeePtc = SalesFeePtc
            };
        }

        public void From(Input input)
        {
            PurchasePriceField = input.PurchasePrice.ToString();
            LandValuePtcField = input.LandValuePtc.ToString();
            DownPaymentPtcField = input.DownPaymentPtc.ToString();
            LoanFeeField = input.LoanFee.ToString();
            EscrowPtcField = input.EscrowPtc.ToString();
            LoanPtcField = input.LoanPtc.ToString();
            LoanTermsField = input.LoanTerms.ToString();
            RentIncomeField = input.RentIncome.ToString();
            RemodelCostField = input.RemodelCost.ToString();
            RemodelValueIncreaseField = input.RemodelValueIncrease.ToString();
            ManagementFeePtcField = input.ManagementFeePtc.ToString();
            MaintenanceCostPtcField = input.MaintenanceCostPtc.ToString();
            TaxRatePtcField = input.TaxRatePtc.ToString();
            PropertyTaxPtcField = input.PropertyTaxPtc.ToString();
            VacancyRatePtcField = input.VacancyRatePtc.ToString();
            AnnualAppreciationPtcField = input.AnnualAppreciationPtc.ToString();
            AnnualRentPtcField = input.AnnualRentPtc.ToString();
            AnnualUtilitiesField = input.AnnualUtilities.ToString();
            AnnualInsuranceField = input.AnnualInsurance.ToString();
            AnnualOtherCostField = input.AnnualOtherCost.ToString();
            SalesFeePtcField = input.SalesFeePtc.ToString();
        }
    }
}
