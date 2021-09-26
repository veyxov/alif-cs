namespace MainApp
{
    static public class Program
    {
        static public int CalculatePoints(string login)
        {
            int result = 0;
            var AccData = SQL.GetAccountData(login);

            // Gender
            // If male add 1 else add 2
            result += AccData.Gender == 0 ? 2 : 1;

            // Age
            if (AccData.Age >= 26 && AccData.Age <= 35)         result += 1;
            else if (AccData.Age >= 36 && AccData.Age <= 62)    result += 2;
            else if (AccData.Age >= 63)                         result += 1;
            


            return result;
        }
    }
}
