using System;

class Transport {
    public double Speed     { get; set; }
    public string FuelType  { get; set; }
    public string Name      { get; set; }
    public int    FuelLevel { get; set; }
    public int    Seats     { get; set; }
    public bool   IsTurnedOn{ get; set; } = false;


    public void StartEngine() {
        Console.WriteLine("Starting engine ...");
        isTurnedOn = true;
    }

    public void StopEngine() {
        Console.WriteLine("Turning the engine off ...");
        isTurnedOn = false;
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
        FuelLevel -= kms;
    }

    // The first rule ! :)
    public Transport() {  }

    public Transport(string name, double speed) {
        Name  = name;
        Speed = speed;
    }
}

class Auto : Transport {
    // Default constructor
    public Auto() {  }

    // How many doors this car has ?
    public int DoorsCount { get; set; }
}

class PassengerAuto : Auto {
    public int passengerCapacity;
    public bool HasConditioner = true;
}

class CargoAuto : Auto {
    public double Capacity { get; set; }
}

class Plane : Transport {
    public int MaxAltitude { get; set; } // How much this plane can go high
    public string engineType;
    public string EingineType {
        get;
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

    }
}
