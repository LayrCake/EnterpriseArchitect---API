using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ExpressionSerialization;

namespace LayrCake.StaticModel.Tests
{
    [TestClass]
    public abstract class BaseTestInitialise
    {
        //internal ExpressionSerializer _expressionSerializer = new ExpressionSerializer();

        [TestInitialize]
        public virtual void TestInitialise()
        {
            new ClassAssemblyLoad();
            //AssemblyLoader.Main();
        }
    }
}
