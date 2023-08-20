using SystemCase.Domain.Core.Base.Abstract;
using SystemCase.Domain.Core.Base.Concrete;

namespace SystemCase.Domain.Entities;

public class Customer:BaseEntity,IEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public virtual IList<Reservation> Reservations { get; set; }
}