namespace ConsoleApp.Test.xUnit
{
    public class GardenTest
    {
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

        [Fact]
        public void Plant_NullName_ArgumentNullException()
        {
            // Arrange
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);
            string? NULL_NAME = null;

            // Act
            Action action = () => garden.Plant(NULL_NAME);

            // Assert
            // mo¿emy mieæ wiele assert, jeœli doczytcz¹ tej samej testowanej rzeczy
            var argumentNullException = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", argumentNullException.ParamName);
        }

        [Fact]
        public void Plant_WhiteSpaceName_ArgumentException()
        {
            // Arrange
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);
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
            const int INSIGNIFICANT_SIZE = 0;
            var garden = new Garden(INSIGNIFICANT_SIZE);

            // Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            // Assert
            //NotSame/Same - sprawdza instancjê
            //NotEqual/Equal - sprawdza zawartoœæ listy
            Assert.NotSame(result1, result2);
        }
    }
}