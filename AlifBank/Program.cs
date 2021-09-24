using System;
using Terminal.Gui;
namespace AlifBank
{
    class Program
    {
        static void LoginScreen() {
            Application.Init();
            var top = Application.Top;
            
            var login = new Label("Login: ") {
                X = Pos.Center() - 10,
                  Y = Pos.Center()
            };

            var password = new Label("Password: ")
            {
                X = Pos.Left(login),
                  Y = Pos.Top(login) + 1
            };
            var loginText = new TextField("")
            {
                X = Pos.Right(password),
                  Y = Pos.Top(login),
                  Width = 40
            };
            var passText = new TextField("")
            {
                Secret = true,
                       X = Pos.Left(loginText),
                       Y = Pos.Top(password),
                       Width = Dim.Width(loginText)
            };
            var doneButton = new Button("Done") {
                X = Pos.Center(),
                  Y = Pos.Bottom(passText)
            };
            doneButton.Clicked += () => {
                MessageBox.Query("Result", passText.Text);
            };
            top.Add(login, password, loginText, passText, doneButton);

            Application.Run();
        }
        static void MainScreen() {
            Application.Init();
            var top = Application.Top;

            var helloLabe = new Label("Hello and welcome !") {
                X = Pos.Center(),
                  Y = Pos.Percent(50f)
            };

            var loginButton = new Button("Login") {
                X = Pos.Center(),
                  Y = Pos.Bottom(helloLabe),
            };

            loginButton.Clicked += () => {
                running = LoginScreen;
                Application.RequestStop();
            };

            top.Add(loginButton, helloLabe);
            Application.Run();
        }

        public static Action running = MainScreen;
        static void Main()
        {
            /* Initialize the window */
            Console.OutputEncoding = System.Text.Encoding.Default;

            while (running != null) {
                running.Invoke ();
            }
            Application.Shutdown ();
        } 
    }
}
