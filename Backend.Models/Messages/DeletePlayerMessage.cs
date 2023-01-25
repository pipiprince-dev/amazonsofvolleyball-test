namespace Backend.Models.Messages;

public class DeletePlayerMessage : RequestMessage<DeletePlayerMessage>
{
    public int Id { get; set; }
}
