using AutoMapper;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Models;

namespace P2PLendingAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<Loan, LoanDto>()
                .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.Borrower.Name));
            CreateMap<CreateLoanDto, Loan>();

            CreateMap<Funding, FundingDto>()
                .ForMember(dest => dest.LenderName, opt => opt.MapFrom(src => src.Lender.Name));
            CreateMap<CreateFundingDto, Funding>();

            CreateMap<Repayment, RepaymentDto>();
            CreateMap<CreateRepaymentDto, Repayment>();
        }
    }
}