namespace P2PLendingAPI.DTOs
{
    public class FundLoanDto
    {
        public required string LoanId { get; set; }
        public decimal Amount { get; set; }
    }
}