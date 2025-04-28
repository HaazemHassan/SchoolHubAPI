namespace School.Core.DTO
{
    public class StudentBasicDTO

    {

        public int Id { get; set; }
        public string Name { get; set; }


        public StudentBasicDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
