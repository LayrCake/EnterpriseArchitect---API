using LayrCake.StaticModel.ViewModelObjects.Implementation;
using Google.Apis.Gmail.v1.Data;
using MailClient.Models;

#pragma warning disable CS0618
namespace MailClient.Mapping
{
    public static class MailClientSetup
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<AspNetUserExtVwm, AspNetUserVwm>();
            AutoMapper.Mapper.CreateMap<AspNetUserVwm, AspNetUserExtVwm>();

            AutoMapper.Mapper.CreateMap<Label, EmailLabel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
                .ForMember(dest => dest.MessageListVisibility, opts => opts.MapFrom(src => src.MessageListVisibility))
                .ForMember(dest => dest.MessagesTotal, opts => opts.MapFrom(src => src.MessagesTotal))
                .ForMember(dest => dest.MessagesUnread, opts => opts.MapFrom(src => src.MessagesUnread))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ThreadsTotal, opts => opts.MapFrom(src => src.ThreadsTotal))
                .ForMember(dest => dest.ThreadsUnread, opts => opts.MapFrom(src => src.ThreadsUnread))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
                ;

    //        AutoMapper.Mapper.CreateMap<Message, EmailMessage>()
    //            .AfterMap((s, c) => { if (c != null && c.Payload != null) 
    //                foreach (var p in c.Payload.Where(item => item.   )) { l.Address4 = p; } })
    //        AutoMapper.Mapper.CreateMap<EmailMessage, Message>();

    //        MapTool.Mapper.CreateMap<Address, AddressVwm>()
    //.AfterMap((s, c) => { if (c != null && c.Address11 != null) foreach (var l in c.Address11) { l.Address4 = c; } })
    //.IgnoreAllNonExisting();

            //.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                //.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
                //.ForMember(dest => dest.HistoryId, opts => opts.MapFrom(src => src.HistoryId))
                //.ForMember(dest => dest.InternalDate, opts => opts.MapFrom(src => src.InternalDate))
                //.ForMember(dest => dest.LabelIds, opts => opts.MapFrom(src => src.LabelIds))
                //.ForMember(dest => dest.Raw, opts => opts.MapFrom(src => src.Raw))
                //.ForMember(dest => dest.SizeEstimate, opts => opts.MapFrom(src => src.SizeEstimate))
                //.ForMember(dest => dest.Snippet, opts => opts.MapFrom(src => src.Snippet))
                //.ForMember(dest => dest.ThreadId, opts => opts.MapFrom(src => src.ThreadId))
                //;
        }
    }
}
#pragma warning restore CS0618
