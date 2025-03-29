using Xunit;

using LibBMS.Common;

namespace LibBMS.CommonUtilTests
{
    public class UtilityClassTests
    {
        private readonly Utilities _utilities;

        public UtilityClassTests()
        {
            _utilities = new Utilities();
        }

        [Fact]
        public void Year_ShouldValidatePublishedYear()
        {
            string publishedYear = "1234";
            bool allowedYear = Utilities.IsValidYear(publishedYear);
            Assert.True(allowedYear);
        }

        [Fact]
        public void FutureYear_ShouldIsNotAllowed()
        {
            int currentYear = DateTime.Now.Year;
            string publishedYear = (currentYear + 1).ToString();
            bool isValidYear = Utilities.IsValidYear(publishedYear);
            Assert.False(isValidYear);
        }

        [Fact]
        public void ISBN_ShouldAllowValid13ISBN()
        {
            string validISBN = "9 781760 279486";
            bool allowed = Utilities.IsValidISBN(validISBN);
            Assert.True(allowed); 
        }

        [Fact]
        public void ISBN_ShouldAllowValid10ISBN()
        {
            string validISBN = "0198526636";
            bool allowed = Utilities.IsValidISBN(validISBN);
            Assert.True(allowed); 
        }

        [Fact]
        public void ISBN_ShouldNotAllowInValidISBN()
        {
            string validISBN = "0-321-16506-XX"; // invalid because it is 12 digits
            bool allowed = Utilities.IsValidISBN(validISBN);
            Assert.False(allowed); 
        }

        [Fact]
        public void ISBN_ShouldNotEmptyOrWhitespaceISBN()
        {
            bool allowed = Utilities.IsValidISBN("            ");
            Assert.False(allowed); 
        }

    }
}