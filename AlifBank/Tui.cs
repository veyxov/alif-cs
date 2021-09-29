/* This file holds the UI part of the project */
using System;
using Terminal.Gui;
using NStack;
using MainApp;
using System.Data;

namespace AlifBank
{
    class tui
    {
        public static string currentLogin = null;
        public static Action currentLoginPriv = null;
        public static string currentClientLogin = null;

        static void GetCurrentClientScreen()
        {
            InitApp(out var top);
            var loginLabel = new Label("Login of the client: ")
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

            var doneButton = new Button("Done")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(loginText) + 3,
            };
            try
            {
                doneButton.Clicked += () =>
                {
                    var tmpLogin = loginText.Text.ToString();
                    try
                    {
                        if (!SQL.ExistAccount(tmpLogin)) throw new Exception($"Account {tmpLogin} not found");

                        currentClientLogin = tmpLogin;
                        Switch(AdminScreen);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                    }

                };

                top.Add(loginLabel, loginText);
                top.Add(doneButton);
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }
            Application.Run();
        }
        static void AccountDataScreen()
        {
            InitApp(out var top);

            var tableView = new TableView()
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                Switch(UserDataScreen);
            };

            try
            {
                tableView.Table = SQL.GetAccountDataAllTableSpecific(currentClientLogin);
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }

            top.RemoveAll();
            top.Add(tableView);
            top.Add(backButton);

            Application.Run();
        }
        static void UserDataScreen()
        {
            InitApp(out var top);


            var getUserDataButton = new Button("Get account data")
            {
                X = Pos.Center(),
                Y = 1
            };

            var userTransactsDataButton = new Button("Get transactions data")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(getUserDataButton) + 1
            };

            userTransactsDataButton.Clicked += () =>
            {
                Switch(UserTransactsDataScreen);
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            var backToAdminButton = new Button("Back to admin screen")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backToAdminButton.Clicked += () =>
            {
                Switch(AdminScreen);
            };

            backButton.Clicked += () =>
            {
                top.RemoveAll();
                top.Add(getUserDataButton);
                top.Add(backToAdminButton);
            };
            getUserDataButton.Clicked += () =>
            {
                Switch(AccountDataScreen);
            };
            top.Add(getUserDataButton);
            top.Add(backToAdminButton);
            top.Add(userTransactsDataButton);
            Application.Run();
        }

        static void UserScreen()
        {
            InitApp(out var top);

            throw new NotImplementedException();

            Application.Run();
        }
        static void CreateCreditScreen()
        {
            InitApp(out var top);

            var marStatusLabel = new Label("Maritial status")
            {
                X = Pos.Center(),
                Y = 1
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
                var sMaritStatus = marStatusRadio.SelectedItem;
                var sIsFromTj = isFromTJ.Checked;
                var sLoanAmount = loanFromTotalRadio.SelectedItem;
                var sCredHistory = creditHistoryRadio.SelectedItem;
                var sPurpose = purposeRadio.SelectedItem;
                var sDelHistory = delayRadio.SelectedItem;
                var sLimit = limitRadio.SelectedItem;

                try
                {
                    var points = Program.CalculatePoints(currentClientLogin,
                            sMaritStatus,
                            sIsFromTj,
                            sLoanAmount,
                            sCredHistory,
                            sPurpose,
                            sDelHistory,
                            sLimit);

                    var curBalance = SQL.CalculateAccountBalance(currentClientLogin);

                    if (curBalance < 0)
                    {
                        throw new Exception($"Account is already in debit: {curBalance}");
                    }

                    if (points > Constants.MIN_POINTS)
                    {
                        MessageBox.Query("Congrats", Constants.Congrats, "Ok");

                        var inputWin = new Window("Input the amount")
                        {
                            X = Pos.Center(),
                            Y = Pos.Center(),
                            Width = Dim.Fill(),
                            Height = Dim.Fill()
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

                        var limitLabel = new Label("Limit in months: ")
                        {
                            X = Pos.Center(),
                            Y = Pos.Bottom(inputText)
                        };
                        var limitText = new TextField()
                        {
                            X = Pos.Right(limitLabel),
                            Y = Pos.Bottom(inputText),
                            Width = 10
                        };

                        var submitButton = new Button("Submit")
                        {
                            X = Pos.Center(),
                            Y = Pos.Bottom(limitText),
                        };

                        submitButton.Clicked += () =>
                        {
                            try
                            {
                                SQL.CreditToAccount(currentClientLogin, decimal.Parse(inputText.Text.ToString()), int.Parse(limitText.Text.ToString()));
                                inputWin.RequestStop();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                                inputText.SetFocus();
                            }
                        };

                        inputWin.Add(inputLabel, inputText, curr, limitLabel, limitText, submitButton);

                        top.Add(inputWin);
                    }
                    else
                    {
                        MessageBox.ErrorQuery("Sorry ...", Constants.SorryMessage, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Error !", ex.Message + " " + ex, "Ok");
                }
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                Switch(AdminScreen);
            };


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
        static void UserTransactsScreen()
        {
            InitApp(out var top);

            var mainLabel = new Label($"{currentClientLogin}'s transactions: ")
            {

            };

            Application.RequestStop();
        }
        static void AdminScreen()
        {
            InitApp(out var top);

            var welcomeLabel = new Label("Welcome to the admin screen !")
            {
                X = Pos.Center(),
                Y = 1
            };

            var userLabel = new Label($"Now you are working with {currentClientLogin}")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(welcomeLabel)
            };

            var newCreditButton = new Button("Create new credit for user")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(welcomeLabel) + 3
            };

            newCreditButton.Clicked += () =>
            {
                Switch(CreateCreditScreen);
            };

            var userDataButton = new Button("Get user data")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(newCreditButton) + 3
            };

            userDataButton.Clicked += () =>
            {
                Switch(UserDataScreen);
            };

            var userTransactsButton = new Button("Create new credit for user")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(userDataButton) + 1
            };

            userTransactsButton.Clicked += () =>
            {
                Switch(UserTransactsScreen);
            };

            var graphButton = new Button("Show repayment graph")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(userTransactsButton) + 3
            };

            graphButton.Clicked += () =>
            {
                Switch(RepaymentGraphScreen);
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
                Switch(LoginScreen);
            };

            top.Add(welcomeLabel, newCreditButton);
            top.Add(userLabel);
            top.Add(userDataButton);
            top.Add(userTransactsButton);
            top.Add(graphButton);
            top.Add(backButton);
            Application.Run();
        }

        static void RepaymentGraphScreen()
        {
            InitApp(out var top);

            var tableView = new TableView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(tableView);
            var dt = new DataTable ();
			dt.Columns.Add ("Num");
			dt.Columns.Add ("Amount");
			dt.Columns.Add ("Expiration date");

            int limit = SQL.GetAccountTransactionData(currentClientLogin).Limit;
            var tmpBal = SQL.CalculateAccountBalance(currentClientLogin);
            decimal mean = Math.Round((-1 * tmpBal) / limit, 2);

            MessageBox.Query("Test", $"limit: {limit}\nBalance:{tmpBal}\nmean: {mean}", "Ok");

            for (int i = 0; i < limit; ++i) {
                dt.Rows.Add (i.ToString(), mean.ToString(), SQL.GetAccountTransactionData(currentClientLogin).Created_At.AddDays(i));
            }

            tableView.Table = dt;
            Application.Run();
        }
        static void UserTransactsDataScreen()
        {
            InitApp(out var top);

            var tableView = new TableView()
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                Switch(UserDataScreen);
            };

            try
            {
                tableView.Table = SQL.GetAccountTransactions(currentClientLogin);
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }

            top.RemoveAll();
            top.Add(tableView);
            top.Add(backButton);


            Application.Run();
        }

        static void RegisterScreen()
        {
            InitApp(out var top);

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

            var backButton = new Button("Back")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                Switch(MainScreen);
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
            top.Add(backButton);

            Application.Run();
        }
        static void LoginScreen()
        {
            InitApp(out var top);

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
            var backButton = new Button("Back to main")
            {
                X = Pos.Percent(5f),
                Y = Pos.Percent(95f)
            };

            backButton.Clicked += () =>
            {
                Switch(MainScreen);
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
                        Switch(GetCurrentClientScreen);
                    }
                    else
                    {
                        currentLoginPriv = UserScreen;
                        Switch(UserScreen);
                    }
                }
            };
            top.Add(login, password, loginText, passText, doneButton, backButton);

            Application.Run();
        }
        static void InitApp(out Terminal.Gui.Toplevel app) { Application.Init(); app = Application.Top; }
        static void Switch(System.Action to) { running = to; Application.RequestStop(); }

        /* Screen for selecting to login or register */
        static void MainScreen()
        {
            InitApp(out var top);

            var helloLabe = new Label("Hello and welcome !") { X = Pos.Center(), Y = Pos.Percent(20f) };

            var loginButton = new Button("Login") { X = Pos.Center() - 10, Y = Pos.Center() };
            loginButton.Clicked += () => { Switch(LoginScreen); };

            var registerButton = new Button("Register") { X = Pos.Right(loginButton) + 5, Y = Pos.Center() };
            registerButton.Clicked += () => { Switch(RegisterScreen); };

            var exitButton = new Button("Exit") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            exitButton.Clicked += () => { Switch(null); };

            top.Add(
                helloLabe,
                loginButton,
                registerButton,
                exitButton);

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
