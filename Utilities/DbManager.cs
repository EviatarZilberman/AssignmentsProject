using System.Data;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Numerics;
using static Utilities.MongoStuff;
using System.Collections;
using static MongoDB.Driver.WriteConcern;

namespace Utilities
{
    public class DBManager<T> 
    {
        public IMongoDatabase db;
        public static DBManager<T>? instance = null;

        private DBManager(string database)
        {
            //var client = new MongoClient();
            var client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase(database);
        }

        public static DBManager<T> Instance(string database)
        {
            if (string.IsNullOrEmpty(database))
            {
                return null;
            }

            if (instance == null)
            {
                instance = new DBManager<T>(database);
            }
            return instance;
        }

        public CoreReturns Insert<T>(string table, T record)
        {
            if (record == null || string.IsNullOrEmpty(table))
            {
                // TODO = ADD LOG
                return CoreReturns.IS_NULL;
            }

            try
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                // TODO = ADD LOG
                return CoreReturns.ON_PROCESS;
            }
        }

        public CoreReturns Delete<T>(string table, Guid id)
        {

            if (id == null || string.IsNullOrEmpty(table))
            {
                return CoreReturns.IS_NULL;
            }

            try
            {
                var collection = db.GetCollection<T>(table);
                var filters = Builders<T>.Filter.Eq("_id", id);
                collection.DeleteOne(filters);
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                return CoreReturns.ON_PROCESS;
            }
        }

        public CoreReturns LoadAll<T>(string table, out List<T> TList) // Load all records
        {
            if (string.IsNullOrEmpty(table))
            {
                TList = null;
                return CoreReturns.IS_NULL;
            }

            try
            {
                var collection = db.GetCollection<T>(table);
                TList = collection.Find(new BsonDocument()).ToList();
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                //TODO = ADD LOG
                TList = null;
                return CoreReturns.ON_PROCESS;
            }
        }

        public CoreReturns UpsertRecordById<T>(string table, ObjectId id, T t1)
        {
            if (string.IsNullOrEmpty(table) || id == null || t1 == null)
            {
                Console.WriteLine($"[Func@UpsertRecordById] table is null or empty");
                //Logger.Instance().AddLog($"[Func@UpsertRecordById] table is null or empty");
                return CoreReturns.IS_NULL_OR_EMPTY;
            }
            try
            {
                var collection = this.db.GetCollection<T>(table);
                var r = collection.ReplaceOne(new BsonDocument("_id", id), t1, new UpdateOptions { IsUpsert = true });
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Func@UpsertRecordById] {ex.Message}");
                //Logger.Instance().AddLog($"[Func@UpsertRecordById] {ex.Message}");
                return CoreReturns.ERROR;
            }
        }

        /*public CoreReturns LoadByStatus(string table, out List<SignatureProcess> signatureProcessesList, STATUS_CODE status) // Load all records
        {
            if (string.IsNullOrEmpty(table) || status.Equals(null))
            {
                signatureProcessesList = null;
                return CoreReturns.IS_NULL;
            }

            try
            {
                //signatureProcessesList = new List<SignatureProcess>();
                var collection = this.db.GetCollection<SignatureProcess>(table);
                var filters = Builders<SignatureProcess>.Filter.Eq("Status", status);
                signatureProcessesList = collection.Find(filters).ToList();

                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                //TODO = ADD LOG
                signatureProcessesList = null;
                return CoreReturns.ERROR;
            }
        }*/

        /*public CoreReturns LoadRecordById(string table, ObjectId id, out Player p)
        {
            if (string.IsNullOrEmpty(table) || id.Equals(null))
            {

                p = null;
                return CoreReturns.IS_NULL;
            }

            try
            {
                var collection = db.GetCollection<Player>(table);
                var filter = Builders<Player>.Filter.Eq("_id", id);
                p = collection.Find(filter).First();
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                p = null;
                return CoreReturns.ON_PROCESS;
            }
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.PlayersCollection.Find(a => true).ToList();
        }

        public Player GetPlayerDetails(string gameName)
        {
            //var playerDetails = 
            return this.PlayersCollection
            .Find(m => m.GameName == gameName)
            .FirstOrDefault();
            //return playerDetails;
        }

        public CoreReturns Create(Player player)
        {
            try
            {
                this.PlayersCollection.InsertOne(player);
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                return CoreReturns.NOT_INSERTED;
            }
        }

        public CoreReturns Update(string gameName, Player player)
        {
            try
            {
                var filter = Builders<Player>.Filter.Eq(c => c.GameName, gameName);
                var update = Builders<Player>.Update
                    .Set("BirthDate", player.BirthDate)
                    .Set("Password", player.Password)
                    .Set("Country", player.Country)
                    .Set("Country", player.Country)
                    .Set("Scores", player.Scores)
                    .Set("HighestScore", player.HighestScore)
                    .Set("Registered", player.Registered)
                    .Set("LastSeen", player.LastSeen)
                    .Set("IsActivated", true); // TODO = MAKE SURE IT'S OK!

                this.PlayersCollection.UpdateOne(filter, update);

                return CoreReturns.SUCCESS;
            } catch (Exception ex)
            {
                return CoreReturns.NOT_UPDATEED;
            }
        }

        public CoreReturns Delete(string gameName, string password, string confirmPassword)
        {
            if (!string.IsNullOrEmpty(gameName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(confirmPassword))
            {
                try
                {
                    var filter = Builders<Player>.Filter.Eq(c => c.GameName, gameName);
                    this.PlayersCollection.DeleteOne(filter);

                    return CoreReturns.SUCCESS;
                } catch (Exception ex)
                {
                    return CoreReturns.NOT_DELETED;
                }
            } else
            {
                return CoreReturns.IS_NULL;
            }
        }*/
    }
}