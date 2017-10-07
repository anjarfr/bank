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
        public void CreateAccount_ValidAmount_CreatesAccount()
        {
            // arrange 
            double expected_balance = 500;
            string expected_name = "Per Persen";
            double expected_money_remaining = 0;
            int expected_id = 1;

            // act
            Account newaccount = this.bank.CreateAccount(this.person, this.amount);

            // assert
            double actual_balance = newaccount.GetBalance().GetValue();
            string actual_name = newaccount.GetOwner().GetName();
            double actual_money_remaining = newaccount.GetOwner().GetMoney().GetValue();
            int actual_id = newaccount.GetID();
            Assert.AreEqual(expected_balance, actual_balance);
            Assert.AreEqual(expected_name, actual_name);
            Assert.AreEqual(expected_money_remaining, actual_money_remaining);
            Assert.AreEqual(expected_id, actual_id);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAccount_InvalidAmount_ThrowsException()
        {
            // act
            this.bank.CreateAccount(this.person, new Money(1000));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateAccount_NegativeAmount_ThrowsException()
        {
            // act
            this.bank.CreateAccount(this.person, new Money(-300));
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
        public void GetAccountsForCustomer_HasAccounts_ReturnsAccounts()
        {
            // arrange
            Account account = this.bank.CreateAccount(this.person, this.amount);
            List<Account> expected = new List<Account>();
            expected.Add(account);

            // act
            List<Account> actual = bank.GetAccountsForCustomer(this.person);

            // assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected.Count, actual.Count);
        }


        [TestMethod]
        public void Deposit_ValidAmount_ChangesBalance()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = bank.CreateAccount(this.person, empty);
            double expected = 500;
            double expected_money_remaining = 0;

            // act
            this.bank.Deposit(emptyaccount, new Money(500));

            // assert
            double actual = emptyaccount.GetBalance().GetValue();
            double actual_money_remaining = emptyaccount.GetOwner().GetMoney().GetValue();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected_money_remaining, actual_money_remaining);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Deposit_InvalidAmount_ShouldThrowException()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = this.bank.CreateAccount(this.person, empty);

            // act
            this.bank.Deposit(emptyaccount, new Money(600));        
         }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_NegativeAmount_ShouldThrowException()
        {
            // arrange
            Money empty = new Money(0);
            Account emptyaccount = bank.CreateAccount(this.person, empty);

            // act
            this.bank.Deposit(emptyaccount, new Money(-600));
        }

        [TestMethod]
        public void Withdraw_ValidAmount_ChangesBalance()
        {
            // arrange
            Account account = bank.CreateAccount(this.person, this.amount);
            double expected = 200;
            double expected_money_remaining = 300;

            // act
            this.bank.Withdraw(account, new Money(300));

            // assert
            double actual = account.GetBalance().GetValue();
            
            double actual_money_remaining = account.GetOwner().GetMoney().GetValue();
            Assert.AreEqual(expected, actual);            
            Assert.AreEqual(expected_money_remaining, actual_money_remaining);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Withdraw_InvalidAmount_ShouldThrowException()
        {
            // arrange
            Account account = this.bank.CreateAccount(this.person, this.amount);

            // act
            this.bank.Withdraw(account, new Money(100000));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_NegativeAmount_ShouldThrowException()
        {
            // arrange
            Account account = this.bank.CreateAccount(this.person, this.amount);

            // act
            this.bank.Withdraw(account, new Money(-400));
        }

        [TestMethod]
        public void Transfer_ValidAmount_ChangesBalance()
        {
            // arrange
            Account from = this.bank.CreateAccount(this.person, new Money(500));
            Account to = this.bank.CreateAccount(this.receiver, new Money(0));
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
        public void Transfer_InvalidAmount_ThrowsException()
        {
            // arrange
            Account from = this.bank.CreateAccount(this.person, new Money(400));
            Account to = this.bank.CreateAccount(this.receiver, new Money(0));

            // act
            bank.Transfer(from, to, new Money(500));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Transfer_NegativeAmount_ThrowsException()
        {
            // arrange
            Account from = this.bank.CreateAccount(this.person, new Money(400));
            Account to = this.bank.CreateAccount(this.receiver, new Money(0));

            // act
            bank.Transfer(from, to, new Money(-5000));
        }
        
    }

}
