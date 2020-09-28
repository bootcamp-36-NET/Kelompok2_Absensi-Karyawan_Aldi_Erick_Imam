using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class DivisionRepo : GeneralRepo<Divisions, MyContext>
    {
        private MyContext _context;

        public DivisionRepo(MyContext context) : base(context)
        {
            this._context = context;
        }

        public override async Task<int> Create(Divisions division)
        {
            division.createdDate = DateTimeOffset.Now;
            division.Department = await _context.Departments.FindAsync(division.DepartmentId);
            await _context.Divisions.AddAsync(division);
            var create = await _context.SaveChangesAsync();
            return create;
        }


        public override async Task<List<Divisions>> GetAll()
        {
            var getAll = await _context.Divisions.Include(x => x.Department).Where(x => x.isDelete == false).ToListAsync();
            if(!getAll.Equals(null))
            {
                return getAll;
            }
            return null;
        }
    }
}
