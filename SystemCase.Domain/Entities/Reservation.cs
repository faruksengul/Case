using SystemCase.Domain.Core.Base.Abstract;
using SystemCase.Domain.Core.Base.Concrete;

namespace SystemCase.Domain.Entities;

public class Reservation:BaseEntity,IEntity
{
    public Guid CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }

    public virtual IList<Table> Table { get; set; } = new List<Table>();
    public virtual Customer Customer { get; set; }
}