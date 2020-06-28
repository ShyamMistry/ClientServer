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
    public class PlayerController
    {
        public Player FindByPlayerID(int id)
        {
            using (var context = new ContextFSIS())
            {
                return context.Players.Find(id);
            }
        }
        public List<Player> List()
        {
            using (var context = new ContextFSIS())
            {
                return context.Players.ToList();
            }
        }
        public List<Player> FindByID(int id)
        {
            using (var context = new ContextFSIS())
            {
                IEnumerable<Player> results =
                    context.Database.SqlQuery<Player>("Player_GetByTeam @ID"
                        , new SqlParameter("ID", id));
                return results.ToList();
            }
        }
        
        public int Add(Player item)
        {
            using (var context = new ContextFSIS())
            {
                context.Players.Add(item);
                context.SaveChanges();
                return item.PlayerID;

            }
        }
        public int Update(Player item)
        {
            using (var context = new ContextFSIS())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int playerid)
        {
            using (var context = new ContextFSIS())
            {
                var existing = context.Players.Find(playerid);
                if (existing == null)
                {
                    throw new Exception("Record has been removed from database");
                }
                context.Players.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
