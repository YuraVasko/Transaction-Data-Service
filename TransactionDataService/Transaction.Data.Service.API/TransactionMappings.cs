using AutoMapper;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Constants;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.DAL.Models;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.API
{
    public class TransactionMappings : Profile
    {
        public TransactionMappings()
        {
            CreateMap<TransactionDto, TransactionModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Payment.Amount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Payment.CurrencyCode))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(GetTransactionStatus));

            CreateMap<TransactionModel, TransactionDetailsResponse>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => $"{src.Amount} {src.CurrencyCode}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }

        private TransactionStatus GetTransactionStatus(TransactionDto source, TransactionModel destination, TransactionStatus member, ResolutionContext context)
        {
            switch (source.Status)
            {
                case TransactionCsvStatus.Approved: return TransactionStatus.A;
                case TransactionCsvStatus.Failed: return TransactionStatus.R;
                case TransactionCsvStatus.Finished: return TransactionStatus.D;
                case TransactionXmlStatus.Rejected: return TransactionStatus.R;
                case TransactionXmlStatus.Done: return TransactionStatus.D;
                default: throw new InvalidTransactionStatusException(source.Status);
            }
        }
    }
}
