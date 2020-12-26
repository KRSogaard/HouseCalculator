using System;
using System.Collections.Generic;
using System.Text;

namespace HouseCalculator.ViewModels
{
    public partial class MainViewModel
    {
        private string _purchasePrice;
        public string PurchasePriceField
        {
            get
            {
                return _purchasePrice;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(PurchasePriceField));
                    return;
                }

                PurchasePrice = tryParse;
                SetProperty(ref _purchasePrice, value);
                RaisePropertyChanged(nameof(LandValueText));
                RaisePropertyChanged(nameof(DownPaymentText));
                RaisePropertyChanged(nameof(EscrowText));
                RaisePropertyChanged(nameof(TotalClosing));
                RaisePropertyChanged(nameof(PropertyTaxText));
            }
        }

        private string _landValuePtc;
        public string LandValuePtcField
        {
            get
            {
                return _landValuePtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(LandValuePtcField));
                    return;
                }

                LandValuePtc = tryParse / 100;
                SetProperty(ref _landValuePtc, value);
                RaisePropertyChanged(nameof(LandValueText));
            }
        }

        private string _downPaymentPtc;
        public string DownPaymentPtcField
        {
            get
            {
                return _downPaymentPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(DownPaymentPtcField));
                    return;
                }

                DownPaymentPtc = tryParse / 100;
                SetProperty(ref _downPaymentPtc, value);
                RaisePropertyChanged(nameof(DownPaymentText));
            }
        }

        private string _loanFee;
        public string LoanFeeField
        {
            get
            {
                return _loanFee;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(LoanFeeField));
                    RaisePropertyChanged(nameof(TotalClosing));
                    return;
                }

                LoanFee = tryParse;
                SetProperty(ref _loanFee, value);
            }
        }

        private string _escrowPtc;
        public string EscrowPtcField
        {
            get
            {
                return _escrowPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(EscrowPtcField));
                    return;
                }

                EscrowPtc = tryParse / 100;
                SetProperty(ref _escrowPtc, value);
                RaisePropertyChanged(nameof(EscrowText));
                RaisePropertyChanged(nameof(TotalClosing));
            }
        }

        private string _loanPtc;
        public string LoanPtcField
        {
            get
            {
                return _loanPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(EscrowPtcField));
                    return;
                }

                LoanPtc = tryParse / 100;
                SetProperty(ref _loanPtc, value);
                RaisePropertyChanged(nameof(LoanPtcField));
            }
        }

        private string _loanTerms;
        public string LoanTermsField
        {
            get
            {
                return _loanTerms;
            }
            set
            {
                int tryParse;
                if (!int.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(LoanTermsField));
                    return;
                }

                LoanTerms = tryParse;
                SetProperty(ref _loanTerms, value);
            }
        }

        private string _rentIncome;
        public string RentIncomeField
        {
            get
            {
                return _rentIncome;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(RentIncomeField));
                    return;
                }

                RentIncome = tryParse;
                SetProperty(ref _rentIncome, value);
                RaisePropertyChanged(nameof(ManagementFeeText));
            }
        }

        private string _remodelCost;
        public string RemodelCostField
        {
            get
            {
                return _remodelCost;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(RemodelCostField));
                    return;
                }

                RemodelCost = tryParse;
                SetProperty(ref _remodelCost, value);
                RaisePropertyChanged(nameof(TotalClosing));
            }
        }

        private string _remodelValueIncrease;
        public string RemodelValueIncreaseField
        {
            get
            {
                return _remodelValueIncrease;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(RemodelValueIncreaseField));
                    return;
                }

                RemodelValueIncrease = tryParse;
                SetProperty(ref _remodelValueIncrease, value);
                RaisePropertyChanged(nameof(TotalClosing));
            }
        }

        private string _managementFeePtc;
        public string ManagementFeePtcField
        {
            get
            {
                return _managementFeePtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(ManagementFeePtcField));
                    return;
                }

                ManagementFeePtc = tryParse / 100;
                SetProperty(ref _managementFeePtc, value);
                RaisePropertyChanged(nameof(ManagementFeeText));
            }
        }

        private string _maintenanceCostPtc;
        public string MaintenanceCostPtcField
        {
            get
            {
                return _maintenanceCostPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(MaintenanceCostPtcField));
                    return;
                }

                MaintenanceCostPtc = tryParse / 100;
                SetProperty(ref _maintenanceCostPtc, value);
                RaisePropertyChanged(nameof(MaintenanceCostText));
            }
        }

        private string _taxRatePtc;
        public string TaxRatePtcField
        {
            get
            {
                return _taxRatePtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(EscrowPtcField));
                    return;
                }

                TaxRatePtc = tryParse / 100;
                SetProperty(ref _taxRatePtc, value);
                RaisePropertyChanged(nameof(TaxRatePtcField));
            }
        }

        private string _propertyTexPtc;
        public string PropertyTaxPtcField
        {
            get
            {
                return _propertyTexPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(PropertyTaxPtcField));
                    return;
                }

                PropertyTaxPtc = tryParse / 100;
                SetProperty(ref _propertyTexPtc, value);
                RaisePropertyChanged(nameof(PropertyTaxText));
            }
        }

        private string _vacancyRatePtc;
        public string VacancyRatePtcField
        {
            get
            {
                return _vacancyRatePtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(VacancyRatePtcField));
                    return;
                }

                VacancyRatePtc = tryParse / 100;
                SetProperty(ref _vacancyRatePtc, value);
            }
        }

        private string _annualAppreciationPtc;
        public string AnnualAppreciationPtcField
        {
            get
            {
                return _annualAppreciationPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(AnnualAppreciationPtcField));
                    return;
                }

                AnnualAppreciationPtc = tryParse / 100;
                SetProperty(ref _annualAppreciationPtc, value);
            }
        }

        private string _annualRentPtc;
        public string AnnualRentPtcField
        {
            get
            {
                return _annualRentPtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(AnnualRentPtcField));
                    return;
                }

                AnnualRentPtc = tryParse / 100;
                SetProperty(ref _annualRentPtc, value);
            }
        }

        private string _annualUtilities;
        public string AnnualUtilitiesField
        {
            get
            {
                return _annualUtilities;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(AnnualUtilitiesField));
                    return;
                }

                AnnualUtilities = tryParse;
                SetProperty(ref _annualUtilities, value);
            }
        }

        private string _annualInsurance;
        public string AnnualInsuranceField
        {
            get
            {
                return _annualInsurance;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(AnnualInsuranceField));
                    return;
                }

                AnnualInsurance = tryParse;
                SetProperty(ref _annualInsurance, value);
            }
        }

        private string _annualOtherCost;
        public string AnnualOtherCostField
        {
            get
            {
                return _annualOtherCost;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(AnnualOtherCostField));
                    return;
                }

                AnnualOtherCost = tryParse;
                SetProperty(ref _annualOtherCost, value);
            }
        }

        private string _salesFeePtc;
        public string SalesFeePtcField
        {
            get
            {
                return _salesFeePtc;
            }
            set
            {
                double tryParse;
                if (!double.TryParse(value, out tryParse))
                {
                    RaisePropertyChanged(nameof(SalesFeePtcField));
                    return;
                }

                SalesFeePtc = tryParse / 100;
                SetProperty(ref _salesFeePtc, value);
            }
        }
    }
}
