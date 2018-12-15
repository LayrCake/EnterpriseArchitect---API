using System.Diagnostics;

namespace LayrCake.StaticModel.StaticModelReserved
{
    internal static class StaticModelConfig
    {
        internal static bool ModelsLoaded;
        internal static bool _configured;
        static object _configureLock = new object();

        internal static bool Configure()
        {
            Config();
            Trace.Assert(ModelsLoaded, "ActionService Models have not been loaded");
            return true;
        }

        internal static void Config()
        {
            if (ModelsLoaded)
                return;
            lock (_configureLock)
            {
                ModelsLoaded = MapperLoad.MapperLoadObjects();
                if (ModelsLoaded)
                {
                    Debug.WriteLine("ActionService Models loaded");
                    return;
                }
            }
        }
    }
}
