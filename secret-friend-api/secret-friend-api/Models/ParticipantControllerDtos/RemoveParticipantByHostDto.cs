namespace secret_friend_api.Models.ParticipantControllerDtos
{
    public class RemoveParticipantByHostDto
    {
        public int GameId { get; set; }
        public int UserIdToRemove { get; set; }
    }
}
