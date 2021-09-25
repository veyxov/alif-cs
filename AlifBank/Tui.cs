/* This file holds the UI part of the project */
using System;
using Terminal.Gui;
namespace AlifBank
{
    class tui
    {
        static void RegisterScreen()
        {
            Application.Init();
            var top = Application.Top;

            /* Registration lable */
            var regLabel = new Label("Register")
            {
                X = Pos.Center(),
                Y = 1
            };

            var loginLab = new Label("Login")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(regLabel) + 3
            };

            var loginText = new TextField("+992")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loginLab),
                /* Phone len in TJ is 9 and +992 prefix and margin */
                Width = 9 + 4 + 1
            };

            var nameLab = new Label("Name") {

            };

            top.Add(regLabel, loginLab, loginText);
            Application.Run();
        }
        static void LoginScreen()
        {
            Application.Init();
            var top = Application.Top;

            var login = new Label("Login: ")
            {
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
            var doneButton = new Button("Done")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(passText) + 3,
            };
            doneButton.Clicked += () =>
            {
                string loginRez = loginText.Text.ToString();
                string passRez = passText.Text.ToString();
                if (!SQL.ExistAccount(loginRez)) MessageBox.ErrorQuery("Error!", $"Account {loginRez} not found.", "Ok");
                else if (!SQL.Auth(loginRez, passRez)) MessageBox.ErrorQuery("Error!", $"Wrong password for {loginRez}.", "Ok");

                else
                {
                    /* Everything is ok ! */
                    // TODO: Go to home screen
                }
            };
            top.Add(login, password, loginText, passText, doneButton);

            Application.Run();
        }
        static void MainScreen()
        {
            Application.Init();
            var top = Application.Top;

            var helloLabe = new Label("Hello and welcome !")
            {
                X = Pos.Center(),
                Y = Pos.Percent(50f)
            };

            var loginButton = new Button("Login")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(helloLabe),
            };

            loginButton.Clicked += () =>
            {
                running = LoginScreen;
                Application.RequestStop();
            };

            var registerButton = new Button("Register")
            {
                X = Pos.Right(loginButton),
                Y = Pos.Bottom(helloLabe)
            };

            registerButton.Clicked += () =>
            {
                running = RegisterScreen;
                Application.RequestStop();
            };

            top.Add(loginButton, registerButton, helloLabe);
            Application.Run();
        }

        public static Action running = MainScreen;
        static void Main()
        {
            /* Initialize the window */
            Console.OutputEncoding = System.Text.Encoding.Default;

            while (running != null)
            {
                running.Invoke();
            }
            Application.Shutdown();
        }
    }
}
