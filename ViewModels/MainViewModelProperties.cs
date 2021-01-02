using System;
using System.Collections.Generic;
using System.Text;

namespace HouseCalculator.ViewModels
{
    public partial class MainViewModel
    {
        private double parseValue(string value, ref string prop, string callMember)
        {
            if (String.IsNullOrEmpty(value))
            {
                SetProperty(ref prop, "", callMember);
                return 0;
            }
            if (value == ".")
            {
                SetProperty(ref prop, ".", callMember);
                return 0;
            }
            
            if (value.StartsWith(","))
            {
                value = value.Substring(1);
            }
            double tryParse;
            if (!double.TryParse(value, out tryParse))
            {
                RaisePropertyChanged(callMember);
                return double.Parse(prop);
            }
            if (tryParse == 0)
            {
                SetProperty(ref prop, value, callMember);
                return 0;
            }

            String newTextValue = String.Format("{0:n0}", tryParse - (tryParse % 1));
            if (tryParse % 1 > 0 || value.EndsWith("."))
            {
                if (value.StartsWith("."))
                {
                    newTextValue = value;
                }
                else
                {
                    newTextValue = newTextValue + ".";
                    if (!value.EndsWith("."))
                    {
                        newTextValue = newTextValue + value.Split(".")[1];
                    }
                }
            }

            SetProperty(ref prop, newTextValue, callMember);
            return tryParse;
        }
        private int parseIntValue(string value, ref string prop, string callMember)
        {
            if (String.IsNullOrEmpty(value))
            {
                SetProperty(ref prop, "", callMember);
                return 0;
            }

            int tryParse;
            if (!int.TryParse(value, out tryParse))
            {
                RaisePropertyChanged(callMember);
                return int.Parse(prop);
            }

            SetProperty(ref prop, String.Format("{0:n0}", tryParse), callMember);
            return tryParse;
        }

        private string _purchasePrice;
        public string PurchasePriceField
        {
            get
            {
                return _purchasePrice;
            }
            set
            {
                PurchasePrice = parseValue(value, ref _purchasePrice, nameof(PurchasePriceField));
                RaisePropertyChanged(nameof(LandValueText));
                RaisePropertyChanged(nameof(DownPaymentText));
                RaisePropertyChanged(nameof(ManagementFeeText));
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
                double tryParse = parseValue(value, ref _landValuePtc, nameof(LandValuePtcField));
                LandValuePtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _downPaymentPtc, nameof(DownPaymentPtcField));
                DownPaymentPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _loanFee, nameof(LoanFeeField));
                LoanFee = tryParse;
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
                double tryParse = parseValue(value, ref _escrowPtc, nameof(EscrowPtcField));
                EscrowPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _loanPtc, nameof(LoanPtcField));
                LoanPtc = tryParse / 100;
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
                int tryParse = parseIntValue(value, ref _loanTerms, nameof(LoanTermsField));
                LoanTerms = tryParse;
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
                double tryParse = parseValue(value, ref _rentIncome, nameof(RentIncomeField));
                RentIncome = tryParse;
                RaisePropertyChanged(nameof(ManagementFeeText));
                RaisePropertyChanged(nameof(MaintenanceCostText));
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
                double tryParse = parseValue(value, ref _remodelCost, nameof(RemodelCostField));
                RemodelCost = tryParse;
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
                double tryParse = parseValue(value, ref _remodelValueIncrease, nameof(RemodelValueIncreaseField));
                RemodelValueIncrease = tryParse;
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
                double tryParse = parseValue(value, ref _managementFeePtc, nameof(ManagementFeePtcField));
                ManagementFeePtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _maintenanceCostPtc, nameof(MaintenanceCostPtcField));
                MaintenanceCostPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _taxRatePtc, nameof(TaxRatePtcField));
                TaxRatePtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _propertyTexPtc, nameof(PropertyTaxPtcField));
                PropertyTaxPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _vacancyRatePtc, nameof(VacancyRatePtcField));
                VacancyRatePtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _annualAppreciationPtc, nameof(AnnualAppreciationPtcField));
                AnnualAppreciationPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _annualRentPtc, nameof(AnnualRentPtcField));
                AnnualRentPtc = tryParse / 100;
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
                double tryParse = parseValue(value, ref _annualUtilities, nameof(AnnualUtilitiesField));
                AnnualUtilities = tryParse;
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
                double tryParse = parseValue(value, ref _annualInsurance, nameof(AnnualInsuranceField));
                AnnualInsurance = tryParse;
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
                double tryParse = parseValue(value, ref _annualOtherCost, nameof(AnnualOtherCostField));
                AnnualOtherCost = tryParse;
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
                double tryParse = parseValue(value, ref _salesFeePtc, nameof(SalesFeePtcField));
                SalesFeePtc = tryParse / 100;
            }
        }
    }
}
