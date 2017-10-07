using System;
using System.Collections.Generic;
using System.Text;

namespace BankNS
{
    public class Money
    {
        private double value;

        public Money(double val)
        {
            this.value = val;
        }

        public double GetValue() {
            return this.value;
        }

        public void SetValue(double value)
        {
            this.value = value;
        }

        override
        public string ToString()
        {
            return this.value.ToString();
        }

        // Makes new Money object 
        public static Money operator +(Money a, Money b)
        {
            Money money = new Money(0);
            double value = a.GetValue() + b.GetValue();
            money.SetValue(value);
            return money;
        }

        // Makes new Money object 
        public static Money operator -(Money a, Money b)
        {
            Money money = new Money(0);
            double value = a.GetValue() - b.GetValue();
            money.SetValue(value);
            return money;
        }

        public static bool operator <(Money lhs, Money rhs)
        {
            bool status = false;
            if (lhs.GetValue() < rhs.GetValue())
            {
                status = true;
            }
            return status;
        }

        public static bool operator >(Money lhs, Money rhs)
        {
            bool status = false;
            if (lhs.GetValue() > rhs.GetValue())
            {
                status = true;
            }
            return status;
        }

        public static bool operator <=(Money lhs, Money rhs)
        {
            bool status = false;
            if (lhs.GetValue() <= rhs.GetValue())
            {
                status = true;
            }
            return status;
        }

        public static bool operator >=(Money lhs, Money rhs)
        {
            bool status = false;
            if (lhs.GetValue() >= rhs.GetValue())
            {
                status = true;
            }
            return status;
        }
    }
}
