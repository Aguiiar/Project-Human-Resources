namespace DTOs
{


    public class EmployeeDepartmentDtoResponse
    {

        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }

        public String Position { get; set; }
        public Decimal Salary { get; set; }
        public DateOnly AdmissionDate { get; set; }

        public Boolean Situation { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
