using System.Collections.Generic;
using System;

static class IO {
    static private bool DEBUG    = true;
    static private bool COLORFUL = true;

    static public void Print(string what, ConsoleColor color = ConsoleColor.White, bool newLine = true) {
        Console.ForegroundColor = COLORFUL ? color : ConsoleColor.White;
        Console.Write(what);

        if (newLine) Console.Write("\n");

        Console.ResetColor();
    }

    /* This function will not output to the terminal if DEBUG is not set */
    static public void Debug(string what, ConsoleColor color = ConsoleColor.DarkGray, bool newLine = true) {
        if (!DEBUG) return;

        Print(what, color, newLine);
    }

    /* This method prompts WHAT and gets the input from Console*/
    static public T GetInput<T>(string what = "") {
        IO.Print(what, ConsoleColor.Yellow, false);
        var input = (Console.ReadLine());
        /* Change the input type to the T type */
        return (T)Convert.ChangeType(input, typeof(T));
    }
}

public class MyDict<TKey, TVal> {
    private TKey[] keys;
    private TVal[] vals;
    private bool hasKey(TKey key) {
        foreach(var k in keys) {
            /* If found return true */
            if (EqualityComparer<TKey>.Default.Equals(k, key)) return true;
        }
        /* Not found */
        return false;
    }

    private void AddKey(TKey key) {
        /* Add one element */
        Array.Resize(ref keys, keys.Length + 1);
        keys[keys.Length - 1] = key;
    }
    private void AddVal(TVal val) {
        /* Add one element */
        Array.Resize(ref vals, vals.Length + 1);
        vals[vals.Length - 1] = val;
    }

    public void Add(TKey key, TVal val) {
        if (hasKey(key)) throw new Exception("This key already exists !");
        AddKey(key);
        AddVal(val);
        IO.Debug($"Added {key} -> {val}");
    }

    // Get length
    public int Size {
        private set {}
        get {
            if (keys.Length != vals.Length)
                throw new Exception("For some reason the keys and vals length are not the same :(");
            return keys.Length;
        }
    }

    public TVal this[TKey key] {
        private set {
            /* Call the internal function */
            Add(key, value);
        }
        get {
            for (int i = 0; i < keys.Length; ++i) {
                /* Return value corresponding with key */
                if (EqualityComparer<TKey>.Default.Equals(keys[i], key)) {
                    return vals[i];
                }
            }
            throw new Exception("Not found !");
        }
    }

    public void Print() {
        for (int i = 0; i < keys.Length; ++i) {
            IO.Print($"{keys[i]} -> {vals[i]}");
        }
    }

    // Default constructor ...
    public MyDict() {
        keys = new TKey[0];
        vals = new TVal[0];
    }
}

class Program {
    static void Main() {
        var dict = new MyDict<string, string>();
        dict.Add("Hello", "Hallo");
        dict.Add("How are your ?", "Wie geht's ?");
        dict.Add("I am sorry", "Es tut mir leid");
        dict.Add("Teacher", "Lehrer");
        dict.Print();
        //-----------------------------------------------
        var nums = new MyDict<int, string>();
        nums.Add(1, "I");
        nums.Add(2, "II");
        nums.Add(3, "III");
        nums.Add(4, "IV");
        nums.Add(5, "V");
        nums.Add(6, "VI");
        nums.Add(7, "VII");
        nums.Add(8, "VIII");
        nums.Add(9, "IX");
        nums.Add(10, "X");
        nums.Print();
    }
}
