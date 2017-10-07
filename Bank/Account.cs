using System;
using System.Collections.Generic;
using System.Text;

namespace BankNS
{
    public class Account
    {
        private Money balance;
        private string name;
        private int id;
        private Person owner;

        public Account(Person owner, Money initialDeposit)
        {
            if (owner.GetAccountByID(id) != null) {
                throw new Exception(owner.GetName() + " already owns an account with ID " + owner.GetCounter().ToString());
            }
            this.id = owner.GetCounter() + 1;
            this.name = owner.GetName() + ' ' + this.id.ToString();
            this.balance = initialDeposit;
            this.owner = owner;
        }        

        public void EditBalance(Money money)
        {
            this.balance += money;
            this.owner.SetMoney(owner.GetMoney() - money);
            
        }

        public Person GetOwner()
        {
            return this.owner;
        }

        public Money GetBalance()
        {
            return this.balance;
        }

        public int GetID()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }

        override
        public string ToString()
        {
            return this.name.PadRight(17) + "| " + this.balance.ToString().PadLeft(12);
        }
       
    }
}
