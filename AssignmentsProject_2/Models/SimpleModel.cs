using AssignmentsProject_2.Models;
using MongoDB.Driver;
using Utilities;

namespace AssignmentsProject_2.Simple_Model
{
    public  class SimpleModel
    {
        public async static Task<CoreReturns> UpsertRecord(string db, string table, Guid id, User u)
        {
            if (string.IsNullOrEmpty(table) || u == null || string.IsNullOrEmpty(db))
            {
                return CoreReturns.IS_NULL_OR_EMPTY;
            }
            try
            {
                List<User> list = new List<User>();
                DBManager<User>.Instance(db).LoadAll(table, out list);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]._id.Equals(id))
                    {
                        DBManager<User>.Instance(db).Delete<User>(table, id);
                        break;
                    }
                }
                await DBManager<User>.Instance(db).Insert(table, u);
                return CoreReturns.SUCCESS;
            }
            catch
            {
                LogWriter.Instance().WriteLog("UpsertRecord", $"Mongo object wasn't updated: {u.UserName}");
                return CoreReturns.ERROR;
            }
        }
    }
}
