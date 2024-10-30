#if UNIT_TEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZ_Cheeseria.Server.Controllers;

namespace PZ_Cheeseria.Server.Tests
{
    [TestClass]
    public class CheeseControllerTests
    {
        // Tests whether the controller has been created (SUCCESSFUL IF: TRUE)
        [TestMethod]
        public void Test_Controller()
        {
            var controller = new CheeseController();
            var result = controller.Get();

            Assert.IsNotNull(result);
        }

        // Tests if the parameters have values (SUCCESSFUL IF: TRUE)
        [TestMethod]
        public void Test_Parameters()
        {
            var controller = new CheeseController();
            var result = controller.Get();

            int counter = 0;

            foreach (var cheese in result)
            {
                counter++;
                Assert.IsTrue(cheese.Index == counter);
                Assert.IsFalse(string.IsNullOrEmpty(cheese.Name));
                Assert.IsFalse(string.IsNullOrEmpty(cheese.Colour));
                Assert.IsTrue(cheese.Price > 0);
            }
        }

        // Tests if the cheeses lists contain 5 elements (SUCCESSFUL IF: TRUE)
        [TestMethod]
        public void Test_Count5()
        {
            var controller = new CheeseController();
            var result = controller.Get();

            var cheeses = result.ToList();
            Assert.AreEqual(5, cheeses.Count);
        }

        // Tests if a specific cheese is in the list (SUCCESSFUL IF: TRUE)
        [TestMethod]
        public void Test_FindValidCheese()
        {
            var controller = new CheeseController();
            var result = controller.Get();

            var cheeses = result.ToList();

            Assert.IsNotNull(cheeses.FirstOrDefault(c => c.Name == "Stilton"));
        }

        // Tests if a specific cheese is in the list (SUCCESSFUL IF: FALSE)
        [TestMethod]
        public void Test_FindInValidCheese()
        {
            var controller = new CheeseController();
            var result = controller.Get();

            var cheeses = result.ToList();

            Assert.IsNull(cheeses.FirstOrDefault(c => c.Name == "Babybel"));
        }
    }
}
#endif