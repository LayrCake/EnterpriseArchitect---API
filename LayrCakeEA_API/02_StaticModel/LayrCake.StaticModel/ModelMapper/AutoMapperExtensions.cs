using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using StaticModel.ViewModelObjects;
using LayrCake.StaticModel.ViewModelObjects;
using LayrCake.StaticModel.DataVisualiserServiceReference;

#pragma warning disable CS0618
namespace LayrCake.StaticModel.ModelMapper
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> FixViewModelObjDest<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : BaseViewModelObject
            where TSource : DataTransferObject
        {
            //mappingExpression.ForMember(dest => dest.Updated, map => map.MapFrom(
            //            sors => sors == true ? 1 : (int?)null));
            mappingExpression.ForMember(dest => dest.ValidationErrors, opt => opt.Ignore());
            mappingExpression.ForMember(dest => dest.ValidationErrorCodes, opt => opt.Ignore());
            //mappingExpression.ForMember(dest => dest.ValidationErrorCodesk__BackingField, o => o.Ignore());
            //mappingExpression.ForMember(dest => dest.ExtensionData, o => o.Ignore());
            //mappingExpression.ForMember(dest => dest.Updated, map => map.MapFrom(
            //    src => src.Updated == null ? src.Created : src.Updated));

            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> FixWcfObjDest<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : DataTransferObject
            where TSource : BaseViewModelObject
        {
            //mappingExpression.ForMember(dest => dest.Updated, map => map.MapFrom(
            //            sors => sors == true ? 1 : (int?)null));
            mappingExpression.ForMember(dest => dest.ValidationErrors, opt => opt.Ignore());
            mappingExpression.ForMember(dest => dest.ValidationErrorCodes, opt => opt.Ignore());
            //mappingExpression.ForMember(dest => dest.ValidationErrorCodesk__BackingField, o => o.Ignore());
            //mappingExpression.ForMember(dest => dest.ExtensionData, o => o.Ignore());

            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> FixSourceCriteria<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : Criteria.VwmCriteria
        {
            //mappingExpression.ForMember(dest => dest.WhereExpression, opt => opt.Ignore());
            //mappingExpression.ForMember(dest => dest.Pagination, opt => opt.Ignore());

            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> FixCriteria<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : LayrCake.StaticModel.DataVisualiserServiceReference.Criteria
        {
            //mappingExpression.ForMember(dest => dest.WhereExpression, opt => opt.Ignore());
            //mappingExpression.ForMember(dest => dest.Pagination, opt => opt.Ignore());

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
#pragma warning restore CS0618
