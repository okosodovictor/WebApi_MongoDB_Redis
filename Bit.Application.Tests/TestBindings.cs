using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Linq;
using System.Reflection;

namespace Bit.Application.Tests
{
    [TestClass]
    public class TestBindings
    {
        [TestMethod]
        public void TestBinding()
        {
            //Create Kernel and Load Assembly Application.Web
            //var kernel = new StandardKernel();
            //kernel.Load(new Assembly[] { Assembly.Load("Bit.WebApi") });
            //var query = from types in Assembly.Load("Bit.Application").GetExportedTypes()
            //            where types.IsInterface
            //            where types.Namespace.StartsWith("Bit.Application.Interfaces")
            //            select types;
            //foreach (var i in query.ToList())
            //{
            //    kernel.Get(i);
            //}
        }
    }
}
