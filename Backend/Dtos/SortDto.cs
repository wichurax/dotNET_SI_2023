using Backend.Enums;

namespace Backend.Dtos
{
    public class SortDto
    {
        /// <summary>
        /// Name of column we want to sort data
        /// </summary>
        public string? ColumnName { get; set; }

        public SortDirection Direction { get; set; } = SortDirection.Ascending;
    }
}
