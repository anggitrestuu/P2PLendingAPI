using System;

namespace P2PLendingAPI.DTOs
{
    public class LoanDto
    {
        public string Id { get; set; }
        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateLoanDto
    {
        public string BorrowerId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
    }

    public class UpdateLoanStatusDto
    {
        public string Status { get; set; }
    }
}