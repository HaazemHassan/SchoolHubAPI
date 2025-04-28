namespace School.Core.Features.Students.Queries.Results
{
    public class GetStudentsPaginatedResponse
    {
        public GetStudentsPaginatedResponse(int studID, string name, string? address, string? departmentName)
        {
            StudID = studID;
            Name = name;
            Address = address;
            DepartmentName = departmentName;
        }

        public int StudID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
    }
}
