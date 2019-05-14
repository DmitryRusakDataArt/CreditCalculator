using System;

namespace Models
{
    public class LoanSummaryItem
    {
        public decimal WeeklyRepayment { get; set; }

        public decimal TotalRepaid { get; set; }

        public decimal TotalInterest { get; set; }

        public override bool Equals(object obj)
        {
            var info = obj as LoanSummaryItem;

            if(info == null)
            {
                return false;
            }

            return info.WeeklyRepayment == WeeklyRepayment &&
                   info.TotalRepaid == TotalRepaid &&
                   info.TotalInterest == TotalInterest;
        }

        public override int GetHashCode()
        {
            var hashCode = -120860152;
            hashCode = hashCode * -1521134295 + WeeklyRepayment.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalRepaid.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalInterest.GetHashCode();
            return hashCode;
        }
    }
}
