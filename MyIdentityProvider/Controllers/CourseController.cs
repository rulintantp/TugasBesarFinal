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
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseDAL;
        private readonly IMapper _mapper;
        public CourseController(ICourse courseDAL, IMapper mapper)
        {
            _courseDAL = courseDAL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CourseCreateDTO courseCreateDto)
        {
            try
            {
                var newCourse = _mapper.Map<Course>(courseCreateDto); 
                var result = await _courseDAL.Insert(newCourse);
                var courseReadDTO = _mapper.Map<CourseReadDTO>(result);

                return CreatedAtAction("GetById", new { id = result.CourseID }, courseReadDTO);
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
                await _courseDAL.Delete(id);
                return Ok($"Data course dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IEnumerable<CourseReadDTO>> GetAll(int pageNumber)
        {
            var results = await _courseDAL.GetAll();
            var courseDTO = _mapper.Map<IEnumerable<CourseReadDTO>>(results); 

            var pagging = courseDTO.Skip((pageNumber - 1) * 5).Take(5).ToList(); 
            if (pagging.Count > 0) //Jika pagging lebih dari 1  
            {
                pagging[0].TotalPage = Math.Ceiling((decimal)results.Count() / (decimal)5); //Memasukan data totalpage pada data pertama
            }

            return pagging;
        }


        [HttpGet("{id}")]
        public async Task<CourseReadDTO> GetById(int id)
        {
            var result = await _courseDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var studentDTO = _mapper.Map<CourseReadDTO>(result);

            return studentDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<CourseReadDTO>> GetByName(string name)
        {
            List<CourseReadDTO> courseDtos = new List<CourseReadDTO>();
            var results = await _courseDAL.GetByName(name);
            foreach (var result in results)
            {
                courseDtos.Add(new CourseReadDTO
                {
                    CourseID = result.CourseID,
                    Title = result.Title,
                    Credits = result.Credits
                });
            }
            return courseDtos;
        }

        [HttpPut]
        public async Task<ActionResult> Put(CourseReadDTO courseDto)
        {
            try
            {
                var updateCourse = new Course
                {
                    CourseID = courseDto.CourseID,
                    Title = courseDto.Title,
                    Credits = courseDto.Credits 
                };
                var result = await _courseDAL.Update(updateCourse);
                return Ok(courseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEnrollmentByCourseId")]
        public async Task<ActionResult> GetEnrollmentByCourseId(int id)
        {
            var results = await _courseDAL.GetEnrollmentByCourseId(id);
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

