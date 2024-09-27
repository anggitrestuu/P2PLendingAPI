using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2PLendingAPI.Models
{
    public class Repayment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string LoanId { get; set; }

        [ForeignKey("LoanId")]
        public Loan Loan { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal RepaidAmount { get; set; }

        [Required]
        public decimal BalanceAmount { get; set; }

        [Required]
        public string RepaidStatus { get; set; }

        public DateTime PaidAt { get; set; }
    }
}