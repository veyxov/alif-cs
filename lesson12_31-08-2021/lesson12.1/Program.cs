using System;
interface IPlayable {
    void Play();
    void Pause();
    void Stop();
}

interface IRecordable {
    void Record();
    void RecordPause();
    void RecordStop();
}

// Wraper interface
interface IRecAndPlayAble : IPlayable, IRecordable {  }

class Player : IRecAndPlayAble {
    public void Play() {
        Console.WriteLine("Playing..");
    }
    public void Pause() {
        Console.WriteLine("Pausing..");
    }
    public void Stop() {
        Console.WriteLine("Stoping..");
    }
    public void Record() {
        Console.WriteLine("Recoring ..");
    }
    public void RecordPause() {
        Console.WriteLine("Pausing recording ..");
    }
    public void RecordStop() {
        Console.WriteLine("Stopping recording ..");
    }
}

// This class inherits only form one interface
class Recorder : IRecordable {
    public void Record() {
        Console.WriteLine("Starting record !");
    }
    public void RecordPause() {
        Console.WriteLine("Pausing record !");
    }
    public void RecordStop() {
        Console.WriteLine("Stopping record!");
    }
}
class Program {
    static void Main() {
        Console.ForegroundColor = ConsoleColor.Red;
        Player player = new Player();
        player.Play();
        player.Pause();
        player.Stop();
        player.Record();
        player.RecordPause();
        player.RecordStop();

        Console.WriteLine();
        // Recorder
        Console.ForegroundColor = ConsoleColor.Yellow;
        Recorder rec = new Recorder();
        rec.Record();
        rec.RecordPause();
        rec.RecordStop();

        Console.ResetColor();
    }
}
