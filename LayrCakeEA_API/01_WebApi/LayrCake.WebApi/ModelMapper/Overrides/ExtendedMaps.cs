using Infrastructure.Helpers;
using MapTool = AutoMapper;
using LayrCake.WebApi.Models.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using LayrCake.WebApi.Models;

#pragma warning disable CS0618
namespace LayrCake.WebApi.ModelMapper.Overrides
{
    public static class ExtendedMaps
    {
        public static void CreateMap()
        {

            // Can map these due to constructor
            //AutoMapper.Mapper.CreateMap<Claim, AspNetUserClaimVwm>();
            //AutoMapper.Mapper.CreateMap<AspNetUserClaimVwm, Claim>();

            //CreateMap_DDDElement_Simple();
            RegisterGoJsMappings();
        }
        public static void RegisterGoJsMappings()
        {
            MapTool.Mapper.CreateMap<DDDElementVwm, GoJsElement>()
                .ForMember(dest => dest.key, opts => opts.MapFrom(src => src.DDDElementID))
                .ForMember(dest => dest.name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.methods, opts => opts.MapFrom(src => src.DDDMethods))
                .FixApiObjDest()
                .IgnoreAllNonExisting()

            //.AfterMap((s, c) => { if (c != null && c.Methods != null) foreach (var l in c.Methods) { l.Element = c; } })
            ;
            MapTool.Mapper.CreateMap<GoJsElement, DDDElementVwm>()
                .ForMember(dest => dest.DDDElementID, opts => opts.MapFrom(src => src.key))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name))
                .ForMember(dest => dest.DDDMethods, opts => opts.MapFrom(src => src.methods))
                .FixDataObjDest()
                .IgnoreAllNonExisting()
                //.AfterMap((s, c) => { if (c != null && c.DDDMethods != null) foreach (var l in c.DDDMethods) { l.DDDElement = c; } })
            ;

            MapTool.Mapper.CreateMap<DDDMethodVwm, GoJsMethod>()
                .ForMember(dest => dest.key, opts => opts.MapFrom(src => src.DDDMethodID))
                .ForMember(dest => dest.name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.returnType, opts => opts.MapFrom(src => src.DataType))
                .FixApiObjDest()
                .IgnoreAllNonExisting()
                ;
            MapTool.Mapper.CreateMap<GoJsMethod, DDDMethodVwm> ()
                .ForMember(dest => dest.DDDMethodID, opts => opts.MapFrom(src => src.key))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name))
                .ForMember(dest => dest.DataType, opts => opts.MapFrom(src => src.returnType))
                .FixDataObjDest()
                .IgnoreAllNonExisting()
                ;

            MapTool.Mapper.CreateMap<DDDConnectorVwm, GoJsConnector>()
                .ForMember(dest => dest.key, opts => opts.MapFrom(src => src.DDDConnectorID))
                .ForMember(dest => dest.from, opts => opts.MapFrom(src => src.FromElement_Ref))
                .ForMember(dest => dest.to, opts => opts.MapFrom(src => src.ToElement_Ref))
                .FixApiObjDest()
                .IgnoreAllNonExisting()
                ;
            MapTool.Mapper.CreateMap<GoJsConnector, DDDConnectorVwm>()
                .ForMember(dest => dest.DDDConnectorID, opts => opts.MapFrom(src => src.key))
                .ForMember(dest => dest.FromElement_Ref, opts => opts.MapFrom(src => src.from))
                .ForMember(dest => dest.ToElement_Ref, opts => opts.MapFrom(src => src.to))
                .FixDataObjDest()
                .IgnoreAllNonExisting()
                ;
        }
        //public static void CreateMap_DDDElement_Simple()
        //{
        //    MapTool.Mapper.CreateMap<DDDElement, DDDElementSimpleVwm>()
        //        //.AfterMap((s, c) => { if (c != null && c.DDDAttributes != null) foreach (var l in c.DDDAttributes) { l.DDDElement = c; } })
        //        //.AfterMap((s, c) => { if (c != null && c.DDDMethods != null) foreach (var l in c.DDDMethods) { l.DDDElement = c; } })
        //        //.AfterMap((s, c) => { if (c != null && c.DDDProperties != null) foreach (var l in c.DDDProperties) { l.DDDElement = c; } })
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<DDDElementSimpleVwm, DDDElement>().IgnoreAllNonExisting();
        //}

    }
}
#pragma warning restore CS0618
