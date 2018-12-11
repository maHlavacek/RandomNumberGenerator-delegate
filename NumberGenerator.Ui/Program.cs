
using NumberGenerator.Logic;
using System;

namespace NumberGenerator.Ui
{
    class Program
    {
        static void Main()
        {
            // Zufallszahlengenerator erstelltn
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(250);

            // Beobachter erstellen
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 10);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 20);
            RangeObserver rangeObserver = new RangeObserver(numberGenerator, 5, 200, 300);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);


            // Nummerngenerierung starten
            numberGenerator.StartNumberGeneration();

            // Resultat ausgeben

            Console.WriteLine("\n\n-------------------------------------------  RESULT  ---------------------------------------------\n");

            Console.WriteLine(statisticsObserver.PrintResult());
            Console.WriteLine(rangeObserver.PrintResult());
            Console.WriteLine(quickTippObserver.PrintResult());

            Console.WriteLine("\n--------------------------------------------------------------------------------------------------");
            Console.WriteLine("Drücken Sie eine beliebige Taste...");
            Console.ReadKey();


            //Console.WriteLine($" >> {statisticsObserver.GetType().Name+":",-20} Received {statisticsObserver.CountOfNumbersReceived:D5} numbers ===> Min='{statisticsObserver.Min}', Max='{statisticsObserver.Max}', Sum='{statisticsObserver.Sum}', Avg='{statisticsObserver.Avg}'.");

            //Console.WriteLine($" >> {rangeObserver.GetType().Name+":",-20} Received {rangeObserver.CountOfNumbersReceived:D5} numbers ===> There were '{rangeObserver.NumbersInRange}' numbers between '{rangeObserver.LowerRange}' and '{rangeObserver.UpperRange}'");

            //Console.WriteLine($" >> {quickTippObserver.GetType().Name+":",-20} Received {quickTippObserver.CountOfNumbersReceived:D5} numbers ===> Quick-Tip is {quickTippObserver.QuickTippNumbers[0]}, {quickTippObserver.QuickTippNumbers[1]}, {quickTippObserver.QuickTippNumbers[2]}, {quickTippObserver.QuickTippNumbers[3]}, {quickTippObserver.QuickTippNumbers[4]}, {quickTippObserver.QuickTippNumbers[5]}.");

 

            //Console.ReadKey();
            // Resultat ausgeben
        }
    }
}
