using lab05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab05_BUS
{
    internal class MajorService
    {
        public List<Major> GetALLByFaculty(int facultyID)
        {
            StudentModelss context = new StudentModelss();
            return context.Majors.Where(propa => propa.FacultyID == facultyID).ToList();
        }
    }
}
