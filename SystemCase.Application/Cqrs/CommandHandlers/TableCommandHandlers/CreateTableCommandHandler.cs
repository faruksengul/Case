using System.Net;
using AutoMapper;
using MediatR;
using SystemCase.Application.Cqrs.Commands.TableCommands;
using SystemCase.Application.Dtos.TableDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.CommandHandlers.TableCommandHandlers;

public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, ApiResponse<TableDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTableCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<TableDto>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var tableEntity = _mapper.Map<Table>(request);
        await _unitOfWork.GetRepository<Table>().AddAsync(tableEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<TableDto>
        {
            Data = _mapper.Map<TableDto>(tableEntity),
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.Created,
            TotalItemCount = 1
        };
    }
}