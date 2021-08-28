using System;
using System.Security;
using System.Net.Mail;
class DocWorker {
    public virtual void OpenDoc() {
        Console.WriteLine("The document is open");
    }

    public virtual void EditDoc() {
        Console.WriteLine("Document editing is available on PRO version");
    }

    public virtual void SaveDoc() {
        Console.WriteLine("Document saving  is availble on PRO version");
    }
}
class ProDocWorker : DocWorker {
    public override void EditDoc() {
        Console.WriteLine("Document edited !");
    }
    public override void SaveDoc() {
        Console.WriteLine("Document saved in old format.");
        Console.WriteLine("Saving in other formats is available in Expert version");
    }
}

class ExpertDowWorker : ProDocWorker {
    public override void SaveDoc() {
        Console.WriteLine("Document saved in new format !"); }
}
class Program {
    // Don't pay attention at this.
    public static bool IsValidEmail(string email) {
        if (!MailAddress.TryCreate(email, out var mailAddress))
            return false;
        return true;
    }

    // Don't pay attention at this.
    // This is copy-pasted from:
    // https://stackoverflow.com/questions/3404421/password-masking-console-application
    public static SecureString GetPassword() {
        var pwd = new SecureString();
        while (true) {
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter) break;
            else if (i.Key == ConsoleKey.Backspace) {
                if (pwd.Length > 0) {
                    pwd.RemoveAt(pwd.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (i.KeyChar != '\u0000' ) {
                pwd.AppendChar(i.KeyChar);
                Console.Write("*");
            }
        }
        return pwd;
    }

    static bool GetUserInfo() {
        Console.Write("Input your email adress: ");
        string email = Console.ReadLine();
        if (!IsValidEmail(email)) {
            Console.WriteLine("Your email is not valid !");
            return false;
        }
        Console.Write("Input your password: ");
        SecureString pass = GetPassword();

        if (pass.Length < 6) {
            Console.WriteLine("Password too short");
            return false;
        }
        Console.WriteLine("\nWelcome, to your account you are a VIP member !");
        return true;
    }
    static void Main(string[] args) {
        // This is an example of propretary(BAD) software :(
        //
        // Software should be FREE and LIBRE !
        // Not FREE as in free beer, FREE as in freedom !!!
        //
        // FOSS (Free and Open Source Software) is the future !
        //

        Console.WriteLine("PRO 99$ or EXPERT 199$ or BASIC *free");
        Console.WriteLine("*Comes with some limitations");
        Console.Write("Choose your plan: ");

        // We will decide later which plan it is.
        DocWorker prog = null;

        string plan = Console.ReadLine();

        if (plan == "pro") {
            if (GetUserInfo()) {
                prog = new ProDocWorker();
                Console.WriteLine("Activated Pro plan !");
            }
        } else if (plan == "expert") {
            if (GetUserInfo()) {
                prog = new ExpertDowWorker();
                Console.WriteLine("Activated Expert plan !");
            }
        } else {
            prog = new DocWorker();
            Console.WriteLine("Activated Basic plan !");
        }

        while (true) {
            Console.Write("What to do: 1-open 2-edit 3-save: ");
            string cmd = Console.ReadLine();
            switch (cmd) {
                case "1":
                    prog.OpenDoc();
                    break;
                case "2":
                    prog.EditDoc();
                    break;
                case "3":
                    prog.SaveDoc();
                    break;
                default:
                    Console.WriteLine("Invalid input !");
                    break;
            }
            Console.Clear();
        }
    }
}
