using SystemCase.Domain.Core.Base.Abstract;
using SystemCase.Domain.Core.Base.Concrete;
using SystemCase.Domain.Enums;

namespace SystemCase.Domain.Entities;

public class Table:BaseEntity,IEntity
{
    public int Number { get; set; }
    public int Capacity { get; set; }
    public TableTypeEnum TableType { get; set; }
    public virtual IList<Reservation> Reservations { get; set; } = new List<Reservation>();
}