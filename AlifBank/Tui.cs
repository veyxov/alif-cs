/* This file holds the UI part of the project */
using System;
using Terminal.Gui;
using NStack;
using MainApp;
namespace AlifBank
{
    class tui
    {
        public static string currentLogin = null;
        public static Action currentLoginPriv = null;
        static void UserScreen()
        {
            Application.Init();
            var top = Application.Top;



            Application.Run();
        }
        static void CreateCreditScreen()
        {
            Application.Init();
            var top = Application.Top;

            var loginLabel = new Label("Login: ")
            {
                X = Pos.Center(),
                Y = 1
            };
            
            var loginText = new TextField() 
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loginLabel),
                Width = Dim.Percent(30f)
            };
            var marStatusLabel = new Label("Maritial status")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loginText)
            };

            var marStatusRadio = new RadioGroup(new ustring[]
            {
              "Single",
              "Married",
              "Divorced",
              "Widow(er)"
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(marStatusLabel)
            };

            var isFromTJ = new CheckBox("I am Tajikistan citizen. (checkbox)")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(marStatusRadio)
            };

            var loanFromTotalLable = new Label("Loan amount from total income: ")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(isFromTJ)
            };

            var loanFromTotalRadio = new RadioGroup(new ustring[]
            {
              "less than 80%",
              "80%  - 150%",
              "150% - 250%",
              "more than 250%"
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loanFromTotalLable)
            };

            var creditHistoryLabel = new Label("Credit history: ")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loanFromTotalRadio)
            };

            var creditHistoryRadio = new RadioGroup(new ustring[]
            {
              "More than 3 closed credits",
              "1 - 2 closed credits",
              "No credit history",
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(creditHistoryLabel)
            };

            var delayLable = new Label("Delay history: ")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(creditHistoryRadio)
            };

            var delayRadio = new RadioGroup(new ustring[]
            {
              "more than 7",
              "5 - 7",
              "4",
              "less than 3",
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(delayLable)
            };

            var purposeLabel = new Label("Purpose: ")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(delayRadio)
            };

            var purposeRadio = new RadioGroup(new ustring[]
            {
              "Appliances",
              "Repair",
              "Phone",
              "Other",
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(purposeLabel)
            };

            var limitLabel = new Label("Delay history: ")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(purposeRadio)
            };

            var limitRadio = new RadioGroup(new ustring[]
            {
              "less than 12 months",
              "more than 12 months",
            })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(limitLabel)
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(limitRadio),
            };

            submitButton.Clicked += () =>
            {
                // Newly entered data
                var sLogin = loginText.Text.ToString();
                var sMaritStatus = marStatusRadio.SelectedItem;
                var sIsFromTj = isFromTJ.Checked;
                var sLoanAmount = loanFromTotalRadio.SelectedItem;
                var sCredHistory = creditHistoryRadio.SelectedItem;
                var sPurpose = purposeRadio.SelectedItem;
                var sDelHistory = delayRadio.SelectedItem;
                var sLimit = limitRadio.SelectedItem;

                var points = Program.CalculatePoints(sLogin,
                                                     sMaritStatus,
                                                     sIsFromTj,
                                                     sLoanAmount,
                                                     sCredHistory,
                                                     sPurpose,
                                                     sDelHistory,
                                                     sLimit);
                // TODO: remove this :)
                MessageBox.Query("Results", points.ToString(), "Ok");
                if (points > Constants.MIN_POINTS)
                {
                    try
                    {
                        MessageBox.Query("Congrats", Constants.Congrats, "Ok");

                        var inputWin = new Window("Input the amount")
                        {
                            X = Pos.Center(),
                            Y = Pos.Center()
                        };

                        var inputLabel = new Label("Amount: ")
                        {
                            X = Pos.Center(),
                            Y = Pos.Center()
                        };
                        var inputText = new TextField()
                        {
                            X = Pos.Right(inputLabel),
                            Y = Pos.Center(),
                            Width = 10
                        };

                        var curr = new Label("Somoni")
                        {
                            X = Pos.Right(inputText),
                            Y = Pos.Center()
                        };

                        var submitButton = new Button("Submit")
                        {
                            X = Pos.Center(),
                            Y = Pos.Bottom(curr),
                        };

                        submitButton.Clicked += () =>
                        {
                            try {
                                SQL.MakeTransaction(sLogin, decimal.Parse(inputText.Text.ToString()));
                            } catch (Exception ex) {
                                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                            }
                            inputWin.RequestStop();
                        };

                        inputWin.Add(inputLabel, inputText, curr, submitButton);
                        //inputWin.Add();

                        top.Add(inputWin);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.ErrorQuery("Sorry ...", Constants.SorryMessage, "Ok");
                }
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                running = AdminScreen;
                Application.RequestStop();
            };


            top.Add(loginLabel, loginText);
            top.Add(marStatusLabel, marStatusRadio);
            top.Add(isFromTJ);
            top.Add(loanFromTotalLable, loanFromTotalRadio);
            top.Add(creditHistoryLabel, creditHistoryRadio);
            top.Add(delayLable, delayRadio);
            top.Add(purposeLabel, purposeRadio);
            top.Add(limitLabel, limitRadio);
            top.Add(submitButton);
            top.Add(backButton);
            Application.Run();
        }
        static void AdminScreen()
        {
            Application.Init();
            var top = Application.Top;

            var welcomeLabel = new Label("Welcome to the admin screen !")
            {
                X = Pos.Center(),
                Y = 1
            };

            var newCreditButton = new Button("Create new credit for user")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(welcomeLabel) + 3
            };

            newCreditButton.Clicked += () =>
            {
                running = CreateCreditScreen;
                Application.RequestStop();
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                // TODO Create proper logout
                currentLogin = null;
                running = LoginScreen;
                Application.RequestStop();
            };

            top.Add(welcomeLabel, newCreditButton);
            top.Add(backButton);
            Application.Run();
        }
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

            var fnameLabel = new Label("First name: ")
            {
                X = Pos.Center() - 20,
                Y = Pos.Bottom(regLabel)
            };

            var fnameText = new TextField()
            {
                X = Pos.Right(fnameLabel),
                Y = Pos.Bottom(regLabel),
                Width = 15
            };

            var lnameLabel = new Label("Last name: ")
            {
                X = Pos.Left(fnameLabel),
                Y = Pos.Bottom(fnameText)
            };

            var lnameText = new TextField()
            {
                X = Pos.Right(lnameLabel) + 1,
                Y = Pos.Bottom(fnameText),
                Width = 15
            };

            var ageLabel = new Label("Age: ")
            {
                X = Pos.Left(fnameLabel),
                Y = Pos.Bottom(lnameText)
            };

            var ageText = new TextField()
            {
                X = Pos.Left(lnameText),
                Y = Pos.Bottom(lnameText),
                Width = 3
            };

            var loginLab = new Label("Login:")
            {
                X = Pos.Left(ageLabel),
                Y = Pos.Bottom(regLabel) + 3
            };

            var loginText = new TextField("+992")
            {
                X = Pos.Left(ageText),
                Y = Pos.Bottom(ageText),
                /* Phone len in TJ is 9 and +992 prefix and margin */
                Width = 9 + 4 + 1
            };

            var passLab = new Label("Password:")
            {
                X = Pos.Left(loginLab),
                Y = Pos.Bottom(loginLab)
            };

            var passText = new TextField()
            {
                X = Pos.Left(loginText),
                Y = Pos.Bottom(loginText),
                Width = 12
            };

            var genderLable = new Label("Gender")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loginText) + 1
            };

            var genderRadio = new RadioGroup(new ustring[] { "Male", "Female" })
            {
                X = Pos.Center(),
                Y = Pos.Bottom(genderLable)
            };

            var isAdminCheckBox = new CheckBox("I am an admin")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(genderRadio)
            };

            var submitButton = new Button("Submit")
            {
                X = Pos.Center(),
                Y = Pos.AnchorEnd(1)
            };

            submitButton.Clicked += () =>
            {
                var newAccount = new Account()
                {
                    FistName = fnameText.Text.ToString(),
                    LastName = lnameText.Text.ToString(),
                    Age = int.Parse(ageText.Text.ToString()),
                    Login = loginText.Text.ToString(),
                    Password = passText.Text.ToString(),
                    Gender = genderRadio.SelectedItem,
                    IsAdmin = isAdminCheckBox.Checked
                };
                bool result = false;
                try
                {
                    result = SQL.CreateAccount(newAccount);
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                }
                if (result) MessageBox.Query("Ok !", "Created Account", "Ok");
            };

            top.Add(regLabel);
            top.Add(fnameLabel, fnameText);
            top.Add(lnameLabel, lnameText);
            top.Add(ageLabel, ageText);
            top.Add(loginLab, loginText);
            top.Add(passLab, passText);
            top.Add(genderLable, genderRadio);
            top.Add(isAdminCheckBox);
            top.Add(submitButton);
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
            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                running = MainScreen;
                Application.RequestStop();
            };

            doneButton.Clicked += () =>
            {
                string loginRez = loginText.Text.ToString();
                string passRez = passText.Text.ToString();
                if (!SQL.ExistAccount(loginRez))
                {
                    MessageBox.ErrorQuery("Error!", $"Account {loginRez} not found.", "Ok");
                    loginText.SetFocus();
                }
                else if (!SQL.Auth(loginRez, passRez))
                {
                    MessageBox.ErrorQuery("Error!", $"Wrong password for {loginRez}.", "Ok");
                    passText.SetFocus();
                }

                else
                {
                    currentLogin = loginRez;
                    // Check that the logined user is an administrator
                    if (SQL.IsAdmin(currentLogin))
                    {
                        currentLoginPriv = AdminScreen;
                    }
                    else
                    {
                        currentLoginPriv = UserScreen;
                    }
                    running = currentLoginPriv;
                    Application.RequestStop();
                }
            };
            top.Add(login, password, loginText, passText, doneButton, backButton);

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

            var exitButton = new Button("Exit")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            exitButton.Clicked += () =>
            {
                // TODO: Fix application closure
                Application.Shutdown();
            };
            top.Add(loginButton, registerButton, helloLabe);
            top.Add(exitButton);
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
