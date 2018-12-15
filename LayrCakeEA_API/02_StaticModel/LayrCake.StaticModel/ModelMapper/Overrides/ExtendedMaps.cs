using Infrastructure.Helpers;
using MapTool = AutoMapper;
using LayrCake.StaticModel.DataVisualiserServiceReference;
using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using System.Security.Claims;
using AutoMapper;
using System;

#pragma warning disable CS0618
namespace LayrCake.StaticModel.ModelMapper.Overrides
{
    public static class ExtendedMaps
    {
        public static void CreateMap()
        {
            MapTool.Mapper.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(d => d.UtcDateTime);
            MapTool.Mapper.CreateMap<DateTime?, DateTime>().ConvertUsing<DateTimeConverter>();
            MapTool.Mapper.CreateMap<DateTime?, DateTime?>().ConvertUsing<NullableDateTimeConverter>();
            MapTool.Mapper.CreateMap<DateTimeOffset?, DateTime>().ConvertUsing<DateTimeOffsetConverter>();
            MapTool.Mapper.CreateMap<DateTimeOffset?, DateTime?>().ConvertUsing<NullableDateTimeOffsetConverter>();

            // Can map these due to constructor
            //AutoMapper.Mapper.CreateMap<Claim, AspNetUserClaimVwm>();
            //AutoMapper.Mapper.CreateMap<AspNetUserClaimVwm, Claim>();

            //CreateMap_DDDElement_Simple();
        }

        public class DateTimeConverter : TypeConverter<DateTime?, DateTime>
        {
            protected override DateTime ConvertCore(DateTime? source)
            {
                if (source.HasValue)
                    return source.Value;
                else
                    return default(DateTime);
            }
        }

        public class NullableDateTimeConverter : TypeConverter<DateTime?, DateTime?>
        {
            protected override DateTime? ConvertCore(DateTime? source)
            {
                return source;
            }
        }

        public class DateTimeOffsetConverter : TypeConverter<DateTimeOffset?, DateTime>
        {
            protected override DateTime ConvertCore(DateTimeOffset? source)
            {
                if (source.HasValue)
                    return source.Value.DateTime;
                else
                    return default(DateTime);
            }
        }

        public class NullableDateTimeOffsetConverter : TypeConverter<DateTimeOffset?, DateTime?>
        {
            protected override DateTime? ConvertCore(DateTimeOffset? source)
            {
                return source.Value.DateTime;
            }
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
