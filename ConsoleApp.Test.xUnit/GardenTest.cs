namespace ConsoleApp.Test.xUnit
{
    /*#region BAD_PRACTISE
public class GardenTest : IDisposable
{

    private Garden Garden { get; set; }

    //odpowiednik Setup w xUnit
    public GardenTest()
    {
        Garden = new Garden(1);
    }

    //odpowiednik TearDown w xUnit
    public void Dispose()
    {
        Garden = null;
    } 
    #endregion*/

    public class GardenTest
    {

        [Theory]
        //testujemy warnuki brzegowe
        [InlineData(0)]
        [InlineData(10)]
        public void Garden_ValidSize_SizeInit(int size)
        {
            // Arrange & Act
            var garden = new Garden(size);

            // Assert
            Assert.Equal(size, garden.Size);
        }

        [Theory]
        //testujemy warunki brzegowe
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(11)]
        [InlineData(int.MaxValue)]
        public void Garden_InvalidSizeSize_ArgumentOutOfRangeException(int size)
        {
            // Arrange & Act
            Action action = () => new Garden(size);

            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.Equal("size", exception.ParamName);
        }

        [Fact]
        //nazwaMetody_scenariusz_oczekiwanyRezultat
        //public void Plant_PassValidName_ReturnsTrue()
        public void Plant_ValidName_True()
        //Plant_GivesTrueWhenProvidedValidName - alternatywnie
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 1; //piszemy testy z minimalnym przekazem i opisujemy swoje intencje
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert
            // testujemy tylko jedn¹ rzecz
            Assert.True(result);
        }

        [Fact]
        public void Plant_OverflowGarden_False()
        {
            // Arrange
            const int MINIMAL_VALID_SIZE = 0;
            const string VALID_NAME = "a";
            var garden = new Garden(MINIMAL_VALID_SIZE);

            // Act
            var result = garden.Plant(VALID_NAME);

            // Assert
            Assert.False(result);
        }

        /* [Fact]
         public void Plant_OverflowGarden_False()
         {
             // Arrange
             const int MINIMAL_VALID_SIZE = 1;
             const string VALID_NAME = "a";
             var garden = new Garden(MINIMAL_VALID_SIZE);
             garden.Plant(VALID_NAME);

             // Act
             var result = garden.Plant(VALID_NAME);

             // Assert
             Assert.False(result);
         }*/

        [Theory]
        [InlineData(null)]
        [InlineData("", "Roœlina musi posiadaæ nazwê!")]
        [InlineData(" ", "Roœlina musi posiadaæ nazwê!")]
        [InlineData("\t", "Roœlina musi posiadaæ nazwê!")]
        public void Plant_InvalidName_ArgumentException(string invalidName, string exprectedMessage = "")
        {
            // Arrange
            Garden garden = GetGardenWIhtInsignificantSize();

            // Act
            Action action = () => garden.Plant(invalidName);

            // Assert
            var argumentException = Assert.ThrowsAny<ArgumentException>(action);
            Assert.Equal("name", argumentException.ParamName);
            Assert.Contains(exprectedMessage, argumentException.Message);

        }

        [Fact(Skip = "Replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_NullName_ArgumentNullException()
        {
            // Arrange
            Garden garden = GetGardenWIhtInsignificantSize();
            string? NULL_NAME = null;

            // Act
            Action action = () => garden.Plant(NULL_NAME);

            // Assert
            // mo¿emy mieæ wiele assert, jeœli doczytcz¹ tej samej testowanej rzeczy
            var argumentNullException = Assert.Throws<ArgumentNullException>(action); //sprawdza rzeczywiœty typ wyj¹tku
            //var argumentException = Assert.ThrowsAny<ArgumentException>(action); //sprawdza czy wyj¹tek jest hierarchi dziedziczenia
            Assert.Equal("name", argumentNullException.ParamName);
        }

        [Fact(Skip = "Replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_WhiteSpaceName_ArgumentException()
        {
            // Arrange
            Garden garden = GetGardenWIhtInsignificantSize();
            string? WHITE_SPACE_NAME = " ";

            // Act

            var exception = Record.Exception(() => garden.Plant(WHITE_SPACE_NAME));

            // Assert
            Assert.NotNull(exception);
            var argumentException = Assert.IsType<ArgumentException>(exception);
            Assert.Equal("name", argumentException.ParamName);
            Assert.Contains("Roœlina musi posiadaæ nazwê!", argumentException.Message);
        }

        [Fact]
        public void Plant_ValidName_AddedToCollection()
        //Plant_GivesTrueWhenProvidedValidName - alternatywnie
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
            Assert.Equal(EXPECTED_RESULT_COUNT, resultCount);
            Assert.Contains(VALID_NAME, plants);
        }

        [Fact]
        public void Plant_ExistingName_ChangedNameOnCollection()
        //Plant_GivesTrueWhenProvidedValidName - alternatywnie
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
            Assert.Contains(POST_NAME, garden.GetPlants());
        }

        [Fact]
        public void GetPlants_CopyOfPlantsCollection()
        {
            // Arrange
            Garden garden = GetGardenWIhtInsignificantSize();

            // Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            // Assert
            //NotSame/Same - sprawdza instancjê
            //NotEqual/Equal - sprawdza zawartoœæ listy
            Assert.NotSame(result1, result2);
        }

        private static Garden GetGardenWithInsignificantSize()
        {
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);
            return garden;
        }
    }
}