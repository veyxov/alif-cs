using System;
using System.Threading;

class Transport {
    public double Speed     { get; set; }
    public string FuelType  { get; set; }
    public string Name      { get; set; }
    public int    FuelLevel { get; set; }
    public int    Seats     { get; set; }
    public bool   IsTurnedOn{ get; set; } = false;


    public void StartEngine() {
        Console.WriteLine("Starting engine ...");
        IsTurnedOn = true;
    }

    public void StopEngine() {
        Console.WriteLine("Turning the engine off ...");
        IsTurnedOn = false;
    }

    // Run for given kilometrs
    public void Go(int kms) {
        if (IsTurnedOn == false) {
            Console.WriteLine("You need to Turn of the engine first !");
            return;
        }

        if (FuelLevel - kms < 0) {
            Console.WriteLine($"Not enought fuel to go {kms} kilometrs");
            return;
        }
        Console.WriteLine($"Going {kms} killometers..");
        Thread.Sleep(1000 + kms);
        Console.WriteLine("Done.");
        FuelLevel -= kms;
    }

    public void GetTransportInfo() {
        Console.WriteLine("Transport info: ");
        Console.WriteLine($"Speed: {Speed}");
        Console.WriteLine($"Fuel Type: {FuelType}");
        Console.WriteLine($"Speed: {Speed}");
    }

    // The first rule ! :)
    public Transport() {  }

    public Transport(string name, double speed, string fuelType, int seats, int fuelLevel) {
        Name  = name;
        Speed = speed;
        FuelType = fuelType;
        Seats = seats;
        FuelLevel = fuelLevel;
    }
}

class Auto : Transport {
    // How many doors this car has ?
    public int DoorsCount { get; set; }
    // Who tints cargo car's windows ?
    public bool HasWindowTinting { get; set; }

    public void GetAutoInfo() {
        Console.WriteLine("Auto info");
        Console.WriteLine($"Doors: {DoorsCount}\nDoes have tinting: {HasWindowTinting}");
    }

    // Default constructor
    public Auto() {  }

                                                                                                                     // Inheriting constructro for Transport
    public Auto(int doorsCout, bool hastinting, string name, double speed, string fuelT, int seats, int fuelLevel) : base(name, speed, fuelT, seats, fuelLevel) {
        DoorsCount = doorsCout;
        HasWindowTinting = hastinting;
    }
}

class PassengerAuto : Auto {
    public int passengerCapacity;
    public bool hasConditioner = true;

    public PassengerAuto() {  }

    public PassengerAuto(int passCount, bool hasCond) {
        passengerCapacity = passCount;
        hasConditioner = hasCond;
    }
}

class CargoAuto : Auto {
    public double Capacity { get; set; }
}

class Plane : Transport {
    public int MaxAltitude { get; set; } // How much this plane can go high
    public string engineType;
    public string EingineType {
        get {
            return engineType;
        }
        set {
            if (value != "jet" && value != "propeller" && value != "rocket") {
                Console.WriteLine("Plane engine types : {jet propeller rocket}");
            } else {
                engineType = value;
            }
        }
    }

    public int WingSize;
}

class PassengerPlane : Plane {
    public int passengerCapacity;
    public bool hasParachute = false; // XD
}

class CargoPlane : Plane {
    // Max Cargo capacity eg. 150 Tonns
    public int MaxLoad { get; set; }
    public int CurrLoad{ get; set; }

    public void Load(int tonns) {
        if (CurrLoad + tonns > MaxLoad) {
            Console.WriteLine($"Cargo Plane full !\n The load exeeds max with {(CurrLoad + tonns) - MaxLoad} tonns");
            return;
        }
        CurrLoad += tonns;
    }
}

class Train : Transport {
    // IsFast train ?
    public bool IsExpress { get; set; }
    // Cars are sections of a train
    public int  cars      { get; set; }
}

class Program {
    static void Main() {
        Console.WriteLine("Transport: ");
        Transport transport = new Transport("Generic transport", 50, "Gas", 5, 100);
        transport.GetTransportInfo();
        transport.Go(50);
        transport.StartEngine();
        transport.Go(30);
        transport.Go(100);
        transport.StopEngine();
        Console.WriteLine("-----------------------------");

        Console.WriteLine("Auto: ");
        Auto auto = new Auto(4, true, "BMW", 120, "Diesel", 6, 150);
        auto.GetTransportInfo();
        auto.GetAutoInfo();
        Console.WriteLine("-----------------------------");
    }
}
