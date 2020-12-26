using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace HouseCalculator.ViewModels
{
    public class MonthViewModel : BindableBase
    {
        public static MonthViewModel From(HouseSimulator.SimulationMonth simulationMonth, HouseSimulator.SimulationResult result)
        {
            return new MonthViewModel();
        }
    }
}
