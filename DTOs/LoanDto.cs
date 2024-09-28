using System;
using System.Text.Json.Serialization;

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
        // LenderEmail is used to find the lender by email
        public required string LenderEmail { get; set; }

        [JsonIgnore]
        public required string LenderId { get; set; }

        // remove from swagger schema
        [JsonIgnore]
        public required string BorrowerId { get; set; }

        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
    }

    public class UpdateLoanStatusDto
    {
        public string Status { get; set; }
    }
}