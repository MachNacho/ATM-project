// all log files will be located in the project folder => bin=> debug
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_ATM_Project
{
    internal class Transaction
    {
        
        internal double Subtract(double Account,double Transact)//withdraw
        {
         if((Account+1) > Transact) // if user is able to succesfully deduct amount from account
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Transaction succesfull");
                Console.ForegroundColor = ConsoleColor.White;
                return (Account - Transact);
            }
         else // if user account fails to deduct
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Transaction failed");
                Console.ForegroundColor = ConsoleColor.White;
                return Account;
            }
        }

        internal double Add(double Account,double Transact) //Deposit
        {
            return (Account + Transact);
        }
    }
}
