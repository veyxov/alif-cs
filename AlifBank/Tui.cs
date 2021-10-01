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
        
        public static string currentLogin = null;       // Current logined user
        public static Action currentLoginPriv = null;   // Is current logined ADMIN or USER
        public static string currentClientLogin = null; // Admins work with another USER

        /* This is a helper screen for AdminScreen for choosing an account to work with */
        static void GetCurrentClientScreen()
        {
            Start(out var top);

            var loginLabel = new Label("Login of the client: ") { X = Pos.Center(), Y = Pos.Center() };
            var loginText = new TextField() { X = Pos.Center(), Y = Pos.Bottom(loginLabel), Width = Dim.Percent(30f) };

            var doneButton = new Button("Done") { X = Pos.Center(), Y = Pos.Bottom(loginText), };

            try {
                doneButton.Clicked += () => {
                    var tmpLogin = loginText.Text.ToString();
                    try {
                        if (!SQL.ExistAccount(tmpLogin)) throw new Exception($"Account {tmpLogin} not found");
                        if (SQL.IsAdmin(tmpLogin))       throw new Exception("Admins can't control eachother XD");
                        currentClientLogin = tmpLogin;
                        Switch(AdminScreen);
                    } catch (Exception ex) {
                        MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                    }
                };
                top.Add(loginLabel, loginText, doneButton);
            }
            catch (Exception ex) {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }
            top.Add(BackButton(LoginScreen, "to login screen"));
            End();
        }

        /* This screen shows account data such as: Name, age ... */
        static void AccountDataScreen()
        {
            Start(out var top);

            // Table that holds the data
            var tableView = new TableView() {
                X = 1, Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            try {
                tableView.Table = SQL.GetAccountDataAllTableSpecific(currentClientLogin);
            } catch (Exception ex) {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }

            top.RemoveAll();
            top.Add(tableView, BackButton(UserDataScreen, "to user data"));

            End();
        }
        /* Choose which user data to show */
        static void UserDataScreen()
        {
            Start(out var top);

            var getUserDataButton = new Button("Get account data") { X = Pos.Center(), Y = Pos.Percent(40f) };

            var userTransactsDataButton = new Button("Get transactions data") { X = Pos.Center(), Y = Pos.Bottom(getUserDataButton) + 1 };
            userTransactsDataButton.Clicked += () => { Switch(UserTransactsDataScreen); };

            var backButton = new Button("Back") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            var backToAdminButton = new Button("Back to admin screen") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            backToAdminButton.Clicked += () => { Switch(currentLoginPriv); };
            backButton.Clicked += () => {
                top.RemoveAll();
                top.Add(getUserDataButton, backToAdminButton);
            };
            getUserDataButton.Clicked += () => { Switch(AccountDataScreen); };

            top.Add( getUserDataButton, userTransactsDataButton, backToAdminButton);
            End();
        }

        /* This screen shows when your account is a USER account (LIMITED FUNCTIONALITY)*/
        static void UserScreen()
        {
            Start(out var top);

            var welcomeLabel = new Label($"Welcome {currentLogin}") { X = Pos.Center(), Y = Pos.Percent(20f) };

            var userDataButton = new Button("Get user data") { X = Pos.Center(), Y = Pos.Bottom(welcomeLabel) + 1 };
            userDataButton.Clicked += () => { Switch(UserDataScreen); };

            var graphButton = new Button("Show repayment graph") { X = Pos.Center(), Y = Pos.Bottom(userDataButton) + 1 };
            graphButton.Clicked += () => { Switch(RepaymentGraphScreen); };

            var backButton = new Button("Back") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            backButton.Clicked += () => {
                // TODO Create proper logout
                currentLogin = null;
                Switch(LoginScreen);
            };

            top.Add(
                welcomeLabel, userDataButton,
                graphButton, backButton);
            End();
        }
        /* Screen for creating credit for user */
        static void CreateCreditScreen()
        {
            Start(out var top);

            var marStatusLabel = new Label("Maritial status") { X = Pos.Percent(20f), Y = 1 };
            var marStatusRadio = new RadioGroup(new ustring[] { "Single", "Married", "Divorced", "Widow(er)" }) {
                X = Pos.Percent(20f),
                Y = Pos.Bottom(marStatusLabel)
            };
            var isFromTJ = new CheckBox("I am Tajikistan citizen. (checkbox)") { X = Pos.Center(), Y = Pos.Bottom(marStatusRadio) };

            var loanFromTotalLable = new Label("Loan amount from total income: ") { X = Pos.Percent(70f), Y = 1 };
            var loanFromTotalRadio = new RadioGroup(new ustring[] {
              "less than 80%",
              "80%  - 150%",
              "150% - 250%",
              "more than 250%"
            }) { X = Pos.Percent(70f), Y = Pos.Bottom(loanFromTotalLable) };

            var creditHistoryLabel = new Label("Credit history: ") { X = Pos.Percent(20f), Y = Pos.Bottom(isFromTJ) };
            var creditHistoryRadio = new RadioGroup(new ustring[] {
              "More than 3 closed credits",
              "1 - 2 closed credits",
              "No credit history",
            }) { X = Pos.Percent(20f), Y = Pos.Bottom(creditHistoryLabel) };

            var delayLable = new Label("Delay history: ") { X = Pos.Percent(70f), Y = Pos.Bottom(isFromTJ) };
            var delayRadio = new RadioGroup(new ustring[]
            {
              "more than 7",
              "5 - 7",
              "4",
              "less than 3",
            }) { X = Pos.Percent(70f), Y = Pos.Bottom(delayLable) };

            var purposeLabel = new Label("Purpose: ") { X = Pos.Percent(20f), Y = Pos.Bottom(delayRadio) };
            var purposeRadio = new RadioGroup(new ustring[]
            {
              "Appliances",
              "Repair",
              "Phone",
              "Other",
            }) { X = Pos.Percent(20f), Y = Pos.Bottom(purposeLabel) };

            var limitLabel = new Label("Delay history: ") { X = Pos.Percent(70f), Y = Pos.Bottom(delayRadio) + 1 };
            var limitRadio = new RadioGroup(new ustring[]
            {
              "less than 12 months",
              "more than 12 months",
            }) { X = Pos.Percent(70f), Y = Pos.Bottom(limitLabel) };

            var submitButton = new Button("Submit") { X = Pos.Center(), Y = Pos.Bottom(purposeRadio), };

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
                            sMaritStatus, sIsFromTj,
                            sLoanAmount, sCredHistory,
                            sPurpose, sDelHistory, sLimit);

                    var curBalance = SQL.CalculateAccountBalance(currentClientLogin);

                    if (curBalance < 0) { throw new Exception($"Account is already in debit: {curBalance}"); }

                    if (points > Constants.MIN_POINTS) {
                        MessageBox.Query("Congrats", Constants.Congrats, "Ok");

                        var inputWin = new Window("Input the amount")
                        {
                            X = Pos.Center(),
                            Y = Pos.Center(),
                            Width = Dim.Fill(),
                            Height = Dim.Fill()
                        };

                        var inputLabel = new Label("Amount: ") { X = Pos.Center(), Y = Pos.Center() };
                        var inputText = new TextField() { X = Pos.Right(inputLabel), Y = Pos.Center(), Width = 10 };
                        var curr = new Label("Somoni") { X = Pos.Right(inputText), Y = Pos.Center() };

                        var limitLabel = new Label("Limit in months: ") { X = Pos.Center() - 11, Y = Pos.Bottom(inputText) };
                        var limitText = new TextField("6") { X = Pos.Left(inputText), Y = Pos.Bottom(inputText), Width = 10 };

                        var submitButton = new Button("Submit") { X = Pos.Center(), Y = Pos.Bottom(limitText), };

                        submitButton.Clicked += () =>
                        {
                            try {
                                SQL.CreditToAccount(currentClientLogin, decimal.Parse(inputText.Text.ToString()), int.Parse(limitText.Text.ToString()));
                                MessageBox.Query("Success", "Operation was successfull", "Ok");
                                inputWin.RequestStop();
                                Switch(currentLoginPriv);
                            } catch (Exception ex) {
                                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
                                inputText.SetFocus();
                            }
                        };

                        inputWin.Add(
                                    inputLabel, inputText,
                                     curr, limitLabel,
                                     limitText, submitButton);

                        top.Add(inputWin);
                    }
                    else {
                        MessageBox.ErrorQuery("Sorry ...", Constants.SorryMessage, "Ok");
                    }
                } catch (Exception ex) {
                    MessageBox.ErrorQuery("Error !", ex.Message + " " + ex, "Ok");
                }
            };


            top.Add(marStatusLabel,
                marStatusRadio, isFromTJ,
                loanFromTotalLable, loanFromTotalRadio,
                creditHistoryLabel, creditHistoryRadio,
                delayLable, delayRadio,
                purposeLabel, purposeRadio,
                limitLabel, limitRadio,
                submitButton, BackButton(AdminScreen, "to admin menu"));

            End();
        }
        /* Screen for depositing (addding money) to account */
        static void depositToAccountScreen()
        {
            Start(out var top);
            var inputLabel = new Label("Amount: ") { X = Pos.Center(), Y = Pos.Center() };
            var inputText = new TextField() { X = Pos.Right(inputLabel), Y = Pos.Center(), Width = 10 };
            var submitButton = new Button("Submit") { X = Pos.Center(), Y = Pos.AnchorEnd(1) };

            submitButton.Clicked += () => {
                try {
                    var depositAmount = decimal.Parse(inputText.Text.ToString());
                    SQL.DepositToAccount(currentClientLogin, depositAmount);
                    MessageBox.Query("Success", $"Amount of {depositAmount} deposited to {currentClientLogin}", "Ok");
                    Switch(currentLoginPriv);
                } catch (Exception ex) {
                    MessageBox.ErrorQuery("Error !", ex.ToString(), "Ok");
                }
            };
            top.Add(inputLabel, inputText, submitButton);
            End();
        }
        /* This is the admin screen with all the functionality */
        static void AdminScreen()
        {
            Start(out var top);

            var welcomeLabel = new Label("Welcome to the admin screen !") { X = Pos.Center(), Y = 1 };
            var userLabel = new Label($"Now you are working with {currentClientLogin}") { X = Pos.Center(), Y = Pos.Bottom(welcomeLabel) };

            var newCreditButton = new Button("Create new credit for user") { X = Pos.Center(), Y = Pos.Bottom(userLabel) + 3 };
            newCreditButton.Clicked += () => { Switch(CreateCreditScreen); };

            var depositToAccount = new Button("Deposit to account") { X = Pos.Center(), Y = Pos.Bottom(newCreditButton) + 1 };
            depositToAccount.Clicked += () => { Switch(depositToAccountScreen); };

            var userDataButton = new Button("Get user data") { X = Pos.Center(), Y = Pos.Bottom(depositToAccount) + 1 };
            userDataButton.Clicked += () => { Switch(UserDataScreen); };

            var graphButton = new Button("Show repayment graph") { X = Pos.Center(), Y = Pos.Bottom(userDataButton) + 1 };
            graphButton.Clicked += () => { Switch(RepaymentGraphScreen); };

            var backButton = new Button("Back") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            backButton.Clicked += () => {
                // Reset the global variables
                currentLogin = null;
                currentLoginPriv = null;
                currentClientLogin = null;
                Switch(LoginScreen);
            };

            top.Add(
                welcomeLabel, newCreditButton, 
                depositToAccount,
                userLabel, userDataButton,
                graphButton, backButton);

            End();
        }
        /* This screen shows your repayment table */
        static void RepaymentGraphScreen()
        {
            Start(out var top);

            var tableView = new TableView()
            {
                X = 0, Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            top.Add(
                tableView,
                BackButton(currentLoginPriv, "to user profile"));

            var dt = new DataTable ();
			dt.Columns.Add ("Num");
			dt.Columns.Add ("Amount");
			dt.Columns.Add ("Expiration date");

            try {
				int limit = SQL.GetAccountTransactionData(currentClientLogin).Limit;
				var tmpBal = SQL.CalculateAccountBalance(currentClientLogin);
				decimal mean = Math.Round((-1 * tmpBal) / limit, 2);

				MessageBox.Query("Data", $"limit: {limit}\nBalance:{tmpBal}\nmean: {mean}", "Ok");

				for (int i = 0; i < limit; ++i) {
					dt.Rows.Add ((i + 1).ToString(), mean.ToString(), SQL.GetAccountTransactionData(currentClientLogin).Created_At.AddDays(i));
				}

				tableView.Table = dt;
			} catch (Exception ex) {
                MessageBox.ErrorQuery("Error !", ex.ToString(), "Ok");
			}
            End();
        }
        /* This screen shows your activity */
        static void UserTransactsDataScreen()
        {
            Start(out var top);

            var tableView = new TableView() {
                X = 1, Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            try {
                tableView.Table = SQL.GetAccountTransactions(currentClientLogin);
            }
            catch (Exception ex) {
                MessageBox.ErrorQuery("Error !", ex.Message, "Ok");
            }

            top.RemoveAll();
            top.Add(
                tableView,
                BackButton(UserDataScreen, "to user data"));

            End();
        }

        /* Screen for registering new user */
        static void RegisterScreen()
        {
            Start(out var top);

            var regLabel = new Label("Register") { X = Pos.Center(), Y = Pos.Percent(1f) };

            var fnameLabel = new Label("First name: ") { X = Pos.Center() - 20, Y = Pos.Percent(20f) };
            var fnameText = new TextField() { X = Pos.Right(fnameLabel), Y = Pos.Percent(20f), Width = 15 };

            var lnameLabel = new Label("Last name: ") { X = Pos.Left(fnameLabel), Y = Pos.Bottom(fnameText) };
            var lnameText = new TextField() { X = Pos.Right(lnameLabel) + 1, Y = Pos.Bottom(fnameText), Width = 15 };

            var ageLabel = new Label("Age: ") { X = Pos.Left(fnameLabel), Y = Pos.Bottom(lnameText) + 1 };
            var ageText = new TextField() { X = Pos.Left(lnameText), Y = Pos.Bottom(lnameText) + 1, Width = 3 };

            var loginLab = new Label("Login:") { X = Pos.Left(ageLabel), Y = Pos.Bottom(ageText) };
            var loginText = new TextField("+992") {
                X = Pos.Left(ageText),
                Y = Pos.Bottom(ageText),
                /* Phone len in TJ is 9 and +992 prefix and margin */
                Width = 9 + 4 + 1
            };

            var passLab = new Label("Password:") { X = Pos.Left(loginLab), Y = Pos.Bottom(loginLab) };
            var passText = new TextField() { Secret = true, X = Pos.Left(loginText), Y = Pos.Bottom(loginText), Width = 15 };

            var genderLable = new Label("Gender") { X = Pos.Center(), Y = Pos.Bottom(passText) + 2 };
            var genderRadio = new RadioGroup(new ustring[] { "Male", "Female" }) { X = Pos.Center(), Y = Pos.Bottom(genderLable) };

            var isAdminCheckBox = new CheckBox("I am an admin") { X = Pos.Center(), Y = Pos.Bottom(genderRadio) + 1 };

            var submitButton = new Button("Submit") { X = Pos.Center(), Y = Pos.AnchorEnd(1) };

            submitButton.Clicked += () =>
            {
                var _fname = fnameText.Text.ToString();
                var _lname = lnameText.Text.ToString();
                var _ageStr = ageText.Text.ToString();
                var _login = loginText.Text.ToString();
                var _pass = passText.Text.ToString();
                Account newAccount = null;
                try {
					if (!int.TryParse(_ageStr, out var _age))                         throw new Exception("Age can not be empty");
                    if (String.IsNullOrEmpty(_fname) || String.IsNullOrEmpty(_lname)) throw new Exception("Names can not be empty");
                    if (_age <= 0 || _age >= 150)                                     throw new Exception("Invalid age");
                    if (_pass.Length < 6)                                             throw new Exception("Password should be 6 chars or longer");
					// Check age for compilance
					newAccount = new Account()
					{
						FistName =_fname,
						LastName = _lname,
						Age = _age,
						Login = _login,
						Password = _pass,
						/* This two don't need checks. */
						Gender = genderRadio.SelectedItem,
						IsAdmin = isAdminCheckBox.Checked
					};
					bool result = false;
					try {
						result = SQL.CreateAccount(newAccount);
						if (result) MessageBox.Query("Success !", $"Created account {newAccount.Login}", "Ok");
						Switch(MainScreen);
					} 
					catch (System.FormatException) {
						MessageBox.ErrorQuery("Error !", "Please fill age form", "Ok");
					} catch (Exception ex) {
						MessageBox.ErrorQuery("Error !", ex.ToString(), "Ok");
					}
                } catch (Exception ex) {
                    MessageBox.ErrorQuery("Error !", ex.ToString(), "Ok");
                }
            };

            top.Add(regLabel,
                fnameLabel, fnameText,
                lnameLabel, lnameText,
                ageLabel, ageText,
                loginLab, loginText,
                passLab, passText,
                genderLable, genderRadio,
                isAdminCheckBox, submitButton,
                BackButton(MainScreen, "to main screen"));

            End();
        }
        static Button BackButton(System.Action goesWhere, string whereName = "")
        {
            var backButton = new Button($"Back {whereName}") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            backButton.Clicked += () => { Switch(goesWhere); }; return backButton;
        }

        // Screen for login in. Determines your privilage and shows Admin and User screen respectively
        static void LoginScreen()
        {
            Start(out var top);

            var login = new Label("Login: ") { X = Pos.Percent(35f), Y = Pos.Center() };
            var password = new Label("Password: ") { X = Pos.Percent(35f), Y = Pos.Bottom(login) };

            var loginText = new TextField("") { X = Pos.Right(password), Y = Pos.Top(login), Width = 40 };
            var passText = new TextField("") { Secret = true, X = Pos.Left(loginText), Y = Pos.Top(password), Width = Dim.Width(loginText) };

            var doneButton = new Button("Done") { X = Pos.Center(), Y = Pos.Bottom(passText) + 3, };

            doneButton.Clicked += () =>
            {
                string loginRez = loginText.Text.ToString();
                string passRez = passText.Text.ToString();
                if (!SQL.ExistAccount(loginRez)) {
                    MessageBox.ErrorQuery("Error!", $"Account {loginRez} not found.", "Ok");
                    loginText.SetFocus();
                }
                else if (!SQL.Auth(loginRez, passRez)) {
                    MessageBox.ErrorQuery("Error!", $"Wrong password for {loginRez}.", "Ok");
                    passText.SetFocus();
                } else {
                    currentLogin = loginRez;
                    // Check that the logined user is an administrator
                    if (SQL.IsAdmin(currentLogin)) {
                        currentLoginPriv = AdminScreen;
                        Switch(GetCurrentClientScreen);
                    } else {
                        currentLoginPriv = UserScreen;
                        // User works with his account NOT any other account
                        currentClientLogin = currentLogin;
                        Switch(UserScreen);
                    }
                }
            };
            top.Add(login,
                password, loginText,
                passText, doneButton,
                BackButton(MainScreen, "to main"));

            End();
        }
        static void Start(out Terminal.Gui.Toplevel app) { Application.Init(); app = Application.Top; }
        static void Switch(System.Action to) { running = to; Application.RequestStop(); }
        static void End() { Application.Run(); }

        /* Screen for selecting to login or register */
        static void MainScreen()
        {
            Start(out var top);

            var helloLabe = new Label("Hello and welcome !") { X = Pos.Center(), Y = Pos.Percent(20f) };

            var loginButton = new Button("Login") { X = Pos.Center() - 10, Y = Pos.Center() };
            loginButton.Clicked += () => { Switch(LoginScreen); };

            var registerButton = new Button("Register") { X = Pos.Right(loginButton) + 5, Y = Pos.Center() };
            registerButton.Clicked += () => { Switch(RegisterScreen); };

            var exitButton = new Button("Exit") { X = Pos.Percent(5f), Y = Pos.Percent(95f) };
            exitButton.Clicked += () => { Switch(null); };

            top.Add(
                helloLabe, loginButton,
                registerButton, exitButton);

            End();
        }
        /* Current running screen */
        public static Action running = MainScreen;
        static void Main()
        {
            /* Initialize the window */
            try {
                Console.OutputEncoding = System.Text.Encoding.Default;
                while (running != null) {
                    running.Invoke();
                }
                Application.Shutdown();
            } catch (Exception ex) {
                Console.WriteLine("Cannot initialize applicatoin, CHECK YOUR SQL connection string or terminal emulator.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
// Select Id from table order by id desc limit 1