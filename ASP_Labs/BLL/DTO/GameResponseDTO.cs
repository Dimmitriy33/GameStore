namespace WebApp.BLL.DTO
{
    public class GameResponseDTO : BaseGameModel
    {
        /// <summary>
        /// Game logotype image
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Game background image
        /// </summary>
        public string Background { get; set; }
    }
}
