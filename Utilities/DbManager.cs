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
using System.Configuration;

namespace Utilities
{
    public class DBManager<T>
    {
        public IMongoDatabase db;
        public static DBManager<T>? instance = null;

        private DBManager(string database)
        {
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

        public async Task<CoreReturns> Insert<T>(string table, T record)
        {
            if (record == null || string.IsNullOrEmpty(table))
            {
                return CoreReturns.IS_NULL;
            }

            try
            {
                var collection = db.GetCollection<T>(table);
                await collection.InsertOneAsync(record);
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                LogWriter.Instance().WriteLog("Insert", $"Error in Insert To Mongo! Error: {ex.Message}");
                return CoreReturns.ON_PROCESS;
            }
        }

        public CoreReturns Delete<T>(string table, Guid id)
        {

            if (string.IsNullOrEmpty(table))
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
            catch
            {
                return CoreReturns.NOT_DELETED;
            }
        }

        public CoreReturns LoadAll<T>(string table, out List<T>? TList) // Load all records
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
                LogWriter.Instance().WriteLog("LoadAll", $"Error in LoadAll from Mongo! Error: {ex.Message}");
                TList = null;
                return CoreReturns.ERROR;
            }
        }

        public async Task<CoreReturns> UpsertRecordById<T>(string table, ObjectId id, T t1)
        {
            if (string.IsNullOrEmpty(table) || t1 == null)
            {
                Console.WriteLine($"[Func@UpsertRecordById] table is null or empty");
                return CoreReturns.IS_NULL_OR_EMPTY;
            }
            try
            {
                var collection = this.db.GetCollection<T>(table);
                var r = await collection.ReplaceOneAsync(new BsonDocument("_id", id), t1, new UpdateOptions { IsUpsert = true });
                return CoreReturns.SUCCESS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Func@UpsertRecordById] {ex.Message}");
                return CoreReturns.ERROR;
            }
        }
    }
}