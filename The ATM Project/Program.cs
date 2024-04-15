// LOG FILE LOCATED IN THE : TheAtm Project\bin\debug\DV2022F7K4F6_ASSIGNMENT_Logs.txt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.IO;

namespace The_ATM_Project
{
    internal class Program
    {
        public static class Globals// This is to holds variables that are global
        {
            public static string InputAccNumber;// holds current Account     
            public static double AccountBalance;//holds the account balance
            public static bool VerifiedAccount = false;//used to verify account
            public static double TransactAmt;//holds amount to be transacted
        }
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("DV2022F7K4F6_ASSIGNMENT_Logs.txt");// writes to a textfile, this creates the logs

            var Acc = new List<UserAccounts>();//Account list using objects from Account class 
            //
            //
            //
            UserAccounts Acc1 = new UserAccounts("1234567890", "1234", 1000);
            UserAccounts Acc2 = new UserAccounts("0876543210", "5678", 2000);
            UserAccounts Acc3 = new UserAccounts("987654321", "8765", 3000);
            Acc.Add(Acc1);//Account 1
            Acc.Add(Acc2);//Account 2
            Acc.Add(Acc3);//Account 3
            //
            //
            //
            Transaction transaction = new Transaction();//Transaction class for transactions
            Encryption encryption = new Encryption();//Use for encryption class
             
            do//for the start of the program, This looks for the account number.
            {
                Console.WriteLine("Please enter your Account Number ");
                Globals.InputAccNumber = Console.ReadLine();   //user enters the account number of the account they want to log into
                
                foreach (UserAccounts acc in Acc)// runs through the list of accounts
                {
                    if (Globals.InputAccNumber == acc.AccountNumber)// checks if user entered account matches with any account on the list
                    {
                        Console.Write("PIN:");
                        string AccountPin = Console.ReadLine();//user enters the pin of the account of their choice
                        if (encryption.Verify(AccountPin,acc.AccountPIN))//Verifies pin through the VERIFY from encryption class
                        {
                            Globals.AccountBalance = acc.AccountAmount;// adds the account amount to a global variable
                            Globals.VerifiedAccount = true;
                        }                                            
                    }
                }
                if (Globals.VerifiedAccount==false)//this will display a message if the check fails
                {
                    Console.WriteLine("Please try again, press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }                  
            } 
            while (Globals.VerifiedAccount == false);
            sw.WriteLine ("Account logged in: "+Globals.InputAccNumber+" Time: "+System.DateTime.Now);
            sw.Flush();
            bool CorrectChoice = false;// allows for do while loop to run
            do//while loop for choices in main menu
            {               
                MenuList();//displays menu items
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1"://check Balance
                        Console.Clear();
                        Console.WriteLine("Account Number: " + Globals.InputAccNumber);
                        Console.WriteLine("Account Balance: R" + Globals.AccountBalance);
                        Console.WriteLine("Press any Key to Continue");
                        Console.ReadKey();
                        MenuList();
                        break;
                    case "2":// Withdraw amount
                        Console.WriteLine("Please enter the amount you need to withdraw");
                        Console.Write("Amount: R");
                        double AmtBef = 0;
                        try
                        {
                            AmtBef = Globals.AccountBalance;// holds the before amount
                            Globals.TransactAmt = Convert.ToDouble(Console.ReadLine());
                            Globals.AccountBalance = transaction.Subtract(Globals.AccountBalance, Globals.TransactAmt);//Method that check if it can subtract
                            Console.WriteLine("Amount left: R" + Globals.AccountBalance.ToString());
                            if(AmtBef != Globals.AccountBalance)//checks if there was a change in the amount
                            sw.WriteLine("Account Withdraw: " + Globals.TransactAmt + " Time: " + System.DateTime.Now);
                            sw.Flush();
                        }
                        catch (Exception e) // Exception Handling
                        {
                            Console.WriteLine("Transaction Error, Returning to Main Menu");
                        }                       
                        Console.WriteLine("Press any Key to Continue");
                        Console.ReadKey();
                        MenuList();
                        break;
                    case "3":// Deposit amount
                        Console.WriteLine("Please enter the amount you need to withdraw");
                        Console.WriteLine("Before Amount: R" + Globals.AccountBalance.ToString());
                        Console.Write("Amount: R");
                        try
                        {
                            Globals.TransactAmt = Convert.ToDouble(Console.ReadLine());
                            Globals.AccountBalance = transaction.Add(Globals.AccountBalance, Globals.TransactAmt);// method that adds amount
                            Console.WriteLine("After Amount: R" + Globals.AccountBalance.ToString());
                            sw.WriteLine("Account Deposited: " + Globals.TransactAmt + " Time: "+System.DateTime.Now);
                            sw.Flush();
                        }
                        catch(Exception e)//Exception Handling
                        {
                            Console.WriteLine("Transaction Error, Returning to Main Menu");
                        }      
                        Console.WriteLine("Press any Key to Continue");
                        Console.ReadKey();
                        MenuList();
                        break;
                    case "4"://Transfer amount
                        Console.WriteLine("Please enter the account number to transfer to");
                        string ToAcc = Console.ReadLine();
                        bool AccFound = false;
                        if (ToAcc == Globals.InputAccNumber)//checks if the user entered the same account number as the one logged in
                        {
                            Console.WriteLine("Error, cant transfer funds to self");
                            Console.WriteLine("Press any Key to Continue");
                            Console.ReadKey();
                            break;
                        }
                        foreach (UserAccounts acc in Acc)
                        {
                            if (ToAcc == acc.AccountNumber)//cecks if an account matches the requested one
                            {
                                AccFound = true;
                                if (AccFound == true)// if found will begin transfer process
                                {
                                    Console.WriteLine("How much do you wish to transfer");
                                    Console.Write("Amount: R");
                                    try
                                    {
                                        Globals.TransactAmt = Convert.ToDouble(Console.ReadLine());
                                    }
                                    catch (Exception e)//Exception Handling
                                    {                                        
                                        Console.WriteLine("Transaction Error, Returning to Main Menu");
                                        Console.WriteLine("Press any Key to Continue");
                                        Console.ReadKey();
                                        break;
                                    }
                                    double AmountChange = Globals.AccountBalance;
                                    Globals.AccountBalance = transaction.Subtract(Globals.AccountBalance, Globals.TransactAmt);// method to check if it can subtract
                                    Console.WriteLine("Amount: R" + Globals.AccountBalance.ToString());
                                    if (AmountChange != Globals.AccountBalance)// if there is a difference in the before and after amount it will update everyones amount
                                    {
                                        acc.AccountAmount = acc.AccountAmount + Globals.TransactAmt;
                                        sw.WriteLine(Globals.InputAccNumber+" Transferd funds to: "+ ToAcc+ " Amount: "+ Globals.TransactAmt+" Time:"+ System.DateTime.Now);//logs the transfer
                                        sw.Flush();
                                    }
                                    Console.WriteLine("Press any Key to Continue");
                                    Console.ReadKey();
                                }
                            }
                        }
                        if (AccFound == false)// if the account doesnt exist
                        {
                            Console.WriteLine("Account not found");
                            Console.WriteLine("Returning to main menu");
                            Console.WriteLine("Press any Key to Continue");
                            Console.ReadKey();
                        }
                        break;
                    case "5"://Log Out
                        sw.WriteLine(Globals.InputAccNumber + " logged out");//logs the log out
                        sw.Flush();
                        //repeats the proccess of signing someone in
                        foreach (UserAccounts acc in Acc)
                        {
                            if (Globals.InputAccNumber == acc.AccountNumber)
                            {
                                     acc.AccountAmount= Globals.AccountBalance;
                            }
                        }
                        Console.Clear();
                        Globals.VerifiedAccount = false;
                        do
                        {
                            Console.WriteLine("Please enter your Account Number ");
                            Globals.InputAccNumber = Console.ReadLine();
                            foreach (UserAccounts acc in Acc)
                            {
                                if (Globals.InputAccNumber == acc.AccountNumber)
                                {
                                    Console.Write("PIN:");
                                    string AccountPin = Console.ReadLine();
                                    if (encryption.Verify(AccountPin, acc.AccountPIN))//Verifies pin through the VERIFY from encryption class
                                    {
                                        Globals.AccountBalance = acc.AccountAmount;
                                        Globals.VerifiedAccount = true;
                                    }
                                    
                                }
                            }
                            if (Globals.VerifiedAccount == false)
                            {
                                Console.WriteLine("Please try again, press any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        while (Globals.VerifiedAccount == false);
                        sw.WriteLine("Account logged in:" + Globals.InputAccNumber + System.DateTime.Now);
                        sw.Flush();
                        break;               
                    case "6"://Exit
                        Console.Clear();
                        Console.WriteLine("Exiting....");
                        Console.WriteLine("Log files are located in: TheAtm Project\\bin\\debug\\DV2022F7K4F6_ASSIGNMENT_Logs.txt");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        sw.WriteLine("Session Ended Time: " + System.DateTime.Now);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error input");//user misinputs
                        break;
                }
            } while (CorrectChoice == false);     
        }
      static void MenuList()// the menu list 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account Number: " + Globals.InputAccNumber);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome user");
            Console.WriteLine("Welcome to main Menu");
            Console.WriteLine("Option 1: Check Balance");
            Console.WriteLine("Option 2: Withdraw Balance");
            Console.WriteLine("Option 3: Deposit Balnce");
            Console.WriteLine("Option 4: Transfer Balnce");
            Console.WriteLine("Option 5: Log Out");
            Console.WriteLine("Option 6: Exit");

        } 
    }
}