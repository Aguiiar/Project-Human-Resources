namespace DTOs
{
    public class CreateEmployeeDto
    {
        public String Name { get; set; }
        public String Email { get; set; }

        public String Position { get; set; }
        public Decimal Salary { get; set; }

        public DateOnly AdmissionDate { get; set; }

        public Boolean Situation { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
