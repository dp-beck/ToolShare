namespace ToolShare.UI.Dtos
{
    public class UpdateToolDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BorrowingPeriodInDays { get; set; }
    }
}