using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using StaticModel.ViewModelObjects;
using LayrCake.WebApi.Models;
using LayrCake.StaticModel.ViewModelObjects;

namespace LayrCake.WebApi.ModelMapper
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> FixDataObjDest<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : BaseViewModelObject
            where TSource : AzureEntityModel
        {
            mappingExpression
                .ForMember(dest => (int?)dest.DeletedBy, map => map.MapFrom(
                        sors => sors.Deleted == true ? 1 : (int?)null))
                .ForMember(dest => dest.Deleted, map => map.MapFrom(
                        sors => sors.Deleted == true ? (DateTime?)DateTime.Now : (DateTime?)null))
                .ForMember(dest => dest.Created, map => map.MapFrom(
                        sors => sors.createdAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Updated, map => map.MapFrom(
                        sors => sors.updatedAt != null ? (DateTime?)DateTime.Now : (DateTime?)null))
                .ForMember(dest => dest.ValidationErrors, opt => opt.Ignore())
                .ForMember(dest => dest.ValidationErrorCodes, opt => opt.Ignore());
            //mappingExpression.ForMember(dest => dest.ValidationErrorCodesk__BackingField, o => o.Ignore());
            //mappingExpression.ForMember(dest => dest.ExtensionData, o => o.Ignore());

            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> FixApiObjDest<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : AzureEntityModel
            where TSource : BaseViewModelObject
        {
            mappingExpression
                .ForMember(dest => dest.updatedAt, map => map.MapFrom(
                        sors => sors.Updated != null ? sors.Updated : DateTime.MinValue))
                .ForMember(dest => dest.Deleted, map => map.MapFrom(
                        sors => sors.Deleted != null ? true : false))
                .ForMember(dest => dest.createdAt, map => map.MapFrom(
                        sors => sors.Created))
                .ForMember(dest => dest.updatedAt, map => map.MapFrom(
                    src => src.Updated == null ? src.Created : src.Updated));

            return mappingExpression;
        }


        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }


        public static IMappingExpression<TSource, TDestination> InheritMappingFromBaseType<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression,
                                                                             WithBaseFor baseFor = WithBaseFor.Both)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var sourceParentType = baseFor == WithBaseFor.Both || baseFor == WithBaseFor.Source
                                       ? sourceType.BaseType
                                       : sourceType;

            var destinationParentType = baseFor == WithBaseFor.Both || baseFor == WithBaseFor.Destination
                                            ? destinationType.BaseType
                                            : destinationType;


            mappingExpression
                .BeforeMap((x, y) => AutoMapper.Mapper.Map(x, y, sourceParentType, destinationParentType))
                .ForAllMembers(x => x.Condition(r => NotAlreadyMapped(sourceParentType, destinationParentType, r)));

            return mappingExpression;
        }


        private static bool NotAlreadyMapped(Type sourceType, Type desitnationType, ResolutionContext r)
        {
            return !r.IsSourceValueNull &&
                   AutoMapper.Mapper.FindTypeMapFor(sourceType, desitnationType).GetPropertyMaps().Where(
                       m => m.DestinationProperty.Name.Equals(r.MemberName)).Select(y => !y.IsMapped()).All(b => b);
        }

    }

    /// <summary>
    /// https://blogs.msdn.microsoft.com/azuremobile/2014/05/22/tables-with-integer-keys-and-the-net-backend/
    /// </summary>
    public static class MySqlFuncs
    {
        public static string StringConvert(int number)
        {
            return number.ToString();
        }
        public static string LTRIM(string s)
        {
            return s == null ? null : s.TrimStart();
        }
        public static int IntParse(string s)
        {
            int ret;
            int.TryParse(s, out ret);
            return ret;
        }
        //public static long LongParse(string s)
        //{
        //    long ret;
        //    long.TryParse(s, out ret);
        //    return ret;
        //}
    }

    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source)
        {
            Type type = Assembly.GetExecutingAssembly().GetType(source);
            return type;
        }
    }

    public interface ITypeConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source);
    }

    public enum WithBaseFor
    {
        Source,
        Destination,
        Both
    }
}
