using BTW.Application.Context;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.ValueObjects;
using BTW.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BTW.Tests.Repositories;

public class DocumentRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly DocumentRepository _repository;

    public DocumentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("RepoDb")
            .Options;

        _context = new AppDbContext(options);
        _repository = new DocumentRepository(_context);
    }

    [Fact]
    public async Task AddAsync_Should_Return_Success()
    {
        var doc = Document.Create("FE-1", DocumentType.Invoice,
            "123", "E", "456", "R",
            new List<DocumentItem> { DocumentItem.Create("A", 1, 100).Value },
            null).Value;

        var result = await _repository.AddAsync(doc);

        Assert.True(result.IsSuccess);
    }
}
