namespace UnivEnrollerApi.Data;
public class University
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Course> Cources { get; set; }
}