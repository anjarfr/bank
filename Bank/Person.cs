using System;
using System.Collections.Generic;
using System.Text;


/*
 * Class Person
 * 
 */

namespace BankNS
{
    public class Person
    {
        private string name;
        private Money money;
        private List<Account> accounts = new List<Account>();
        private int accountcounter = 0;
        
        public Person(string name, Money money)
        {
            this.name = name;
            this.money = money;
        }
        
        public string GetName()
        {
            return this.name;
        }

        // Adds a new account to array of accounts
        public Account AddAccount(Money amount)
        {
            if (amount <= this.money)
            {
                Account newaccount = new Account(this, amount);
                accounts.Add(newaccount);
                this.accountcounter++;
                this.money -= amount;
                return newaccount;
            } else
            {
                throw new Exception(this.name + " does not have enough money.");
            }
        }
        
        public List<Account> GetAccounts()
        {
            return this.accounts;
        }

        public Account GetAccountByID(int id)
        {
            foreach (Account account in this.accounts)
            {
                if (account.GetID() == id)
                {
                    return account;
                }
              
            }
            return null;
        }

        public int GetCounter()
        {
            return this.accountcounter;
        }

        public Money GetMoney()
        {
            return this.money;
        }

        public void SetMoney(Money money)
        {
            this.money = money;
        } 

        public string AccountsTable()
        {
            string header = "NAME".PadRight(17) + "| BALANCE".PadRight(14);
            string table = header + "\n-------------------------------";
            if (this.accounts.Count > 0)
            {
                for (int i=0; i < this.accounts.Count; i++)
                {
                    table += '\n' + this.accounts[i].ToString();
                }
                table += '\n';
            }

            return table;
        }
    }
}
