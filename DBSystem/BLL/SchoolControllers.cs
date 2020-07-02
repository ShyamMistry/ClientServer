using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class SchoolControllers
    {
        public List<School> List() {
            using (var context = new ContextStarTED()) 
            {
                return context.Schools.ToList();
            }         
        }
        public string Add(School item)
        {
            using (var context = new ContextStarTED())
            {
                context.Schools.Add(item);
                context.SaveChanges();
                return item.SchoolCode;
            }
        }
        public int Update(School item)
        {
            using (var context = new ContextStarTED())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(string schoolCode)
        {
            using (var context = new ContextStarTED())
            {
                var existing = context.Schools.Find(schoolCode);
                if (existing == null)
                {
                    throw new Exception("Record has been removed from database");
                }
                context.Schools.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
