using System;
using System.Collections.Generic;
using System.Text;

/*
 * MAIN CLASS
 * 
 */ 

namespace BankNS
{
    public class Bank {



        // Creates account. Account objects must only be created through this method.
        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            if (initialDeposit.GetValue() < 0)
            {
                throw new ArgumentOutOfRangeException("Initial deposit is negative");
            }
            Account createdaccount = customer.AddAccount(initialDeposit);
            Console.WriteLine("Debug info: The account " + createdaccount + " has been created.");
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
                throw new Exception(to.GetOwner() + " does not have enough money to deposit the amount " + amount + " to the account " + to);
            }
            if (amount.GetValue() < 0)
            {
                throw new ArgumentOutOfRangeException("amount");              
            }
            to.EditBalance(amount);
            Console.WriteLine("Debug info: "+amount + " has been deposited to the account " + to);
        }

        public void Withdraw(Account from, Money amount)
        {
            if (from.GetBalance() < amount)
            {
                throw new Exception("The account " + from + "'s balance does not cover withdrawal amount " + amount + '.');
            } 
            if (amount.GetValue() < 0) 
            {
                throw new ArgumentOutOfRangeException("amount");
            }
            Money withdrawal = new Money(-(amount.GetValue()));
            from.EditBalance(withdrawal);
            Console.WriteLine("Debug info: "+amount + " has been withdrawn from the account " + from);

        }

        public void Transfer(Account from, Account to, Money amount)
        {
            this.Withdraw(from, amount);
            if (from.GetOwner() != to.GetOwner())
            {
                from.GetOwner().SetMoney(new Money(-amount.GetValue()));
                to.GetOwner().SetMoney(amount);
            }
            this.Deposit(to, amount);
            Console.WriteLine("Debug info: "+amount + " has been transfered from the account " + from + " to the account " + to);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bank class - visual representation.\n");
            Bank bank = new Bank();

            Console.WriteLine("1. Creating a new Money object:");
            Money amount = new Money(1000);
            Console.WriteLine("\tMoney object has value: " +amount.ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n2. Creating Person object:");
            Person person = new Person("Anne Andersen", amount);
            Console.WriteLine("\tPerson created: " + person.GetName().ToString());
            Console.WriteLine("\t"+person.GetName().ToString() + " has " + person.GetMoney().GetValue().ToString());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n3. Creating empty account for "+ person.GetName().ToString());
            Money empty = new Money(0);
            Account account1 = bank.CreateAccount(person, empty);
            Console.WriteLine("\nList of this person's accounts:");
            Console.WriteLine(person.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n4. Depositing 200 to bank account: ");
            bank.Deposit(account1, new Money(200));
            Console.WriteLine("\n"+person.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n5. Withdrawing 100 from account.");
            bank.Withdraw(account1, new Money(100));
            Console.WriteLine("\n" + person.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n6. Creating new account for same person.\n");
            Account account2 = bank.CreateAccount(person, empty);
            Console.WriteLine("\n"+person.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n7. Transfering from account " + account1 + " to account " + account2 + ".");
            bank.Transfer(account1, account2, new Money(100));
            Console.WriteLine("\n" + person.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\n8. Creating new person with new account and transfering money to " + account1+".");
            Person person2 = new Person("Per Son", new Money(3000000));
            Console.WriteLine("Person " + person2 + " was created.");
            Account account3 = bank.CreateAccount(person2, new Money(3000000));
            Console.WriteLine("\n" + person2.AccountsTable()+"\n");
            bank.Transfer(account3, account1, new Money(1500000));
            Console.WriteLine("\n"+person.AccountsTable());
            Console.WriteLine("\n" + person2.AccountsTable());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("End of presentation.");














        }        
    }
}
