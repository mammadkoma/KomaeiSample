namespace KomaeiSample.Server.Config;
public static class MapsterConfigs
{
    public static void RegisterMapsterConfigs(this IServiceCollection services)
    {
        //TypeAdapterConfig<Order, OrderOfficeDto>.NewConfig()
        //.Map(dest => dest.ModelTitle, src => src.Envelope.Model.Title)
        //.Map(dest => dest.PaperTitle, src => src.Envelope.Paper.Title)
        //.Map(dest => dest.GrammageTitle, src => src.Envelope.Grammage.Title)
        //.Map(dest => dest.CountTitle, src => src.Envelope.Count.Title)
        //.Map(dest => dest.HasInternalTeramTitle, src => src.Envelope.HasInternalTeram == 1 ? "دارد" : "ندارد")
        //.Map(dest => dest.HasDoorGlueTitle, src => src.Envelope.HasDoorGlue == 1 ? "دارد" : "ندارد")
        //;

        TypeAdapterConfig<User, UserDto>.NewConfig()
        .Map(dest => dest.OrdersCount, src => src.OrderAddUsers.Where(x => x.OrderStatusId != OrderStatusesEnum.Card.ToInt()).Count());
    }
}