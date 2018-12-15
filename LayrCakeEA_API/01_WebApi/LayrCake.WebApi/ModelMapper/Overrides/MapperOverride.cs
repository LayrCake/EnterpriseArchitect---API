using System;
using LayrCake.WebApi.Models.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using MapTool = AutoMapper;

#pragma warning disable CS0618
namespace LayrCake.WebApi.ModelMapper
{
    // ReSharper disable InconsistentNaming
    public partial class MapperOverride : MapperLoader
    {
        //internal override void CreateMap_Address()
        //{
        //    MapTool.Mapper.CreateMap<Address, AddressVwm>()
        //            .ForMember(dest => dest.AddressID, map => map.MapFrom(
        //                    src => MySqlFuncs.IntParse(src.Id)))
        //                    //src => src.Id))
        //            .FixDataObjDest()
        //        .AfterMap((s, c) => { if (c != null && c.Address11 != null) foreach (var l in c.Address11) { l.Address4 = c; } })
        //        .IgnoreAllNonExisting();
        //    MapTool.Mapper.CreateMap<AddressVwm, Address>()
        //            .ForMember(dest => dest.Id, map => map.MapFrom(
        //                    src => MySqlFuncs.LTRIM(MySqlFuncs.StringConvert(src.AddressID))))
        //            .ForMember(dest => dest.updatedAt, map => map.MapFrom(
        //                    src => src.Updated == null ? src.Created : src.Updated))
        //            .FixApiObjDest()
        //            .IgnoreAllNonExisting();
        //}

    }
    // ReSharper restore InconsistentNaming
}
#pragma warning restore CS0618
