using lab05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace lab05_BUS
{
    public class StudentService
    {
        public List<Student> GetALL()
        {
            StudentModelss context = new StudentModelss();
            return context.Students.ToList();
        }

        public List<Student> GetALLHasNoMajor() 
        {
            StudentModelss context = new StudentModelss();
            return context.Students.Where(p => p.MajorID == null).ToList();
        }

        public List <Student> GetALLHasNoMinor(int facultyID) 
        {
            StudentModelss context = new StudentModelss();
            return context.Students.Where(p => p.MajorID == null && p.FacultyID == facultyID).ToList();
        }

        public Student FindById(string studentId) 
        {
            StudentModelss context = new StudentModelss();
            return context.Students.FirstOrDefault(p => p.StudentID == studentId);
        }

        public void InsertUpdate(Student s)
        {
            StudentModelss context = new StudentModelss();
            context.Students.AddOrUpdate(s);
            context.SaveChanges();
        }

        public List<Student> GetAllHasNoMajor()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
