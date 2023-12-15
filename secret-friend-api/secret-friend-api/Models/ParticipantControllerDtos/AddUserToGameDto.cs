namespace secret_friend_api.Models.ParticipantControllerDtos
{
    public record AddUserToGameDto(string GameCode, string? Option1, string? Option2, string? Option3);
}
