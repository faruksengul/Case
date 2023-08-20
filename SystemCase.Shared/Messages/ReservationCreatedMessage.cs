namespace SystemCase.Shared.Messages;

public class ReservationCreatedMessage
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public List<int> ReservedTableNumbers { get; set; }
}