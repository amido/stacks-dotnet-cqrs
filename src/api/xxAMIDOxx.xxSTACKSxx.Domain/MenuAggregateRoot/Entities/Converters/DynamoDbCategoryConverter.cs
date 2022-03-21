using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using xxAMIDOxx.xxSTACKSxx.Domain.Entities;

namespace xxAMIDOxx.xxSTACKSxx.Domain.MenuAggregateRoot.Entities.Converters
{
    // IMPORTANT!!!
    // Since Categories is IReadOnlyCollection<T> we need the converter to work with that interface both during serialization
    // and deserialization. If Categories was a property using a CONCRETE type like List<T>, then the DynamoSDK would automatically handle
    // the collection mapping and we could've made a converter for only a single Category
    // DynamoDB .NET Object Persistence Model Types - https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKHighLevel.html#DotNetDynamoDBContext.SupportedTypes
    public class DynamoDbCategoryConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            IEnumerable<Category> categories = value as IReadOnlyCollection<Category>;
            List<Primitive> primitives = new List<Primitive>();

            if (categories == null) throw new ArgumentOutOfRangeException();

            primitives.AddRange(categories.Select(x =>
            {
                var category = JsonConvert.SerializeObject(x);
                var dbEntry = new Primitive(category);
                return dbEntry;
            }));

            return new PrimitiveList() { Entries = primitives };
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            PrimitiveList primitives = entry as PrimitiveList;
            if (primitives == null || (primitives.Entries.Count == 0))
                throw new ArgumentOutOfRangeException();

            var categories = primitives.Entries
                .Select(x => JsonConvert.DeserializeObject<Category>((string)x.Value))
                .ToList()
                .AsReadOnly();

            return categories;
        }
    }
}
