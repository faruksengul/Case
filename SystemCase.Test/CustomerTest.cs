using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Moq;
using SystemCase.Application.Cqrs.CommandHandlers.CustomerCommandHandlers;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.Repository;
using SystemCase.Infrastructure.UnitOfWork;
using Xunit;
using Assert = Xunit.Assert;

namespace SystemCase.Test;

public class CustomerTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Customer>> _customerRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateCustomerCommandHandler _handler;

    public CustomerTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _customerRepositoryMock = new Mock<IGenericRepository<Customer>>();
        _mapperMock = new Mock<IMapper>();
        
        _unitOfWorkMock.Setup(u => u.GetRepository<Customer>()).Returns(_customerRepositoryMock.Object);

        _handler = new CreateCustomerCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCustomer_WhenCustomerDoesNotExist()
    {
        var request = new CreateCustomerCommand { Email = "test@test.com", Name = "Test", LastName = "User" };

        _customerRepositoryMock.Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
            .ReturnsAsync((Customer)null);

        var mappedCustomer = new Customer { Email = request.Email, Name = request.Name, LastName = request.LastName };
        _mapperMock.Setup(m => m.Map<Customer>(request)).Returns(mappedCustomer);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.True(result.IsSuccessful);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        _customerRepositoryMock.Verify(r => r.AddAsync(mappedCustomer, CancellationToken.None), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
