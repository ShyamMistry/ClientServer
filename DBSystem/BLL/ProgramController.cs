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
    public class ProgramController
    {
        public Program FindByPGID(int id) 
        {
            using (var context = new ContextStarTED())
            {
                return context.Programs.Find(id);
            }
        }
        public List<Program> List()
        {
            using (var context = new ContextStarTED())
            {
                return context.Programs.ToList();
            }
        }
        public List<Program> FindBySchoolCode(string partialcode)
        {
            using (var context = new ContextStarTED())
            {
                IEnumerable<Program> results =
                    context.Database.SqlQuery<Program>("Programs_FindBySchool @schoolcode"
                        , new SqlParameter("schoolcode", partialcode));
                return results.ToList();
            }
        }
        public int Add(Program item)
        {
            using (var context = new ContextStarTED())
            {
                context.Programs.Add(item);
                context.SaveChanges();
                return item.ProgramID;
            }
        }
        public int Update(Program item)
        {
            using (var context = new ContextStarTED())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int programid)
        {
            using (var context = new ContextStarTED())
            {
                var existing = context.Programs.Find(programid);
                if (existing == null)
                {
                    throw new Exception("Record has been removed from database");
                }
                context.Programs.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
