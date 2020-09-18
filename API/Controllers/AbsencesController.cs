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
        public IActionResult Create(AbsenceVm absenceVm)
        {
            //var checkIn = _context.Absences.Where(x => x.UserId == absenceVm.UserId && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();
            var checkIn = _context.Absences.Where(x => x.UserId == absenceVm.UserId && x.isAbsence == true).FirstOrDefault();
            if (checkIn==null)
            {
                    if (absenceVm.Type == "masuk")
                    {
                        Absence _absence = new Absence()
                        {
                        UserId = absenceVm.UserId,
                        TimeIn = DateTimeOffset.Now,
                        isAbsence = true
                        };

                        _context.Absences.Add(_absence);
                        //_context.Entry(_absence).State = EntityState.Modified;
                        _context.SaveChanges();

                        return Ok("Absen masuk berhasil");
                    }
                    else if (absenceVm.Type == "pulang")
                    {
                        var user = _context.Absences.Where(x => x.UserId == absenceVm.UserId && x.TimeIn.Day == DateTimeOffset.Now.Day).FirstOrDefault();

                        if (user == null)
                        {
                        return BadRequest("User belum absen");
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
                    return BadRequest("Anda tidak absen masuk 2 kali");
                    }
                
            }
            else
            {
               return BadRequest("Anda sudah absen masuk");
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
}
