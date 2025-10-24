using Xunit;
using CMCS.Models;

namespace CMCS.Tests
{
    public class ClaimsTest
    {
        [Fact]
        public void CalculateTotalAmount_ShouldBeCorrect()
        {
            // Arrange
            var claim = new Claim
            {
                HoursWorked = 10,
                HourlyRate = 200
            };

            // Act
            var total = claim.TotalAmount;

            // Assert
            Assert.Equal(2000, total);
        }

        [Fact]
        public void Claim_Status_ShouldBePendingByDefault()
        {
            // Arrange
            var claim = new Claim();

            // Assert
            Assert.Equal("Pending", claim.Status);
        }
    }
}
