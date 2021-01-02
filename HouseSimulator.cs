﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseCalculator
{
    public class HouseSimulator
    {
        public SimulationResult Simulate(HouseSimulatorInput input, int years)
        {
            DateTime start = DateTime.Now;
            
            SimulationResult result = new SimulationResult();

            double totalLoan = input.PurchasePrice - input.DownPayment;
            double loanRemaining = totalLoan;

            double currentRent = input.MonthlyRentIncomePerMonth;
            double currentHousePrice = input.PurchasePrice + input.RemodelValueIncrease;
            double depreciation = (input.PurchasePrice - input.LandValue) / input.DepreciationOverYears / 12;
            double taxCredit = input.RemodelCost + input.ClosingCost;
            double totalProfit = 0;

            result.OutOfPocket = input.DownPayment + input.ClosingCost + input.RemodelCost;

            for (int i = 1; i <= years * 12; i++)
            {
                SimulationMonth simulationMonth = new SimulationMonth();
                
                double principal = -Math.Round(Financial.PPmt(input.LoanRatePtc / 12, i, input.LoanTermYears * 12, totalLoan), 2);
                double intrest = -Math.Round(Financial.IPmt(input.LoanRatePtc / 12, i, input.LoanTermYears * 12, totalLoan), 2);
                simulationMonth.PrincipalPaid = principal;
                simulationMonth.IntrestPaid = intrest;

                double monthlyPropertyTax = input.PurchasePrice * (input.PropertyTaxPtc / 12.0);
                double monthlyMaintenance = input.MaintenanceCostPtc * currentRent;
                double monthlyManagement = input.ManagementFeePtc * currentRent;
                double monthlyExpense = intrest + input.ExpensesMonthly + monthlyMaintenance + monthlyManagement + monthlyPropertyTax;

                simulationMonth.PropertyTax = monthlyPropertyTax;
                simulationMonth.Expenses = monthlyExpense;
                simulationMonth.OutOfPocket = principal + monthlyExpense;

                simulationMonth.RentIncome = currentRent;
                double monthlyProfit = currentRent * (1 - input.VacancyRatePtc) - monthlyExpense;
                totalProfit += monthlyProfit;
                double monthlyDepreciation = 0;
                if (i / 12.0 <= input.DepreciationOverYears)
                {
                    monthlyDepreciation = depreciation;
                }

                simulationMonth.NetIncome = monthlyProfit;
                double tax = 0;
                if (monthlyProfit > 0)
                {
                    double taxableIncome = monthlyProfit - monthlyDepreciation;
                    if (taxableIncome > 0)
                    {
                        if (taxCredit > taxableIncome)
                        {
                            taxCredit -= taxableIncome;
                            taxableIncome = 0;
                        }
                        else
                        {
                            taxableIncome -= taxCredit;
                            taxCredit = 0;

                        }
                        simulationMonth.TaxableIncome = taxableIncome;
                        tax = taxableIncome * input.TaxRatePtc;
                    }
                    else
                    {
                        taxCredit += -taxableIncome;
                        tax = 0;
                    }
                }
                else
                {
                    tax = 0;
                    taxCredit += -monthlyProfit + monthlyDepreciation;
                    simulationMonth.TaxableIncome = 0;
                }

                simulationMonth.Tax = tax;
                simulationMonth.TaxCredit = taxCredit;

                double cashInHand = (currentRent - monthlyExpense) * (1 - input.VacancyRatePtc) - principal - tax;
                double valueGain = monthlyProfit - tax;

                // We should not include the remodel cost in the cash in hand returns
                simulationMonth.CashInHand = cashInHand;
                simulationMonth.ValueGain = valueGain;

                loanRemaining -= principal;

                currentHousePrice *= 1 + input.AnnualAppreciationPtc / 12;


                double costToSale = currentHousePrice * input.FeesAtSalePtc;
                double cashAtSalePreTax = currentHousePrice - costToSale - input.DownPayment - loanRemaining;
                // We don't add the remodel and closing cost and that should be in the taxCredt
                double taxableAtSale = cashAtSalePreTax - taxCredit;
                if (taxableAtSale < 0)
                {
                    taxableAtSale = 0;
                }

                double taxAtSale = calculateLongTermCapitalGainsTax(taxableAtSale);
                double cashAtSale = cashAtSalePreTax - taxAtSale - input.RemodelCost - input.ClosingCost;
                double valueAtSale = cashAtSale + totalProfit;

                simulationMonth.CashAtSale = cashAtSale;
                simulationMonth.ValueAtSale = valueAtSale;

                if (i % 12 == 0)
                {
                    currentRent *= 1 + input.AnnualRentIncreasePtc;
                }

                simulationMonth.Month = i;
                simulationMonth.Year = (int) Math.Floor(i / 12.0);
                result.Months.Add(simulationMonth);

                if (i % 12 == 0)
                {
                    double yearlyCashInHand = 0;
                    double yearlyNetIncome = 0;
                    double yearlyTaxPaid = 0;
                    double yearlyValueGain = 0;
                    double yearlyIntrestPaid = 0;
                    double yearlyPrincipalPaid = 0;
                    double yearlyPropertyTax = 0;
                    for (int q = result.Months.Count - 12; q < result.Months.Count; q++)
                    {
                        yearlyCashInHand += result.Months[q].CashInHand;
                        yearlyTaxPaid += result.Months[q].Tax;
                        yearlyNetIncome += result.Months[q].NetIncome;
                        yearlyValueGain += result.Months[q].ValueGain;
                        yearlyIntrestPaid += result.Months[q].IntrestPaid;
                        yearlyPrincipalPaid += result.Months[q].PrincipalPaid;
                        yearlyPropertyTax += result.Months[q].PropertyTax;
                    }
                    result.Years.Add(new SimulationYear()
                    {
                        Year = (int)Math.Floor(i / 12.0),
                        CashInHand = yearlyCashInHand,
                        ValueAtSale = valueAtSale,
                        NetIncome = yearlyNetIncome,
                        TotalNetIncome = result.Years.Count == 0 ? yearlyNetIncome : result.Years[result.Years.Count - 1].TotalNetIncome + yearlyNetIncome,
                        TaxPaid = yearlyTaxPaid,
                        ValueGain = yearlyValueGain,
                        TotalValueGain = result.Years.Count == 0 ? yearlyValueGain : result.Years[result.Years.Count - 1].TotalValueGain + yearlyValueGain,
                        IntrestPaid = yearlyIntrestPaid,
                        PrincipalPaid = yearlyPrincipalPaid,
                        PropertyTax = yearlyPropertyTax
                    }); ;
                }
            }

            result.ExectureTime = DateTime.Now - start;
            return result;
        }

        private double calculateLongTermCapitalGainsTax(double valueGain)
        {
            // Long - term capital gains tax rate    Your income
            // 0 %  $0 to $53,600
            // 15 % $53,601 to $469,050
            // 20 % $469,051 or more

            double bracker1 = valueGain > 53600 ? 53600 : valueGain;
            double bracker2 = valueGain > 469050 ? 469050 - 53000 : (valueGain > 53600 ? valueGain - 53000 : 0);
            double bracker3 = valueGain > 469050 ? valueGain - 469050 - 53600 : 0;

            return bracker1 * 0.0 + bracker2 * 0.15 + bracker3 * 0.2;
        }


        public class SimulationResult
        {
            public TimeSpan ExectureTime { get; set; }
            public double OutOfPocket { get; set; }
            public List<SimulationMonth> Months { get; set; }
            public List<SimulationYear> Years { get; set; }

            public SimulationResult()
            {
                Months = new List<SimulationMonth>();
                Years = new List<SimulationYear>();
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        public class SimulationYear
        {
            public int Year { get; set; }
            public double CashInHand { get; set; }
            public double ValueAtSale { get; set; }
            public double NetIncome { get; set; }
            public double TaxPaid { get; set; }
            public double ValueGain { get; set; }
            public double IntrestPaid { get; set; }
            public double PrincipalPaid { get; set; }
            public double PropertyTax { get; set; }
            public double TotalNetIncome { get; set; }
            public double TotalValueGain { get; set; }

            public override string ToString()
            {
                return "$" + Math.Round(ValueAtSale, 2);
            }
        }

        public class SimulationMonth
        {
            public int Month { get; set; }
            public int Year { get; set; }
            public double OutOfPocket { get; set; }
            public double PrincipalPaid { get; set; }
            public double IntrestPaid { get; set; }
            public double RentIncome { get; set; }
            public double Expenses { get; set; }
            public double NetIncome { get; set; }
            public double CashInHand { get; set; }
            public double ValueGain { get; set; }
            public double TaxableIncome { get; set; }
            public double PropertyTax { get; set; }
            public double Tax { get; set; }
            public double TaxCredit { get; set; }

            public double ValueAtSale { get; set; }
            public double CashAtSale { get; set; }

            public override string ToString()
            {
                return "$" + Math.Round(ValueAtSale, 2);
            }
        }

        public class HouseSimulatorInput
        {
            public double PurchasePrice;
            public double LandValue;
            public double DownPayment;
            public double ClosingCost;
            public double RemodelCost;
            public double RemodelValueIncrease;
            public double LoanRatePtc;
            public double LoanTermYears;
            public double ManagementFeePtc;
            public double MaintenanceCostPtc;
            public double MonthlyRentIncomePerMonth;
            public double TaxRatePtc;
            public double PropertyTaxPtc;
            public double DepreciationOverYears;
            public double VacancyRatePtc;
            public double ExpensesMonthly;
            public double FeesAtSalePtc;
            public double AnnualAppreciationPtc;
            public double AnnualRentIncreasePtc;
        }
    }
}
