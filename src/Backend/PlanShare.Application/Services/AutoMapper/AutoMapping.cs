using AutoMapper;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Application.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestRegisterWorkItemJson, Domain.Entities.WorkItem>()
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(source => source.DueDate.Date))
            .ForMember(dest => dest.Assignees, opt => opt.MapFrom(source => source.Assignees.Distinct()));

        CreateMap<Guid, Domain.Entities.Assignee>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source));

        CreateMap<RequestUpdateWorkItemJson, Domain.Entities.WorkItem>()
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(source => source.DueDate.Date));
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

        CreateMap<Domain.Entities.User, ResponseAssigneeJson>();

        CreateMap<Domain.Entities.WorkItem, ResponseShortWorkItemJson>();
    }
}
