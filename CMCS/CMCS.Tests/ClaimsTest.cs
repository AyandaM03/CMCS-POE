using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CMCS.Controllers;

namespace CMCS.Tests
{
    public class ClaimsTest
    {
        // -------------------------------------------
        // Create an In-Memory EF Core Database Context
        // -------------------------------------------
        private static CMCSContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CMCSContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CMCSContext(options);
        }

        // -------------------------------------------
        // Create a mock IWebHostEnvironment
        // -------------------------------------------
        private static IWebHostEnvironment GetEnvironment()
        {
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());
            return mockEnv.Object;
        }

        // -------------------------------------------
        // Test 1: Claim is successfully saved
        // -------------------------------------------
        [Fact]
        public async Task Submit_AddsClaim_WithPendingStatus()
        {
            // Arrange
            var context = GetDbContext();
            var environment = GetEnvironment();
            var controller = new ClaimsController(context, environment);

            var claim = new Claim
            {
      
                LecturerName = "John Doe",
                HoursWorked = 5,
                HourlyRate = 200,
                Notes = "Test case"
            };
            var result = await controller.Submit(claim, null);
            // Assert
            var savedClaim = context.Claims.FirstOrDefault();
            Assert.NotNull(savedClaim);
            Assert.Equal("Pending", savedClaim.Status);
        }

        // -------------------------------------------
        // Test 2: Status is updated successfully
        // -------------------------------------------
        [Fact]
        public async Task UpdateStatus_Changes_Claim_Status()
        {
            // Arrange
            var context = GetDbContext();
            var environment = GetEnvironment();

            var claim = new Claim
            {
                ClaimId = 1,
                LecturerName = "Jane Doe",
                HoursWorked = 10,
                HourlyRate = 150,
                Status = "Pending"
            };
       context.Claims.Add(claim);
            await context.SaveChangesAsync();

            var controller = new ClaimsController(context, environment);

            // Act
            var result = await controller.UpdateStatus(1, "Approved");

            // Assert
            var updated = context.Claims.First(c => c.ClaimId == 1);
            Assert.Equal("Approved", updated.Status);
        }
    }
}
