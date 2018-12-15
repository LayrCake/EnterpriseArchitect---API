using System;
using System.Diagnostics;
using Infrastructure.Core;
using Mapper = LayrCake.WebApi.ModelMapper.Mapper;
using LayrCake.StaticModel.ModelMapper.Overrides;
using LayrCake.WebApi.ModelMapper.Overrides;

namespace LayrCake.WebApi.ApiReserved
{
    internal static class MapperLoad
    {
        internal static bool ObjectsLoaded = false;

        static MapperLoad()
        {
            LayrCake.StaticModel.ModelMapper.Overrides.ExtendedMaps.CreateMap();
            LayrCake.WebApi.ModelMapper.Overrides.ExtendedMaps.CreateMap();
            MapperLoadObjects();
        }

        internal static bool MapperLoadObjects()
        {
            if (ObjectsLoaded) return ObjectsLoaded;
            if (Mapper.Mapper_Load())
                ObjectsLoaded = true;

            try
            {
                AutoMapper.Mapper.AssertConfigurationIsValid();
                Debug.Assert(ObjectsLoaded, "LayrCake.WebApi.ModelMapper - MapperLoadObjects failed.");
            }
            catch (Exception exception)
            {
                Trace.Fail("LayrCake.WebApi - Mapper_Security_Load failed: " + exception.Message + " " +
                           (exception.InnerException != null ? exception.InnerException.Message : ""));
                ErrorHandler.Throw(exception);
                ObjectsLoaded = false;
            }

            Trace.Assert(ObjectsLoaded, "LayrCake.WebApi Mapper failed to load all objects");
            return ObjectsLoaded;
        }
    }
}
