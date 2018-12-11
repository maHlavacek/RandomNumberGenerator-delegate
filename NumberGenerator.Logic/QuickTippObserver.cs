using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver
    {
        #region Fields

        private RandomNumberGenerator _numberGenerator;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(RandomNumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator ?? throw new ArgumentNullException(nameof(numberGenerator));
            QuickTippNumbers = new List<int>(6);

            // Beim NumberGenerator als Beobachter registrieren
            _numberGenerator.NewNumber += OnNextNumber;
        }

        #endregion

        #region Methods

        public void OnNextNumber(object sender,int number)
        {
            CountOfNumbersReceived++;

            if ((1 <= number && number <= 45)
                && !QuickTippNumbers.Contains(number))
            {
                QuickTippNumbers.Add(number);
            }

            if (QuickTippNumbers.Count >= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got a full Quick-Tipp => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();

                DetachFromNumberGenerator();
            }
        }
        /// <summary>
        /// Ergebnis ausgeben
        /// </summary>
        /// <returns></returns>
        public string PrintResult()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($">> {this.GetType().Name + ":",-20} Received {CountOfNumbersReceived} numbers ===> Quick-Tipp is ");
            QuickTippNumbers.Sort();
            for (int i = 0; i < QuickTippNumbers.Count; i++)
            {
                stringBuilder.Append($"{QuickTippNumbers[i]}, ");
            }
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return $"{base.ToString()} => QuickTippObserver [{nameof(QuickTippNumbers)}='{String.Join(", ", QuickTippNumbers.OrderBy(_ => _))}']";
        }
        /// <summary>
        /// Von Event abmelden
        /// </summary>
        private void DetachFromNumberGenerator()
        {
            _numberGenerator.NewNumber -= OnNextNumber;
        }

        #endregion
    }
}
