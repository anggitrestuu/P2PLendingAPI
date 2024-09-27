using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2PLendingAPI.Models
{
    public class Loan
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string BorrowerId { get; set; }

        [ForeignKey("BorrowerId")]
        public User Borrower { get; set; }

        [Required]
        public string LenderId { get; set; }

        [ForeignKey("LenderId")]
        public User Lender { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}