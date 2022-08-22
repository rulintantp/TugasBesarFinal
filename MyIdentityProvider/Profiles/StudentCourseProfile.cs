using StudentCourse.Domain;
using StudentCourseWebAPI.DTO;
using AutoMapper;

namespace StudentCourseWebAPI.Profile
{
    public class StudentCourseProfile : AutoMapper.Profile
    {
        public StudentCourseProfile()
        {
            CreateMap<Student, StudentReadDTO>();
            CreateMap<StudentReadDTO, Student>();
            CreateMap<StudentCreateDTO, Student>();

            CreateMap<Student, StudentWithCourseAndEnrollmentReadDTO>();
            CreateMap<StudentWithCourseAndEnrollmentReadDTO, Student>();

            CreateMap<Course, CourseReadDTO>();
            CreateMap<CourseReadDTO, Course>();
            CreateMap<CourseCreateDTO, Course>();

            CreateMap<EnrollmentCreateDTO, Enrollment>();
            CreateMap<EnrollmentReadDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentReadDTO>();

        }
    }
}
