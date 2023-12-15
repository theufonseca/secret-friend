namespace secret_friend_api.Models.GameControllerDtos
{
    public class UpdatePriceDto
    {
        public int GameId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
