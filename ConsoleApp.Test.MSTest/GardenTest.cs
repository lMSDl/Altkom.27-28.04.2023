using System;

namespace ConsoleApp.Test.MSTest
{
    [TestClass]
    public class GardenTest
    {

        /*#region BAD_PRACTISE
        private Garden Garden { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Garden = new Garden(1);
        }

        [TestCleanup]
        public void TearDown()
        {
            Garden = null;
        }
        #endregion*/

        [TestMethod]
        [DataRow(0)]
        [DataRow(10)]
        public void Garden_ValidSize_SizeInit(int size)
        {
            // Arrange & Act
            var garden = new Garden(size);

            // Assert
            Assert.AreEqual(size, garden.Size);
        }

        [TestMethod]
        //testujemy warunki brzegowe
        [DataRow(int.MinValue)]
        [DataRow(-1)]
        [DataRow(11)]
        [DataRow(int.MaxValue)]
        public void Garden_InvalidSizeSize_ArgumentOutOfRangeException(int size)
        {
            // Arrange & Act
            Action action = () => new Garden(size);

            // Assert
            var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
            Assert.AreEqual("size", exception.ParamName);
        }

        [TestMethod]
        public void Plant_ValidName_True()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 1;
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert 
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Plant_OverflowGarden_False()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 0;
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("", "Roœlina musi posiadaæ nazwê!")]
        [DataRow(" ", "Roœlina musi posiadaæ nazwê!")]
        [DataRow("\t", "Roœlina musi posiadaæ nazwê!")]
        public void Plant_InvalidName_ArgumentException(string invalidName, string expectedMessage)
        {
            // Arrange
            Garden garden = GetGardenWithInsignificantSize();

            // Act
            Action action = () => garden.Plant(invalidName);

            //Assert
            var argumentException = Assert.ThrowsException<ArgumentException>(action);
            Assert.AreEqual("name", argumentException.ParamName);
            Assert.IsTrue(argumentException.Message.Contains(expectedMessage));
        }

        [TestMethod]
        //[DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Plant_NullName_ArgumentException(/*string invalidName*/)
        {
            // Arrange
            Garden garden = GetGardenWithInsignificantSize();

            // Act
            garden.Plant(null);
        }

        [TestMethod]
        public void Plant_ValidName_AddedToCollection()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 2;
            const string VALID_NAME = "a";
            const int EXPECTED_RESULT_COUNT = 1;
            var garden = new Garden(MINIMAL_VALID_SIZE);


            // Act
            garden.Plant(VALID_NAME);

            // Assert
            var plants = garden.GetPlants();
            var resultCount = plants.Count();
            Assert.AreEqual(EXPECTED_RESULT_COUNT, resultCount);
            Assert.IsTrue(garden.GetPlants().Contains(VALID_NAME));
        }

        [TestMethod]
        public void Plant_ExistingName_ChangedNameOnCollection()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 2;
            const string VALID_NAME = "a";
            const string POST_NAME = VALID_NAME + "2";
            var garden = new Garden(MINIMAL_VALID_SIZE);
            garden.Plant(VALID_NAME);

            // Act
            garden.Plant(VALID_NAME);

            // Assert
            Assert.IsTrue(garden.GetPlants().Contains(POST_NAME));
        }

        [TestMethod]
        public void GetPlants_CopyOfPlantsCollection()
        {
            // Arrange
            Garden garden = GetGardenWithInsignificantSize();

            // Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            // Assert
            Assert.AreNotSame(result1, result2);
        }

        private static Garden GetGardenWithInsignificantSize()
        {
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);
            return garden;
        }
    }
}