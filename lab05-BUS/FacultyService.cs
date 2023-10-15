using lab05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab05_BUS
{
    public class FacultyService
    {
        public List<Faculty> GetALL()
        {
            StudentModelss context = new StudentModelss();
            return context.Faculties.ToList();
        }
    }
}
