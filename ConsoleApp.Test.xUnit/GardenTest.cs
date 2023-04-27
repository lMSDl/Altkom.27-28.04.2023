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
            var result2 = garden.Plant(VALID_NAME);

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
    }
}