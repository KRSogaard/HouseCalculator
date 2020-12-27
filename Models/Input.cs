using System;
using System.Collections.Generic;
using System.Text;

namespace HouseCalculator.Models
{
    public class Input
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
    }
}
