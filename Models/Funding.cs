using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2PLendingAPI.Models
{
    public class Funding
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string LoanId { get; set; }

        [ForeignKey("LoanId")]
        public Loan Loan { get; set; }

        [Required]
        public string LenderId { get; set; }

        [ForeignKey("LenderId")]
        public User Lender { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime FundedAt { get; set; }
    }
}