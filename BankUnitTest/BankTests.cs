using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankNS;


namespace BankTests
{
    [TestClass]
    public class BankTests
    {
        Bank bank;
        Money amount;
        Person person, receiver;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            this.bank = new Bank();
            this.amount = new Money(500);
            this.person = new Person("Per Persen", this.amount);
            this.receiver = new Person("Anne Annesen", this.amount);
        }
        
        [TestMethod]
        public void GetAccountsForCustomer_NoAccounts_ReturnsEmpty()
        {
            // arrange
            int expected = 0;
            
            // act
            List<Account> accounts = this.bank.GetAccountsForCustomer(this.person);

            // assert
            int actual = accounts.Count;
            Assert.AreEqual(expected, actual, "Person is expected to not have any accounts");                       
        }

        [TestMethod]
        public void Deposit_ValidAmount_ChangesDeposit()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = new Account(this.person, empty);
            double expected = 500;

            // act
            this.bank.Deposit(emptyaccount, new Money(500));

            // assert
            double actual = emptyaccount.GetBalance().GetValue();
            Assert.AreEqual(expected, actual);     

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Deposit_InvalidAmount_ShouldThrowException()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = new Account(this.person, empty);

            // act
            this.bank.Deposit(emptyaccount, new Money(600));        
         }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_NegativeAmount_ShouldThrowException()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = new Account(this.person, empty);

            // act
            this.bank.Deposit(emptyaccount, new Money(-600));
        }

        [TestMethod]
        public void Withdraw_ValidAmount_ChangeBalance()
        {
            // arrange
            Account account = new Account(this.person, this.amount);
            double expected = 200;

            // act
            this.bank.Withdraw(account, new Money(300));

            // assert
            double actual = account.GetBalance().GetValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Withdraw_InvalidAmount_ShouldThrowException()
        {
            // arrange
            Account account = new Account(this.person, this.amount);

            // act
            this.bank.Withdraw(account, new Money(600));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_NegativeAmount_ShouldThrowException()
        {
            // arrange
            Account account = new Account(this.person, this.amount);

            // act
            this.bank.Withdraw(account, new Money(-400));
        }

        [TestMethod]
        public void Transfer_ValidAmount_ChangeBalance()
        {
            // arrange
            Account from = new Account(this.person, new Money(500));
            Account to = new Account(this.receiver, new Money(0));
            double exp_value_from = 0;
            double exp_value_to = 500;

            // act
            bank.Transfer(from, to, new Money(500));

            // assert
            double actual_value_from = from.GetBalance().GetValue();
            double actual_value_to = to.GetBalance().GetValue();
            Assert.AreEqual(exp_value_from, actual_value_from);
            Assert.AreEqual(exp_value_to, actual_value_to);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Transfer_InvalidAmount_ChangeBalance()
        {
            // arrange
            Account from = new Account(this.person, new Money(400));
            Account to = new Account(this.receiver, new Money(0));

            // act
            bank.Transfer(from, to, new Money(500));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Transfer_NegativeAmount_ChangeBalance()
        {
            // arrange
            Account from = new Account(this.person, new Money(400));
            Account to = new Account(this.receiver, new Money(0));

            // act
            bank.Transfer(from, to, new Money(-500));
        }
        
    }

}
