namespace secret_friend_api.Models.GameControllerDtos
{
    public record NewGameDto(string Name, int? MinPrice, int? MaxPrice);
}
