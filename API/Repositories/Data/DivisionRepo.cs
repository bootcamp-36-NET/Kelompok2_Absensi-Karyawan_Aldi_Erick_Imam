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

        public override async Task<List<Divisions>> GetAll()
        {
            var getAll = await _context.Divisions.Include("Departments").Where(x => x.isDelete == false).ToListAsync();
            if(!getAll.Equals(null))
            {
                return getAll;
            }
            return null;
        }
    }
}
