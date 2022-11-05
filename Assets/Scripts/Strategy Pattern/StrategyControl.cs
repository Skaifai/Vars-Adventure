using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Strategy_Pattern
{
    internal class StrategyControl
    {
        ICommandStrategy _commandStrategy;

        public StrategyControl(ICommandStrategy commandStrategy)
        {
            _commandStrategy = commandStrategy;
            Console.WriteLine("Strategy is set!");
        }

        // Setter method. Allows us to change strategies at runtime
        public void SetStrategy(ICommandStrategy strategy)
        {
            this._commandStrategy = strategy;
            //Console.WriteLine(strategy.SortingType + " sorting is set.\n");
        }
    }
}
