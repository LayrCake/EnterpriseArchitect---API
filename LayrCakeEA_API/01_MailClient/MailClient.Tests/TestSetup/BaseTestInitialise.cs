using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ExpressionSerialization;
using LayrCake.StaticModel;

namespace MailClient.Tests
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
