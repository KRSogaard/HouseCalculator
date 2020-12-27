using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HouseCalculator.ViewModels;
using static HouseCalculator.HouseSimulator;

namespace HouseCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            //
            // HouseSimulator houseSimulator = new HouseSimulator();
            // SimulationResult result = houseSimulator.Simulate(new HouseSimulator.HouseSimulatorInput()
            // {
            //     PurchasePriceField = 380000.0,
            //     LandValue = 114000.0,
            //     DownPayment = 38000.0,
            //     ClosingCost = 4800.0,
            //     RemodelCostField = 20000.0,
            //     RemodelValueIncreaseField = 20000.0,
            //     LoanRatePtc = 0.03,
            //     LoanTermYears = 30,
            //     ManagementFeePtcField = 0.0,
            //     MaintenanceCostPtcField = 0.05,
            //     MonthlyRentIncomePerMonth = 2400.0,
            //     TaxRatePtcField = 0.21,
            //     PropertyTaxPtcField = 0.0064,
            //     DepreciationOverYears = 27.5,
            //     VacancyRatePtcField = 0.1,
            //     ExpensesMonthly = 3960 / 12,
            //     FeesAtSalePtc = 0.05,
            //     AnnualAppreciationPtcField = 0.01,
            //     AnnualRentIncreasePtc = 0.01
            // }, 30);
        }
    }
}
