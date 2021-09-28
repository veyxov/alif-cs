using System;

namespace MainApp
{
    /* Constants to use in windows */
    static public class Constants
    {
        public const int MIN_POINTS = 11;
        public const string SorryMessage = "You cannot create a credit";
        public const string Congrats = "You can create a credit !";
    }
    /* Mehthods that hold the logic of the program */
    static public class Program
    {
        static public int CalculatePoints(string login, int maritialStatus, bool isFromTj, int loanAmount, int credHistory, int purpose, int delHistory, int limit)
        {
            if (!SQL.ExistAccount(login)) throw new Exception("Account does not exist");

            int result = 0;

            /* Basic information */

            var AccData = SQL.GetAccountData(login);

            // Gender
            // If male add 1 else add 2
            result += AccData.Gender == 0 ? 2 : 1;

            // Age
            if (AccData.Age >= 26 && AccData.Age <= 35) result += 1;
            else if (AccData.Age >= 36 && AccData.Age <= 62) result += 2;
            else if (AccData.Age >= 63) result += 1;

            /* Additional iformation */

            // Maritial status
            if (maritialStatus == 0) result += 1;
            else if (maritialStatus == 1) result += 2;
            else if (maritialStatus == 2) result += 1;
            else if (maritialStatus == 3) result += 2;

            // Citizenship
            if (isFromTj) result += 1;
            // Loan

            if (loanAmount == 0) result += 4;
            else if (loanAmount == 1) result += 3;
            else if (loanAmount == 2) result += 2;
            else if (loanAmount == 3) result += 1;

            // Credit history
            if (credHistory == 0) result += 2;
            else if (credHistory == 1) result += 1;
            else if (credHistory == 2) result += -1;

            // Delays
            if (delHistory == 0) result += -3;
            else if (delHistory == 1) result += -2;
            else if (delHistory == 2) result += -1;
            else if (delHistory == 3) result += 0;

            // Credit purpose
            if (purpose == 0) result += 2;
            else if (purpose == 1) result += 1;
            else if (purpose == 2) result += 0;
            else result += -1;

            // Limit
            if (limit == 0) result += 1;
            else result += 0;

            return result;
        }
    }
}
