using Mapster;
using PlanShare.Communication.Requests;

namespace PlanShare.Application.Services.Mappings;
public static class MapConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestRegisterUserJson, Domain.Entities.User>
            .NewConfig()
            .Ignore(dest => dest.Password);

        TypeAdapterConfig<RequestRegisterWorkItemJson, Domain.Entities.WorkItem>
            .NewConfig()
            .Map(dest => dest.DueDate, source => source.DueDate.Date)
            .Map(dest => dest.Assignees, source => source.Assignees.Distinct());

        TypeAdapterConfig<Guid, Domain.Entities.Assignee>
            .NewConfig()
            .Map(dest => dest.UserId, source => source);

        TypeAdapterConfig<RequestUpdateWorkItemJson, Domain.Entities.WorkItem>
            .NewConfig()
            .Map(dest => dest.DueDate, source => source.DueDate.Date);
    }
}
