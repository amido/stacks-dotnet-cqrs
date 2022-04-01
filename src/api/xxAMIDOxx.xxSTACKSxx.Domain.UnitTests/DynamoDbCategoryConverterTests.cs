using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoFixture.Xunit2;
using Xunit;
using xxAMIDOxx.xxSTACKSxx.Domain.Converters;
using xxAMIDOxx.xxSTACKSxx.Domain.Entities;

namespace xxAMIDOxx.xxSTACKSxx.Domain.UnitTests
{
    [Trait("TestType", "UnitTests")]
    public class DynamoDbCategoryConverterTests
    {
        [Theory, AutoData]
        public void CategoryToDynamoDbObject(List<Category> categories)
        {
            // Arrange
            IPropertyConverter converter = new DynamoDbCategoryConverter();

            // Act
            var result = converter.ToEntry(categories);
            var first = result.AsListOfDocument().FirstOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(first);
            Assert.Equal(3, result.AsListOfDocument().Count);

            // Category Fields
            Assert.Equal(categories.First().Id.ToString(), first["Id"]);
            Assert.Equal(categories.First().Name, first["Name"]);
            Assert.Equal(categories.First().Description, first["Description"]);
            Assert.NotNull(first["Items"].AsListOfDocument());
            Assert.Equal(3, first["Items"].AsListOfDocument().Count);

            // MenuItem
            var menuItem = first["Items"].AsListOfDocument().FirstOrDefault();
            Assert.Equal(categories.First().Items.First().Id.ToString(), menuItem["Id"]);
            Assert.Equal(categories.First().Items.First().Description, menuItem["Description"]);
            Assert.Equal(categories.First().Items.First().Name, menuItem["Name"]);
        }

        [Theory, AutoData]
        public void DynamoDbObject(List<Category> categories)
        {
            // Arrange
            IPropertyConverter converter = new DynamoDbCategoryConverter();

            var documents = new List<Document>();
            documents.AddRange(categories.Select(x =>
            {
                var data = new Dictionary<string, DynamoDBEntry>
                {
                    { "Id", x.Id },
                    { "Description", x.Description },
                    { "Name", x.Name }
                };

                return new Document(data);
            }));

            // Act
            var converterResultObj = converter.FromEntry(documents);

            // Assert
            Assert.NotNull(converterResultObj);

            var catResult = converterResultObj as IReadOnlyList<Category>;
            Assert.NotNull(catResult);
            Assert.Equal(3, catResult.Count);

            var first = catResult.FirstOrDefault();
            Assert.Equal(categories.First().Id, first.Id);
            Assert.Equal(categories.First().Description, first.Description);
            Assert.Equal(categories.First().Name, first.Name);
        }
    }
}
