namespace WebApp.BLL.DTO
{
    public class GameSelectingDTO
    {
        /// <summary>
        /// Games genre filter
        /// </summary>
        /// <example>1</example>
        public string FilterParameter { get; set; }

        /// <summary>
        /// Games age filter
        /// </summary>
        /// <example>1</example>
        public string FilterParameterValue { get; set; }

        /// <summary>
        /// Game parameter for sorting
        /// </summary>
        /// <example>1</example>
        public string SortField { get; set; }

        /// <summary>
        /// Game unique identifier
        /// </summary>
        /// <example>1</example>
        public string OrderType { get; set; } = DAL.Enums.OrderType.Asc.ToString();
    }
}
