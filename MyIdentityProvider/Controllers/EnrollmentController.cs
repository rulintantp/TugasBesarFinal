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
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollment _enrollmentDal;
        private readonly IMapper _mapper;
        public EnrollmentController(IEnrollment enrollmentDAL, IMapper mapper)
        {
            _enrollmentDal = enrollmentDAL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EnrollmentCreateDTO enrollmentCreateDto)
        {
            try
            {
                var newEnrollment = _mapper.Map<Enrollment>(enrollmentCreateDto);
                var result = await _enrollmentDal.Insert(newEnrollment);
                var enrollmentReadDTO = _mapper.Map<EnrollmentReadDTO>(result);

                return CreatedAtAction("GetById", new { id = enrollmentReadDTO.EnrollmentID }, enrollmentReadDTO);
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
                await _enrollmentDal.Delete(id);
                return Ok($"Data course dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber)
        {
            var results = await _enrollmentDal.GetAll();
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

            var pagging = enrollments.Skip((pageNumber - 1) * 10).Take(10).ToList();
            if(pagging.Count > 0)
            {
                pagging[0].TotalPage = Math.Ceiling((decimal)results.Count() / (decimal)5);
            }

            return Ok(pagging);
        }


        [HttpGet("{id}")]
        public async Task<EnrollmentReadDTO> GetById(int id)
        {
            var result = await _enrollmentDal.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var enrollmentReadDTO = _mapper.Map<EnrollmentReadDTO>(result);

            return enrollmentReadDTO;
        }





    }
}
