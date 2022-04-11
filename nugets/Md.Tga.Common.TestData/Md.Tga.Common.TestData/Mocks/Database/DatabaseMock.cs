namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.GoogleCloudFirestore.Contracts.Logic;

    public abstract class DatabaseMock<T> where T : class

    {
        private readonly IDictionary<string, T> dictionary;
        private readonly Func<IDictionary<string, object>, T> factory;

        protected DatabaseMock(IDictionary<string, T> dictionary, Func<IDictionary<string, object>, T> factory)
        {
            this.dictionary = dictionary;
            this.factory = factory;
        }

        protected IEnumerable<T> Values => this.dictionary.Values;

        public async Task<string> InsertAsync(string documentId, IToDictionary data)
        {
            await Task.CompletedTask;
            var value = data.ToDictionary();
            if (!value.TryAdd(DatabaseObject.CreatedName, DateTime.Now))
            {
                value[DatabaseObject.CreatedName] = DateTime.Now;
            }

            if (!value.TryAdd(DatabaseObject.DocumentIdName, documentId))
            {
                value[DatabaseObject.DocumentIdName] = documentId;
            }

            this.dictionary.Add(documentId, this.factory(value));
            return documentId;
        }

        public async Task<string> InsertAsync(IToDictionary data)
        {
            return await this.InsertAsync(Guid.NewGuid().ToString(), data);
        }

        public async Task<T?> ReadByDocumentIdAsync(string documentId)
        {
            await Task.CompletedTask;
            if (this.dictionary.TryGetValue(documentId, out var value))
            {
                return value;
            }

            return null;
        }

        public async Task<IEnumerable<T>> ReadManyAsync()
        {
            await Task.CompletedTask;
            return this.dictionary.Values;
        }

        public async Task<IEnumerable<T>> ReadManyAsync(string fieldPath, object value)
        {
            return await this.ReadManyAsync(fieldPath, value, OrderType.Unsorted);
        }

        public virtual Task<IEnumerable<T>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}
