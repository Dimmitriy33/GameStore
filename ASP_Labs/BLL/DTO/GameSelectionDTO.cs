namespace WebApp.BLL.DTO
{
    public class GameSelectionDTO
    {
        /// <summary>
        /// Games filter
        /// </summary>
        /// <example>Genre</example>
        public string FilterType { get; set; }

        /// <summary>
        /// Games filter value
        /// </summary>
        /// <example>Action</example>
        public string FilterValue { get; set; }

        /// <summary>
        /// Game parameter for sorting
        /// </summary>
        /// <example>Price</example>
        public string SortField { get; set; }

        /// <summary>
        /// Games sorting order type
        /// </summary>
        /// <example>Asc</example>
        public string OrderType { get; set; } = DAL.Enums.OrderType.Asc.ToString();
    }
}
