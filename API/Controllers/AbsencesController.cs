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
            
            var checkIn = _context.Absences.Where(x => x.UserId == userId && x.isAbsence == true && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();
            if (checkIn == null)
            {
                
                    Absence _absence = new Absence()
                    {
                        UserId = userId,
                        TimeIn = new DateTimeOffset(2020, 9, 19, 8, 0, 0, new TimeSpan(7, 0, 0)),
                        isAbsence = true
                    };

                    _context.Absences.Add(_absence);
                    //_context.Entry(_absence).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Ok("Checked in accomplished");
                
            }
            else 
            {
                var hourNow = DateTime.Now.ToString("HH");
                var hourInt = Convert.ToInt32(hourNow);
                
                if (hourInt > 15)
                {
                    var user = _context.Absences.Where(x => x.UserId == userId && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();

                    if (user == null)
                    {
                        return BadRequest("Anda belum absen");
                    }
                    else
                    {
                        if (!user.TimeOut.Equals(null))
                        {
                            user.TimeOut = DateTimeOffset.Now;
                            user.isAbsence = false;
                            _context.Entry(user).State = EntityState.Modified;
                            //_context.Absences.Update(user);
                            _context.SaveChanges();
                            return Ok("Absen pulang berhasil");
                        }
                        return BadRequest("absen pulang gagal, silahkan coba lagi");
                    }
                }
                else
                {
                    return BadRequest("Anda tidak bisa pulang sebelum pukul 16.00 WIB");
                }
            }
            
        }

        [HttpGet]

        public async Task<IActionResult> GetData()
        {
            var getData = await _context.Absences.ToListAsync();
            return Ok(getData);
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
