using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Pagination;

namespace TicketTracking.Core.Mapping;
public static class MapsterConfiguration
{
    public static void AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        RegisterMappings(config);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Priority, PriorityDto>();

        config.NewConfig<Status, StatusDto>();

        config.NewConfig<TicketType, TicketDto>();

        config.NewConfig<TicketReply, TicketReplyDto>();

        config.NewConfig<Ticket, TicketDto>();

        config.NewConfig<Ticket, TicketListItemDto>();

        config.ForType(typeof(PagedCollection<>), typeof(PagedCollection<>))
            .Map("Items", "Items")
            .PreserveReference(true);

        config.NewConfig<TicketRequestDto, Ticket>();
    }
}
