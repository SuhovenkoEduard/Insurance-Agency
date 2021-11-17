using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using System.Windows;
using MongoDB.Bson.Serialization;

namespace CourseApp.DataLayer.Adapters
{
    public class MongoAdapter : IAdapter
    {
        protected MongoDB mongo;
        public MongoAdapter(MongoDB mongo)
        {
            this.mongo = mongo;
        }

        public void Add<T>(T item) where T : class, new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                if ((int) properties[0].GetValue(item) == 0)
                    properties[0].SetValue(item, GetAll<T>().Select(x => (int) properties[0].GetValue(x)).Max() + 1);
                var collection = mongo.Database.GetCollection<T>(type.Name);
                collection.InsertOne(item);
            } catch (Exception e)
            {
                MessageBox.Show($"Вставка в коллекцию {type.Name} завершилась неудачей. " + e.Message);
            }
        }

        public List<T> GetAll<T>() where T : class, new()
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                var collection = mongo.Database.GetCollection<BsonDocument>(type.Name);
                var list = collection.Aggregate().ToList();
                list.ForEach(x => x.Remove("_id"));
                result = list.Select(x => BsonSerializer.Deserialize<T>(x)).ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Получение элементов из коллекции {type.Name} завершилось неудачей. " + e.Message);
            }

            return result;
        }

        public void Remove<T>(T item) where T : class, new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                var collection = mongo.Database.GetCollection<BsonDocument>(type.Name);
                var filter = Builders<BsonDocument>.Filter.Eq(x => x[properties[0].Name], properties[0].GetValue(item));
                collection.DeleteOne(filter);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Удаление из коллекции {type.Name} завершилась неудачей. " + e.Message);
            }
        }

        public void Update<T>(T item) where T : class, new()
        {
            Type type = typeof(T);
            try
            {
                Remove(item);
                Add(item);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Обновление элемента в коллекции {type.Name} завершилась неудачей. " + e.Message);
            }
        }
    }
}
