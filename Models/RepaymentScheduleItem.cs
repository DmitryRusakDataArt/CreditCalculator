using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RepaymentScheduleItem
    {
        public int InstallmentNumber { get; set; }

        public decimal AmountDue { get; set; }

        public decimal Principal { get; set; }

        public decimal Interest { get; set; }

        public override bool Equals(object obj)
        {
            var info = obj as RepaymentScheduleItem;

            if(info == null)
            {
                return false;
            }

            return info.InstallmentNumber == InstallmentNumber &&
                   info.AmountDue == AmountDue &&
                   info.Principal == Principal &&
                   info.Interest == Interest;
        }

        public override int GetHashCode()
        {
            var hashCode = -272501761;
            hashCode = hashCode * -1521134295 + InstallmentNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + AmountDue.GetHashCode();
            hashCode = hashCode * -1521134295 + Principal.GetHashCode();
            hashCode = hashCode * -1521134295 + Interest.GetHashCode();
            return hashCode;
        }
    }
}
