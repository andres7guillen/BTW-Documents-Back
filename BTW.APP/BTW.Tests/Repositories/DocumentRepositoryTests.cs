using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.ValueObjects;
using BTW.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BTW.Tests.Repositories;

public class DocumentRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly AppDbContext _context;
    private readonly DocumentRepository _repository;

    public DocumentRepositoryTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new AppDbContext(options);

        _context.Database.EnsureCreated();

        _repository = new DocumentRepository(_context);
    }

    [Fact]
    public async Task AddAsync_Should_Return_Success()
    {
        // Arrange
        var createResult = Document.Create(
            "FE-1", DocumentType.Invoice,
            "123", "E", "456", "R",
            new List<DocumentItem>
            {
                DocumentItem.Create("A", 1, 100).Value
            },
            null
        );


        // Act
        var result = await _repository.AddAsync(createResult.Value);

        // Assert
        Assert.True(result.IsSuccess);
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Close();
    }
}
