using System;
using System.Collections.Generic;
using static System.Console;
namespace LiveOffDividend
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();

        }



        static void Menu()
        {
            bool shouldNotExit = true;

            while (shouldNotExit)
            {



                WriteLine("1.Beräkna när du kan leva på dina fonder");

                WriteLine("2. Exit");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {

                    case ConsoleKey.D1:

                        CalculateDividendBreakEven();

                        break;

                    case ConsoleKey.D2:

                        CalculateDividendBasedOnMonthlySavings();

                        break;

                    case ConsoleKey.D3:

                        break;

                    case ConsoleKey.D4:

                        shouldNotExit = false;

                        break;

                }

            }
        }

        private static void CalculateDividendBasedOnMonthlySavings()
        {
            List<Savings> savings = new List<Savings>();

            WriteLine("Hur många år vill du spara? ");
            int yearsOfSaving = Convert.ToInt32(ReadLine());
            WriteLine("Hur mycket kan du spara per månad?: ");
            decimal monthlySavings = Convert.ToDecimal(ReadLine());

            decimal totalThisYear = 0;

            decimal totalinterest = 0;

            decimal totalAmount = 0;

            decimal amountSavedPerYear = monthlySavings * 12;


            for (int i = 0; i < yearsOfSaving; i++)
            {

                totalAmount += amountSavedPerYear;

                decimal interestEarnedPerYear = (totalAmount * (Convert.ToDecimal((Math.Pow((1.0 + 0.08), 1))))) - totalAmount;


                totalAmount += interestEarnedPerYear;

                savings.Add(new Savings(i, amountSavedPerYear, interestEarnedPerYear, totalAmount));

            }







            Clear();


            decimal totalSaved = 0;
            decimal totalInterestEarned = 0;
            int fromTop = 5;
            WriteLine("".PadRight(150, '-'));

            foreach (var year in savings)
            {
                totalSaved += year.AmountSaved;
                totalInterestEarned += year.InterestEarned;

                SetCursorPosition(0, fromTop);
                WriteLine(@$"År nr: {year.YearNr} " +
                    $" Årets sparande: { decimal.Truncate(year.AmountSaved).ToString("c")} |" +
                    $"Totalt sparande: {decimal.Truncate(totalSaved).ToString("c")} |" +
                    $"Årets avkastning: {decimal.Truncate(year.InterestEarned).ToString("c")} |" +
                    $"Total intjänad avkastning: {decimal.Truncate(totalInterestEarned).ToString("c")} |" +
                    $"Totalt kapital: {decimal.Truncate(year.TotalAmount).ToString("c")}|");

                fromTop += 1;
                SetCursorPosition(0, fromTop);
                WriteLine("".PadRight(150, '-'));
                fromTop += 2;


            }

            ReadKey();
        }


        private static void CalculateDividendBreakEven()
        {

            WriteLine("Vad har du för månadskostnader? ");
            decimal monthlySpending = Convert.ToDecimal(ReadLine());
            WriteLine("När vill du leva på avkastningen (år)? ");
            DateTime yearofFreedome = Convert.ToDateTime($"{ReadLine()}-01-01");


            decimal yearlySpending = monthlySpending * 12;

            TimeSpan timesSpanToFreedom = yearofFreedome - DateTime.Now;

            var totalDays = Math.Floor(timesSpanToFreedom.TotalDays);
            var nrOfYearsToFreedom = Math.Round(totalDays / 365)-1;

            decimal amountNeeded = yearlySpending / 0.04M;

            decimal monthlySavings = 50;

            decimal monthlySavingsNeeded = 0;

            decimal totalCapital = 0;

            bool breakLookp = true;


            decimal interestOffSavings = 0;
            decimal totalSavings = 0;

            List<Savings> savings = new List<Savings>();



            do
            {

                decimal totalIntrest = 0;
                decimal totalAmountSaved = 0;

                monthlySavings += 50;


                decimal totalAmount = 0;
                decimal total = 0;

                // decimal amountSaved = (monthlySavings * 12) * Convert.ToDecimal(nrOfYearsToFreedom);

                // decimal interestEarned = (amountSaved * (Convert.ToDecimal((Math.Pow((1.0 + 0.08), nrOfYearsToFreedom))))) - amountSaved;

                for (int i = 0; i < nrOfYearsToFreedom; i++)
                {

                    total += (monthlySavings * 12);

                    decimal interestEarnedPerYear = (total * (Convert.ToDecimal((Math.Pow((1.0 + 0.08), 1))))) - total;


                    total += interestEarnedPerYear;

                    totalIntrest += interestEarnedPerYear;
                    totalAmountSaved += (monthlySavings * 12);

                }







                if (amountNeeded <= total)
                {

                    decimal amountSavedPerYear = monthlySavings * 12;

                    for (int i = 0; i < nrOfYearsToFreedom; i++)
                    {

                        totalAmount += amountSavedPerYear;

                        decimal interestEarnedPerYear = (totalAmount * (Convert.ToDecimal((Math.Pow((1.0 + 0.08), 1))))) - totalAmount;


                        totalAmount += interestEarnedPerYear;

                        savings.Add(new Savings(i, amountSavedPerYear, interestEarnedPerYear, totalAmount));

                    }


                    breakLookp = false;
                    monthlySavingsNeeded = monthlySavings;
                    totalCapital = total;
                    interestOffSavings = totalIntrest;
                    totalSavings = totalAmountSaved;

                }


            } while (breakLookp);






            Clear();
            WriteLine(@$"Förutsatt 8% ränta per år och att  pengarna du lever på per år är 4% av det totala kapitalet " +
                        $"\nbehöver du spara {monthlySavingsNeeded.ToString("c")} per månad under dessa {nrOfYearsToFreedom} år för att nå ditt mål." +
                        $"\nDitt Totala Kapital Kommer vara {totalCapital.ToString("c")} varav {interestOffSavings.ToString("c")} är ränta " +
                        $"\noch {totalSavings.ToString("c")} är sparade pengar.");

            decimal totalSaved = 0;
            decimal totalInterestEarned = 0;
            int fromTop = 5;
            WriteLine("".PadRight(150, '-'));

            foreach (var year in savings)
            {
                totalSaved += year.AmountSaved;
                totalInterestEarned += year.InterestEarned;

                SetCursorPosition(0, fromTop);
                WriteLine(@$"År nr: {year.YearNr} |" +
                    $" Årets sparande: { decimal.Truncate(year.AmountSaved).ToString("c")} |" +
                    $"Totalt sparande: {decimal.Truncate(totalSaved).ToString("c")} |" +
                    $"Årets avkastning: {decimal.Truncate(year.InterestEarned).ToString("c")} |" +
                    $"Total intjänad avkastning: {decimal.Truncate(totalInterestEarned).ToString("c")} |" +
                    $"Totalt kapital: {decimal.Truncate(year.TotalAmount).ToString("c")} |");

                fromTop += 1;
                SetCursorPosition(0, fromTop);
                WriteLine("".PadRight(150, '-'));
                fromTop += 2;


            }

            ReadKey();
        }



    }

    class Savings
    {
        public int YearNr { get; }
        public decimal AmountSaved { get; }

        public decimal InterestEarned { get; }

        public decimal TotalAmount { get; }


        public Savings(int yearNr, decimal amountSaved, decimal interestEarned, decimal totalAmount)
        {
            YearNr = yearNr;
            AmountSaved = amountSaved;
            InterestEarned = interestEarned;
            TotalAmount = totalAmount;
        }
    }


}
