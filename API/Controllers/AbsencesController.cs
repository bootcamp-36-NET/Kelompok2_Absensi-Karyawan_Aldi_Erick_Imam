using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        public AbsencesController(MyContext myContext)
        {
            _context = myContext;
        }
        private readonly MyContext _context;

        [HttpPost]
        public IActionResult Create(Dto dto)
        {
            string userId;
            try
            {
                userId = _context.Users.Where(x => x.UserName == dto.UserName).FirstOrDefault().Id;
            }
            catch (Exception)
            {
                return BadRequest("User not found");
            }
            
            var checkIn = _context.Absences.Where(x => x.UserId == userId && x.TimeIn.Year >= 1000 && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();
            if (checkIn == null)
            {
                if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 10)
                {
                    Absence _absence = new Absence()
                    {
                        UserId = userId,
                        TimeIn = DateTimeOffset.Now,
                        isAbsence = true
                    };

                    _context.Absences.Add(_absence);
                    //_context.Entry(_absence).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Ok("Checked in accomplished");
                }
                else
                {
                    return BadRequest("You can only check in between 06:00 and 09:00");
                }   
                
            }
            else 
            {
                if (DateTime.Now.Hour >= 16)
                {
                    var user = _context.Absences.Where(x => x.UserId == userId && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();

                    
                    if (user.TimeOut.Year <= 1000)
                    {
                        user.TimeOut = DateTimeOffset.Now;
                        user.isAbsence = false;
                        _context.Entry(user).State = EntityState.Modified;
                        //_context.Absences.Update(user);
                        _context.SaveChanges();
                        return Ok("Check out Accomplished");
                    }
                    return BadRequest("You can only check two times a day");
                    
                }
                else
                {
                    return BadRequest("You cannot check out before 16.00 GMT+7");
                }
            }
            
        }

        [HttpGet]
        public async Task<List<Absence>> GetData()
        {
            var getData = await _context.Absences.Include(a => a.User)
                .ThenInclude(u => u.Employee)
                .ThenInclude(e => e.Divisions)
                .ToListAsync();
            return getData;
        }

        [HttpGet("{Id}")]
        public IActionResult GetByUsers(string Id)
        {
            var getUsers = _context.Absences.Where(x => x.UserId == Id).ToList();

            if (getUsers == null)
            {
                return NotFound("Users belum pernah absen");
            }

            return Ok(getUsers);
        }

    }

    public class Dto
    {
        public string UserName { get; set; }
    }
}
