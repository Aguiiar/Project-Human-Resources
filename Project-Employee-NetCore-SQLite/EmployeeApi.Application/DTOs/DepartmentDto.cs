namespace DTOs
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int AmountEmployee { get; set; }

        public DateTime HoursOperation { get; set; }
    }
}
