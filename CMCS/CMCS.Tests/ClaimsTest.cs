using Xunit;
using CMCS.Data;
using CMCS.Models;
using CMCS.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Tests
{
    public class ClaimsTest
    {
        private CMCSContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CMCSContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new CMCSContext(options);
        }

        [Fact]
        public async Task Coordinator_Can_PreApprove_Claim()
        {
            // Arrange
            var context = GetDbContext();
            var claim = new Claim
            {
                ClaimId = 1,
                LecturerName = "John Doe",
                HoursWorked = 10,
                HourlyRate = 200,
               
                Status = "Pending"
            };
            context.Claims.Add(claim);
            await context.SaveChangesAsync();

            var controller = new CoordinatorController(context);

            // Act
            await controller.ChangeStatus(1, "PreApprove");
            var updatedClaim = context.Claims.FirstOrDefault(c => c.ClaimId == 1);

            // Assert
            Assert.Equal("PreApproved", updatedClaim.Status);
        }

        [Fact]
        public async Task Manager_Can_Approve_Claim()
        {
            // Arrange
            var context = GetDbContext();
            var claim = new Claim
            {
                ClaimId = 2,
                LecturerName = "Jane Smith",
                HoursWorked = 8,
                HourlyRate = 180,
               
                Status = "PreApproved"
            };
            context.Claims.Add(claim);
            await context.SaveChangesAsync();

            var controller = new ManagerController(context);

            // Act
            await controller.ChangeStatus(2, "Approve");
            var updatedClaim = context.Claims.FirstOrDefault(c => c.ClaimId == 2);

            // Assert
            Assert.Equal("Approved", updatedClaim.Status);
        }
    }
}
