using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using MassTransit;
using MediatR;
using Moq;
using SystemCase.Application.Cqrs.CommandHandlers.ReservationCommandHandlers;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;
using SystemCase.Application.Cqrs.Queries.TableQueries;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Application.Dtos.TableDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.Repository;
using SystemCase.Infrastructure.UnitOfWork;
using Xunit;
using Assert = Xunit.Assert;


namespace SystemCase.Test;

public class CreateReservationCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IGenericRepository<Reservation>> _reservationRepoMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    
    private readonly CreateReservationCommandHandler _handler;

    public CreateReservationCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        _reservationRepoMock = new Mock<IGenericRepository<Reservation>>();
        
        _unitOfWorkMock.Setup(u => u.GetRepository<Reservation>()).Returns(_reservationRepoMock.Object);

        _handler = new CreateReservationCommandHandler(_unitOfWorkMock.Object,_mapperMock.Object, _mediatorMock.Object,_publishEndpointMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateReservation_WhenValidRequest()
    {
       var mediatorMock = new Mock<IMediator>();
    var unitOfWorkMock = new Mock<IUnitOfWork>();
    var reservationRepositoryMock = new Mock<IGenericRepository<Reservation>>();
    var mapperMock = new Mock<IMapper>();

    var testCommand = new CreateReservationCommand
    {
        Name = "John",
        LastName = "Doe",
        Email = "john.doe@example.com",
        NumberOfPerson = 5,
        ReservationDate = DateTime.Today
    };

    var testCustomer = new CustomerDto { Id = Guid.NewGuid() };

    mediatorMock.Setup(m => m.Send(It.Is<CreateCustomerCommand>(c => c.Email == testCommand.Email),It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ApiResponse<CustomerDto>
        {
            IsSuccessful = true,
            Data = testCustomer
        });

    reservationRepositoryMock.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Reservation, bool>>>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(false);

    var testAvailableTables = new List<TableDto> 
    {
        // Sample tables, you should adjust as per your logic
        new TableDto { Id = Guid.NewGuid(), Capacity = 3 },
        new TableDto { Id = Guid.NewGuid(), Capacity = 3 },
    };

    mediatorMock.Setup(m => m.Send(It.IsAny<GetAvailableTableQuery>(),It.IsAny<CancellationToken>()))
        .ReturnsAsync(testAvailableTables);

    mediatorMock.Setup(m => m.Send(It.IsAny<GetSelectTableQuery>(),It.IsAny<CancellationToken>()))
        .ReturnsAsync(testAvailableTables);

    var testReservation = new Reservation { CustomerId = testCustomer.Id, ReservationDate = testCommand.ReservationDate };

    mapperMock.Setup(m => m.Map<Reservation>(It.IsAny<List<TableDto>>()))
        .Returns(testReservation);

    unitOfWorkMock.Setup(u => u.GetRepository<Reservation>()).Returns(reservationRepositoryMock.Object);

    var handler = new CreateReservationCommandHandler( unitOfWorkMock.Object, mapperMock.Object,mediatorMock.Object,_publishEndpointMock.Object);

    // Act
    var result = await handler.Handle(testCommand, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccessful);
    Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    reservationRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Reservation>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
