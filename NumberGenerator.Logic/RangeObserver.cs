using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Anzahl der generierten Zahlen in einem bestimmten Bereich zählt. 
    /// </summary>
    public class RangeObserver : BaseObserver
    {
        #region Properties

        /// <summary>
        /// Enthält die untere Schranke (inkl.)
        /// </summary>
        public int LowerRange { get; private set; }
        
        /// <summary>
        /// Enthält die obere Schranke (inkl.)
        /// </summary>
        public int UpperRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der Zahlen, welche sich im Bereich befinden.
        /// </summary>
        public int NumbersInRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der gesuchten Zahlen im Bereich.
        /// </summary>
        public int NumbersOfHitsToWaitFor { get; private set; }

        #endregion


        #region Constructors

        public RangeObserver(RandomNumberGenerator numberGenerator, int numberOfHitsToWaitFor, int lowerRange, int upperRange) : base(numberGenerator, int.MaxValue)
        {
            if(numberOfHitsToWaitFor < 0)
            {
                throw new ArgumentException("Negative zahlen sind nicht erlaubt");
            }
            if(lowerRange > upperRange)
            {
                throw new ArgumentException("Ungültiger Grenzenbereich");
            }
            
            NumbersOfHitsToWaitFor = numberOfHitsToWaitFor;
            LowerRange = lowerRange;
            UpperRange = upperRange;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{ base.ToString()} => RangeObserver: [LowerRange : '{LowerRange}',UpperRange: '{UpperRange}',NumbersInRange: '{NumbersInRange}',NumbersOfHintToWaitFor: '{NumbersOfHitsToWaitFor}' ]";
        }

        public override void OnNextNumber(int number)
        {
            if (number > LowerRange && number < UpperRange)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  >>{this.GetType().Name}:Number {number} is in range ('200-300')!");
                Console.ResetColor();

                NumbersInRange++;               
            }
            if (NumbersInRange >= NumbersOfHitsToWaitFor)
            {
                DetachFromNumberGenerator();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got '{CountOfNumbersReceived}' in the configured range  => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
            }           
            base.OnNextNumber(number);
        }

        #endregion
    }
}
