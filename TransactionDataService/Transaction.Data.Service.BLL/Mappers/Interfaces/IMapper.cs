namespace Transaction.Data.Service.BLL.Mappers
{
    public interface IMapper<Tin,Tout>
    {
        Tout Map(Tin model);
    }
}
