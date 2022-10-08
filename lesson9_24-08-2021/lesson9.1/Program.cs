using System;

// NOTE: Using decimal instead of double is better for this kind of operation
// Run:
// :%s/double/decimal/g
// in (Neo)Vim if you want to replace double-s with decimal-s

class Converter {
    // Props
    public double Usd { get; set; }
    public double Eur { get; set; }
    public double Rub { get; set; }

    // From Somoni to other
    public double SomToUsd(double som) {
        return som / Usd;
    }

    public double SomToEur(double som) {
        return som / Eur;
    }

    public double SomToRub(double som) {
        return som / Rub;
    }

    // From other to Somoni
    public double UsdToSom(double val) {
        return Usd * val;
    }
    public double EurToSom(double val) {
        return Eur * val;
    }
    public double RubToSom(double val) {
        return Rub * val;
    }

    // Default constructor
    public Converter() {

    }
    // Custom constructor
    public Converter(double usd, double eur, double rub) {
        Usd = usd;
        Eur = eur;
        Rub = rub;
    }
}

class Program {
    static void Main() {
        // Get information
        Console.Write("How much Somoni is 1 USD ?: "); double usdVal = double.Parse(Console.ReadLine());
        Console.Write("How much Somoni is 1 EUR ?: "); double eurVal = double.Parse(Console.ReadLine());
        Console.Write("How much Somoni is 1 RUB ?: "); double rubVal = double.Parse(Console.ReadLine());

        Converter conv = new Converter(usdVal, eurVal, rubVal);

        // Interactive testing system :)
        while (true) {
            Console.Write("Convert to somoni or from somoni ? {to, from}: "); string type = Console.ReadLine();

            Console.Write($"Input the money: ");   double val = double.Parse(Console.ReadLine());

            double rez = 0; // Answer
            string suffix = "";
            Console.Write("Convert to what ? {usd, eur, rub, som}: "); string what = Console.ReadLine();

            if (type == "to") {
                suffix = what;
                if (what == "usd") rez = conv.SomToUsd(val);
                else if (what == "eur") rez = conv.SomToEur(val);
                else if (what == "rub") rez = conv.SomToRub(val);
                else Console.WriteLine("Incorrect data !");
            } else {
                suffix = "Som";
                if (what == "usd") rez = conv.UsdToSom(val);
                else if (what == "eur") rez = conv.EurToSom(val);
                else if (what == "rub") rez = conv.RubToSom(val);
                else Console.WriteLine("Incorrect data !");
            }
            Console.WriteLine($"Result: {rez}{suffix}");
        }
    }
}
/*
 * Vim config:
 * mapping for fast changing FROMtoWHAT to WHATtoFROM
 * nnoremap rem :s/\([A-Z].*\)To\(.*\)(/\2To\1(<CR>
 * */
