using BTW.Application.Services.DocumentHistory;
using BTW.Application.Services.DocumentLogService;
using BTW.Application.Services.DocumentService;
using BTW.Domain.Entities;
using BTW.Domain.Enums;
using BTW.Domain.Repositories;
using BTW.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Moq;

namespace BTW.Tests.Services;

public class DocumentServiceTests
{
    private readonly Mock<IDocumentRepository> _repoMock = new();
    private readonly Mock<IDocumentLogService> _logMock = new();
    private readonly Mock<IDocumentHistoryService> _historyMock = new();

    private readonly DocumentService _service;

    public DocumentServiceTests()
    {
        _service = new DocumentService(
            _repoMock.Object,
            _logMock.Object,
            _historyMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Succeed()
    {
        _repoMock.Setup(x => x.GetByLegalNumberAsync(It.IsAny<string>()))
            .ReturnsAsync(Maybe<Document>.None);

        var doc = Document.Create("FE-1", DocumentType.Invoice,
            "123", "E", "456", "R",
            new List<DocumentItem> { DocumentItem.Create("A", 1, 100).Value },
            null).Value;

        _repoMock.Setup(x => x.AddAsync(It.IsAny<Document>()))
            .ReturnsAsync(Result.Success(doc));

        var result = await _service.CreateAsync(
            "FE-1", DocumentType.Invoice,
            "123", "E", "456", "R",
            new List<DocumentItem> { DocumentItem.Create("A", 1, 100).Value },
            null);

        Assert.True(result.IsSuccess);
    }
}
