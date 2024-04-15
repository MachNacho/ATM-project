using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_ATM_Project
{

    public class UserAccounts
    {
        private string AccNumber;
        public string AccountNumber
        {
            get { return AccNumber; }
            set { AccNumber = value; }
        }

        private string AccPin;
        public string AccountPIN
        {
            get { return AccPin; }
            set { AccPin = value; }
        }

        private double AccAmt;
        public double AccountAmount
        {
            get { return AccAmt; }
            set { AccAmt = value; }
        }

        public UserAccounts(string A, string B, double C)
        {
            AccNumber = A;
            AccPin = B;
            AccAmt = C;
        }

    }
}
