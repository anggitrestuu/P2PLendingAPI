using System;

namespace P2PLendingAPI.DTOs
{
    public class FundingDto
    {
        public string Id { get; set; }
        public string LoanId { get; set; }
        public string LenderId { get; set; }
        public string LenderName { get; set; }
        public decimal Amount { get; set; }
        public DateTime FundedAt { get; set; }
    }

    public class CreateFundingDto
    {
        public string LoanId { get; set; }
        public string LenderId { get; set; }
        public decimal Amount { get; set; }
    }
}