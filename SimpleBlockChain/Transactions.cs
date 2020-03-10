using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlockChain
{
    class Transactions
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public double Amount { get; private set; }

        public Transactions(string from, string to, double amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }

    }
}
