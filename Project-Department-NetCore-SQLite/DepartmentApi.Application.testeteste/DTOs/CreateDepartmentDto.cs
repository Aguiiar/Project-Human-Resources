namespace DepartmentApi.Application.DTOs
{
    public class CreateDepartmentDto
    {
        public Guid Id { get; private set; }
        public String Name { get; set; }
        public int AmountEmployee { get; set; }


    }
}
