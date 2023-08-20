using System.Net;
using AutoMapper;
using Moq;
using SystemCase.Application.Cqrs.CommandHandlers.TableCommandHandlers;
using SystemCase.Application.Cqrs.Commands.TableCommands;
using SystemCase.Domain.Entities;
using SystemCase.Domain.Enums;
using SystemCase.Infrastructure.Repository;
using SystemCase.Infrastructure.UnitOfWork;
using Xunit;
using Assert = Xunit.Assert;

namespace SystemCase.Test;

public class TableTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Table>> _tableRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateTableCommandHandler _handler;

    public TableTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _tableRepositoryMock = new Mock<IGenericRepository<Table>>();
        _mapperMock = new Mock<IMapper>();
        
        _unitOfWorkMock.Setup(u => u.GetRepository<Table>()).Returns(_tableRepositoryMock.Object);

        _handler = new CreateTableCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateTable()
    {
        var request = new CreateTableCommand
        {
            Capacity = 5,
            TableType = TableTypeEnum.Min 
        };

        var mappedTable = new Table
        {
            Capacity = request.Capacity,
            TableType = request.TableType
        };

        _mapperMock.Setup(m => m.Map<Table>(request)).Returns(mappedTable);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.True(result.IsSuccessful);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        _tableRepositoryMock.Verify(r => r.AddAsync(mappedTable, CancellationToken.None), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
