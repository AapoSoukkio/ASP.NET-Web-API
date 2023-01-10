namespace UnivEnrollerApi.Data;
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int UniversityId { get; set; }        
    public University University { get; set; }

    public List<Enrollment> Enrollments { get; set; }

}