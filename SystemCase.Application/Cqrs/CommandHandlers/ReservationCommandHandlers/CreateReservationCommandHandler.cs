using System.Net;
using AutoMapper;
using MassTransit;
using MediatR;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;
using SystemCase.Application.Cqrs.Queries.TableQueries;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Application.Dtos.ReservationDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;
using SystemCase.Shared.Messages;

namespace SystemCase.Application.Cqrs.CommandHandlers.ReservationCommandHandlers;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ApiResponse<ReservationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }
 public async Task<ApiResponse<ReservationDto>> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var (customer, errorResponse) = await CreateOrRetrieveCustomer(request);
        if (errorResponse != null)
            return errorResponse;

        bool anyReservationToday = await _unitOfWork.GetRepository<Reservation>().AnyAsync(
            f => f.CustomerId.Equals(customer.Id) && f.ReservationDate.Day == request.ReservationDate.Day,
            cancellationToken);

        if (anyReservationToday)
            return GenerateErrorResponse(new List<string> { "All ready reservation today" });

        var avaibleTableResult = await _mediator.Send(new GetAvailableTableQuery(request.ReservationDate));
        var selectedTableResult =
            await _mediator.Send(new GetSelectTableQuery(avaibleTableResult, request.NumberOfPerson));

        if (selectedTableResult.Sum(f => f.Capacity) < request.NumberOfPerson)
            return GenerateErrorResponse(new List<string> { "Capacity not found" });

        var reservationEntity = new Reservation
        {
            CustomerId = customer.Id,
            ReservationDate = request.ReservationDate
        };

        await _unitOfWork.GetRepository<Reservation>().AddAsync(reservationEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var message = new ReservationCreatedMessage
        {
            Name = customer.Name,  
            Surname = customer.LastName,
            Email = customer.Email,
            ReservedTableNumbers = selectedTableResult.Select(t => t.Number).ToList()
        };
        await _publishEndpoint.Publish(message, cancellationToken);
        
        return new ApiResponse<ReservationDto>
        {
            Data = _mapper.Map<ReservationDto>(reservationEntity),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.Created,
            TotalItemCount = 1
        };
    }
   

    private async Task<(CustomerDto Customer, ApiResponse<ReservationDto> ErrorResponse)> CreateOrRetrieveCustomer(
        CreateReservationCommand request)
    {
        var createCustomerCommand = new CreateCustomerCommand
        {
            LastName = request.Name,
            Name = request.Name,
            Email = request.Email
        };
        var customerResponse = await _mediator.Send(createCustomerCommand);

        if (!customerResponse.IsSuccessful)
        {
            return (null,
                GenerateErrorResponse(new List<string> { "Some error related to customer creation/retrieval." }));
        }
        return (customerResponse.Data, null);
    }

    private ApiResponse<ReservationDto> GenerateErrorResponse(List<string> errorMessages)
    {
        return new ApiResponse<ReservationDto>
        {
            IsSuccessful = false,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Errors = errorMessages
        };
    }
}