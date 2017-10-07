using System;
using System.Collections.Generic;
using System.Text;

namespace BankNS
{
    public class Bank {

        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            Account createdaccount = customer.AddAccount(initialDeposit);
            return createdaccount;
        }

        public List<Account> GetAccountsForCustomer(Person customer)
        {
            return customer.GetAccounts();
        }

        public void Deposit(Account to, Money amount)
        {
            if (to.GetOwner().GetMoney() < amount)
            {
                throw new Exception(to.GetOwner().GetName() + " does not have enough money to deposit the amount " + amount + " to the account " + to.GetName());
            }
            if (amount.GetValue() < 0)
            {
                throw new ArgumentOutOfRangeException("amount");              
            }
            to.EditBalance(amount);
        }

        public void Withdraw(Account from, Money amount)
        {
            if (from.GetBalance() < amount)
            {
                throw new Exception("The account " + from.GetName() + "'s balance does not cover withdrawal amount " + amount + '.');
            } 
            if (amount.GetValue() < 0) 
            {
                throw new ArgumentOutOfRangeException("amount");
            }
            Money withdrawal = new Money(-amount.GetValue());
            from.EditBalance(withdrawal);

        }

        public void Transfer(Account from, Account to, Money amount)
        {
            this.Withdraw(from, amount);
            this.Deposit(to, amount);
        }
        
        static void Main(string[] args)
        {
       
            var am1 = new Money(300);
            var am2 = new Money(1000);
            var am400 = new Money(800);

            var sindre = new Person("Sindre", am2);
            var anja = new Person("Anja From", am1);
            var bank = new Bank();

            var account1 = bank.CreateAccount(anja, am1);
            var account2 = bank.CreateAccount(sindre, am1);
            Console.WriteLine(sindre.AccountsTable());
            bank.Deposit(account2, am1);
            Console.WriteLine(sindre.AccountsTable());
            Console.WriteLine(sindre.GetMoney().ToString());

          
            Console.WriteLine(sindre.AccountsTable());
           // Console.WriteLine(anja.AccountsTable());
            // Console.WriteLine(anja.GetAccountByID(1).ToString());

        }        
    }
}
