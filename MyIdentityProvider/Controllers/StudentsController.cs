using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCourse.DATA.DAL;
using StudentCourse.Domain;
using StudentCourseWebAPI.DTO;

namespace MyIdentityProvider.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _studentDAL;
        private readonly IMapper _mapper;
        public StudentsController(IStudent studentDAL, IMapper mapper)
        {
            _studentDAL = studentDAL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentCreateDTO studentCreateDto)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(studentCreateDto);
                newStudent .EnrollmentDate = DateTime.Now;
                var result = await _studentDAL.Insert(newStudent);
                var studentReadDTO = _mapper.Map<StudentReadDTO>(result);

                return CreatedAtAction("GetById", new { id = result.ID }, studentReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _studentDAL.Delete(id);
                return Ok($"Data student dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IEnumerable<StudentWithCourseAndEnrollmentReadDTO>> GetAll(int pageNumber)
        {
            var results = await _studentDAL.GetAll();
            var studentDTO = _mapper.Map<IEnumerable<StudentWithCourseAndEnrollmentReadDTO>>(results);

            var pagging = studentDTO.Skip((pageNumber - 1) * 5).Take(5).ToList();
            if (pagging.Count > 0)
            {
                pagging[0].TotalPage = Math.Ceiling((decimal)results.Count() / (decimal)5);
            }
            

            return pagging;
        }
    

        [HttpGet("{id}")]
        public async Task<StudentWithCourseAndEnrollmentReadDTO> GetById(int id)
        {
            var result = await _studentDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var studentDTO = _mapper.Map<StudentWithCourseAndEnrollmentReadDTO>(result);

            return studentDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<StudentReadDTO>> GetByName(string name)
        {
            List<StudentReadDTO> studentDtos = new List<StudentReadDTO>();
            var results = await _studentDAL.GetByName(name);
            foreach (var result in results)
            {
                studentDtos.Add(new StudentReadDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName
                });
            }
            return studentDtos;
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentReadDTO studentDto)
        {
            try
            {
                var updateStudent = new Student
                {
                    ID = studentDto.ID,
                    FirstMidName = studentDto.FirstMidName,
                    LastName = studentDto.LastName
                };
                var result = await _studentDAL.Update(updateStudent);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEnrollmentByID")]
        public async Task<ActionResult> GetEnrollmentByID(int id)
        {
            var results = await _studentDAL.GetEnrollmentByID(id);
            List<EnrollmentReadDTO> enrollments = new List<EnrollmentReadDTO>();
            foreach (var i in results)
            {
                EnrollmentReadDTO enrollment = new EnrollmentReadDTO();
                enrollment.EnrollmentID = i.EnrollmentID;
                enrollment.Grade = i.Grade;
                enrollment.CourseID = i.CourseID;
                enrollment.StudentID = i.StudentID;
                enrollment.CourseTitle = i.Course.Title;
                enrollment.StudentName = $"{i.Student.FirstMidName} {i.Student.LastName}";
                enrollments.Add(enrollment);

            }

            return Ok(enrollments);
        }
    }
    
}
