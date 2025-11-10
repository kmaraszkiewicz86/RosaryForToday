using Microsoft.EntityFrameworkCore;
using NSubstitute;
using RosaryForToday.Application.CommandHandlers;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Models.Commands;
using Xunit;

namespace RosaryForToday.Application.Unit.Tests.CommandHandlers;

public class CreateRosaryTypeCommandHandlerTests : BaseFixture
{
    [Fact]
    public async Task Handle_Creates_RosaryType_In_Db()
    {
        // Arrange: in-memory DB
        var options = new DbContextOptionsBuilder<RosaryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var db = new RosaryDbContext(options);
        // seed language so foreign key is valid
        db.Languages.Add(new Domain.Entities.Language { Id = 1, Code = "en", Name = "English" });
        await db.SaveChangesAsync();

        var handler = new CreateRosaryTypeCommandHandler(db);

        var command = new CreateRosaryTypeCommand
        {
            Name = "Test Rosary",
            Description = "Desc",
            LanguageId = 1
        };

        // Act
        var id = await handler.Handle(command);

        // Assert
        var created = await db.RosaryTypes.FindAsync(id);
        Assert.NotNull(created);
        Assert.Equal("Test Rosary", created!.Name);
        Assert.Equal(1, created.LanguageId);
    }
}