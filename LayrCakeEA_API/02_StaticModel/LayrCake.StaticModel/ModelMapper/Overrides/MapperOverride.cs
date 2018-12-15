using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.DataVisualiserServiceReference;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using MapTool = AutoMapper;
using System;

#pragma warning disable CS0618
namespace LayrCake.StaticModel.ModelMapper
{
    // ReSharper disable InconsistentNaming
    public partial class MapperOverride : MapperLoader
    {
        //internal override void CreateMap_Address()
        //{
        //    MapTool.Mapper.CreateMap<Address, AddressVwm>()
        //        .AfterMap((s, c) => { if (c != null && c.Address11 != null) foreach (var l in c.Address11) { l.Address4 = c; } })
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<AddressVwm, Address>().IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<AddressCriteria, AddressVwmCriteria>().FixSourceCriteria().IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<AddressVwmCriteria, AddressCriteria>().FixCriteria().IgnoreAllNonExisting();

        //}
        //internal override void CreateMap_ChallengeDetail()
        //{
        //    MapTool.Mapper.CreateMap<ChallengeDetail, ChallengeDetailVwm>()
        //        .ForMember(dest => dest.Created.DateTime, map => map.MapFrom(sors => (DateTime)sors.Created))
        //        .ForMember(dest => dest.Updated.Value.DateTime, map => map.MapFrom(sors => (DateTime?)sors.Updated))
        //        .ForMember(dest => dest.Deleted.Value.DateTime, map => map.MapFrom(sors => (DateTime?)sors.Deleted))
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<ChallengeDetailVwm, ChallengeDetail>()
        //        .ForMember(dest => (DateTime)dest.Created, map => map.MapFrom(sors => (DateTimeOffset)sors.Created.DateTime))
        //        .ForMember(dest => (DateTime?)dest.Updated, map => map.MapFrom(sors => sors.Updated != null ? (DateTimeOffset?)sors.Updated.Value.DateTime : null))
        //        .ForMember(dest => (DateTime)dest.Deleted, map => map.MapFrom(sors => sors.Updated != null ? (DateTimeOffset?)sors.Deleted.Value.DateTime : null))
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<ChallengeDetailCriteria, ChallengeDetailVwmCriteria>().FixSourceCriteria().IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<ChallengeDetailVwmCriteria, ChallengeDetailCriteria>().FixCriteria().IgnoreAllNonExisting();

        //}

        //internal static DDDElementSimpleVwm ToViewModelObject(DDDElement dataTransferObject)
        //{
        //    return MapTool.Mapper.Map<DDDElement, DDDElementSimpleVwm>(dataTransferObject);
        //}

        //internal static DDDElement FromViewModelObject(DDDElementSimpleVwm businessObject)
        //{
        //    return MapTool.Mapper.Map<DDDElementSimpleVwm, DDDElement>(businessObject);
        //}

        //internal virtual void CreateMap_DDDMethod()
        //{
        //    MapTool.Mapper.CreateMap<DDDMethod, DDDMethodVwm>()
        //        //.ForMember(t => t.DDDElement, opt => opt.Ignore())
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<DDDMethodVwm, DDDMethod>().IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<DDDMethodCriteria, DDDMethodVwmCriteria>().FixSourceCriteria().IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<DDDMethodVwmCriteria, DDDMethodCriteria>().FixCriteria().IgnoreAllNonExisting();
        //}

    }
    // ReSharper restore InconsistentNaming
}
#pragma warning restore CS0618
