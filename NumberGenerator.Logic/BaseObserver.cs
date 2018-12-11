﻿using System;
using System.ComponentModel;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Zahlen auf der Konsole ausgibt.
    /// Diese Klasse dient als Basisklasse für komplexere Beobachter.
    /// </summary>
    public class BaseObserver
    {
        #region Fields

        private readonly RandomNumberGenerator _numberGenerator;

        #endregion

        #region Properties

        public int CountOfNumbersReceived { get; private set; }
        public int CountOfNumbersToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public BaseObserver(RandomNumberGenerator numberGenerator, int countOfNumbersToWaitFor)
        {
            if (countOfNumbersToWaitFor < 0)
            {
                throw new ArgumentException("negative Zahlen sind nicht erlaubt");
            }          
            _numberGenerator = numberGenerator ?? throw new ArgumentNullException("Parameter numberGenerator darf nicht null sein");
            CountOfNumbersToWaitFor = countOfNumbersToWaitFor;
            _numberGenerator.NewNumber += OnNextNumber;
        }

        #endregion

        #region Methods

        #region IObserver Members

        /// <summary>
        /// Wird aufgerufen wenn der NumberGenerator eine neue Zahl generiert hat.
        /// </summary>
        /// <param name="number"></param>
        public virtual void OnNextNumber(object sender,int number)
        {
            CountOfNumbersReceived++;

            // Sobald die Anzahl der max. Beobachtungen (_countOfNumbersToWaitFor) erreicht ist -> Detach()
            if (CountOfNumbersReceived >= CountOfNumbersToWaitFor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Received '{CountOfNumbersReceived}' of '{CountOfNumbersToWaitFor}' => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                DetachFromNumberGenerator();
            }

        }

        #endregion
        /// <summary>
        /// Ergebnis ausgeben
        /// </summary>
        /// <returns></returns>
        public virtual string PrintResult()
        {
            return $">> {this.GetType().Name+":",-20} Received {CountOfNumbersReceived} numbers";
        }

        public override string ToString()
        {
            return $"BaseObserver [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}']";
        }
        /// <summary>
        /// Von Event abmelden
        /// </summary>
        protected void DetachFromNumberGenerator()
        {
            _numberGenerator.NewNumber -= OnNextNumber;
        }

        #endregion
    }
}
