using System;
using System.Diagnostics;
using Infrastructure.Core;
using Mapper = LayrCake.StaticModel.ModelMapper.Mapper;
using LayrCake.StaticModel.ModelMapper.Overrides;

namespace LayrCake.StaticModel.StaticModelReserved
{
    internal static class MapperLoad
    {
        internal static bool ObjectsLoaded = false;

        static MapperLoad()
        {
            ExtendedMaps.CreateMap();
            MapperLoadObjects(); }

        internal static bool MapperLoadObjects()
        {
            if (ObjectsLoaded) return ObjectsLoaded;
            if (Mapper.Mapper_Load())
                ObjectsLoaded = true;

            try
            {
                AutoMapper.Mapper.AssertConfigurationIsValid();
                Debug.Assert(ObjectsLoaded, "LayrCake.ActionService.ModelMapper - MapperLoadObjects failed.");
            }
            catch (Exception exception)
            {
                Trace.Fail("LayrCake.ActionService - Mapper_Security_Load failed: " + exception.Message + " " +
                           (exception.InnerException != null ? exception.InnerException.Message : ""));
                ErrorHandler.Throw(exception);
                ObjectsLoaded = false;
            }

            Trace.Assert(ObjectsLoaded, "LayrCake.ActionService Mapper failed to load all objects");
            return ObjectsLoaded;
        }
    }
}
