using System;

namespace P2PLendingAPI.DTOs
{
    public class RepaymentDto
    {
        public string Id { get; set; }
        public string LoanId { get; set; }
        public decimal Amount { get; set; }
        public decimal RepaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string RepaidStatus { get; set; }
        public DateTime PaidAt { get; set; }
    }

    public class CreateRepaymentDto
    {
        public string LoanId { get; set; }
        public decimal Amount { get; set; }
    }
}