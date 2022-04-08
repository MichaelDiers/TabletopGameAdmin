namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    public abstract class DatabaseMock<T> where T : class

    {
        private readonly IDictionary<string, T> dictionary;
        private readonly Func<IToDictionary, KeyValuePair<string, T>> factory;

        protected DatabaseMock(IDictionary<string, T> dictionary, Func<IToDictionary, KeyValuePair<string, T>> factory)
        {
            this.dictionary = dictionary;
            this.factory = factory;
        }

        protected IEnumerable<T> Values => this.dictionary.Values;

        public async Task<string> InsertAsync(string documentId, IToDictionary data)
        {
            await Task.CompletedTask;
            var kvp = this.factory(data);
            this.dictionary.Add(documentId, kvp.Value);
            return kvp.Key;
        }

        public async Task<string> InsertAsync(IToDictionary data)
        {
            await Task.CompletedTask;
            var kvp = this.factory(data);
            this.dictionary.Add(kvp);
            return kvp.Key;
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

        public Task UpdateByDocumentIdAsync(string documentId, IDictionary<string, object> updates)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOneAsync(string fieldPath, object value, IDictionary<string, object> updates)
        {
            throw new NotImplementedException();
        }
    }
}
