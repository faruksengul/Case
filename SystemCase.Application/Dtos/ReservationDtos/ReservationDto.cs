using SystemCase.Application.Dtos.TableDtos;

namespace SystemCase.Application.Dtos.ReservationDtos;

public class ReservationDto
{
    public Guid CustomerId { get; set; }
    public List<TableDto> Tables { get; set; }
    public DateTime ReservationDate { get; set; }
}