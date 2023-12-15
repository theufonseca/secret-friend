namespace secret_friend_api.Models.ParticipantControllerDtos
{
    public record AddUserToGameDto(int GameId, int UserId, string? Option1, string? Option2, string? Option3);
}
