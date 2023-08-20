using System.Net;
using AutoMapper;
using MediatR;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.CommandHandlers.CustomerCommandHandlers;

public class CreateCustomerCommandHandler:IRequestHandler<CreateCustomerCommand,ApiResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerRepository = _unitOfWork.GetRepository<Customer>();

        var customer = await customerRepository
            .SingleOrDefaultAsync(f => f.Email==request.Email);
    
        if (customer == null)
        {
            customer = _mapper.Map<Customer>(request);
            await customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new ApiResponse<CustomerDto>
        {
            Data = _mapper.Map<CustomerDto>(customer),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.Created,
            TotalItemCount = 1
        };
    }
}