﻿using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg
        {
            get
            {
                return Sum / Math.Max(CountOfNumbersReceived,1);
            }          
        }

    #endregion

    #region Constructors

    public StatisticsObserver(RandomNumberGenerator numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {
            Max = int.MinValue;
            Min = int.MaxValue;            
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{base.ToString()} => StatisticsObserver [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']";
        }
        /// <summary>
        /// Ergebnis ausgeben
        /// </summary>
        /// <returns></returns>
        public override string PrintResult()
        {
            return $"{base.PrintResult()} ===> Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}'.";
        }

        public override void OnNextNumber(object sender,int number)
        {
            Min = Math.Min(Min, number);
            Max = Math.Max(Max, number);
            Sum += number;
            base.OnNextNumber(this.GetType(),number);          
        }

        #endregion
    }
}
