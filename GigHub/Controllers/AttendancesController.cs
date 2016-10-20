using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using GigHub.Dtos;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var exixts = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (exixts)
             return BadRequest("The attendance already exist");
            

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId

            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
            
        }
    }
}
