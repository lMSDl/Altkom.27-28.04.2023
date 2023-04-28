namespace ConsoleApp.Test.NUnit
{

    public class GardenTest
    {

/*        #region BAD_PRACTISE
        private Garden Garden { get; set; }

        [SetUp]
        public void Setup()
        {
            Garden = new Garden(1);
        }

        [TearDown]
        public void TearDown()
        {
            Garden = null;
        }
        #endregion*/

        [Theory]
        [TestCase(0)]
        [TestCase(10)]
        public void Garden_ValidSize_SizeInit(int size)
        {
            // Arrange & Act
            var garden = new Garden(size);

            // Assert
            Assert.That(garden.Size, Is.EqualTo(size));
        }

        [Theory]
        //testujemy warunki brzegowe
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(11)]
        [TestCase(int.MaxValue)]
        public void Garden_InvalidSizeSize_ArgumentOutOfRangeException(int size)
        {
            // Arrange & Act
            TestDelegate action = () => new Garden(size);

            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.That(exception.ParamName, Is.EqualTo("size"));
        }

        [Test]
        public void Plant_ValidName_True()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 1;
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert 
            Assert.That(result, Is.True);
        }

        [Test]
        public void Plant_OverflowGarden_False()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 0;
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert
            Assert.That(result, Is.False);
        }

        [Theory]
        [TestCase(null)]
        [TestCase("", "Roœlina musi posiadaæ nazwê!")]
        [TestCase(" ", "Roœlina musi posiadaæ nazwê!")]
        [TestCase("\t", "Roœlina musi posiadaæ nazwê!")]
        public void Plant_InvalidName_ArgumentException(string invalidName, string exprectedMessage = "")
        {
            // Arrange
            Garden garden = GetGardenWithInsignificantSize();

            // Act
            TestDelegate action = () => garden.Plant(invalidName);

            // Assert
            Assert.Throws(Is.InstanceOf<ArgumentException>()
                .And.Property("ParamName").EqualTo("name")
                .And.Property("Message").Contain(exprectedMessage), action);
        }

        [Test]
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
            Assert.That(resultCount, Is.EqualTo(EXPECTED_RESULT_COUNT));
            Assert.That(plants, Does.Contain(VALID_NAME));
        }

        [Test]
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
            Assert.That(garden.GetPlants(), Does.Contain(POST_NAME));
        }

        [Test]
        public void GetPlants_CopyOfPlantsCollection()
        {
            // Arrange
            Garden garden = GetGardenWithInsignificantSize();

            // Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            // Assert
            Assert.That(result1, Is.Not.SameAs(result2));
        }

        private static Garden GetGardenWithInsignificantSize()
        {
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);
            return garden;
        }
    }
}