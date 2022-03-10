using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amido.Stacks.Domain;
using xxAMIDOxx.xxSTACKSxx.Domain.Entities;
using xxAMIDOxx.xxSTACKSxx.Domain.Events;
using xxAMIDOxx.xxSTACKSxx.Domain.MenuAggregateRoot.Exceptions;
using Amido.Stacks.Domain.Events;

namespace xxAMIDOxx.xxSTACKSxx.Domain
{
#if (CosmosDb || InMemoryDb)
    public class Menu : AggregateRoot<Guid>
    {
        [JsonProperty("Categories")]
        private List<Category> categories;

        public Menu(Guid id, string name, Guid tenantId, string description, bool enabled, List<Category> categories = null)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            Description = description;
            this.categories = categories ?? new List<Category>();
            Enabled = enabled;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public Guid TenantId { get; private set; }

        public string Description { get; private set; }

        [JsonIgnore]
        public IReadOnlyList<Category> Categories { get => categories?.AsReadOnly(); private set => categories = value.ToList(); }

        public bool Enabled { get; private set; }

        public void Update(string name, string description, bool enabled)
        {
            this.Name = name;
            this.Description = description;
            this.Enabled = enabled;

            Emit(new MenuChanged());//TODO: Pass the event data
        }

        public void AddCategory(Guid categoryId, string name, string description)
        {
            if (categories.Any(c => c.Name == name))
                CategoryAlreadyExistsException.Raise(Id, name);

            categories.Add(new Category(categoryId, name, description));

            Emit(new CategoryCreated());//TODO: Pass the event data
        }

        public void UpdateCategory(Guid categoryId, string name, string description)
        {
            var category = GetCategory(categoryId);

            category.Update(name, description);

            Emit(new CategoryChanged());//TODO: Pass the event data
        }

        public void RemoveCategory(Guid categoryId)
        {
            var category = GetCategory(categoryId);

            categories.Remove(category);

            Emit(new CategoryRemoved());//TODO: Pass the event data
        }

        public void AddMenuItem(
            Guid categoryId,
            Guid menuItemId,
            string name,
            string description,
            double price,
            bool available)
        {
            var category = GetCategory(categoryId);

            category.AddMenuItem(
                new MenuItem(
                    menuItemId,
                    name,
                    description,
                    price,
                    available));

            Emit(new MenuItemCreated());//TODO: Pass the event data
        }

        public void UpdateMenuItem(
            Guid categoryId,
            Guid menuItemId,
            string name,
            string description,
            double price,
            bool available)
        {
            var category = GetCategory(categoryId);

            category.UpdateMenuItem(
                new MenuItem(
                    menuItemId,
                    name,
                    description,
                    price,
                    available));

            Emit(new MenuItemChanged());//TODO: Pass the event data
        }

        public void RemoveMenuItem(Guid categoryId, Guid menuItemId)
        {
            var category = GetCategory(categoryId);

            category.RemoveMenuItem(menuItemId);

            Emit(new MenuItemRemoved());//TODO: Pass the event data
        }

        private Category GetCategory(Guid categoryId)
        {
            var category = categories.SingleOrDefault(c => c.Id == categoryId);

            if (category == null)
                CategoryDoesNotExistException.Raise(Id, categoryId);

            return category;
        }
    }

#elif (DynamoDb)

    [DynamoDBTable("Menus")]
    public class Menu
    {
        [JsonProperty("Categories")]
        [DynamoDBIgnore]
        private List<Category> categories;

        public Menu() { }

        public Menu(Guid id, string name, Guid tenantId, string description, bool enabled, List<Category> categories = null)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            Description = description;
            this.categories = categories ?? new List<Category>();
            Enabled = enabled;
        }

        [DynamoDBHashKey]
        [DynamoDBProperty(typeof(DynamoDbGuidConverter))]
        public Guid Id { get; set; }

        public string Name { get; private set; }

        [DynamoDBProperty(typeof(DynamoDbGuidConverter))]
        public Guid TenantId { get; private set; }

        public string Description { get; private set; }

        [JsonIgnore]
        [DynamoDBIgnore]
        public IReadOnlyList<Category> Categories { get => categories?.AsReadOnly(); private set => categories = value.ToList(); }

        public bool Enabled { get; private set; }

        public void Update(string name, string description, bool enabled)
        {
            this.Name = name;
            this.Description = description;
            this.Enabled = enabled;

            Emit(new MenuChanged());//TODO: Pass the event data
        }

        public void AddCategory(Guid categoryId, string name, string description)
        {
            if (categories.Any(c => c.Name == name))
                CategoryAlreadyExistsException.Raise(Id, name);

            categories.Add(new Category(categoryId, name, description));

            Emit(new CategoryCreated());//TODO: Pass the event data
        }

        public void UpdateCategory(Guid categoryId, string name, string description)
        {
            var category = GetCategory(categoryId);

            category.Update(name, description);

            Emit(new CategoryChanged());//TODO: Pass the event data
        }

        public void RemoveCategory(Guid categoryId)
        {
            var category = GetCategory(categoryId);

            categories.Remove(category);

            Emit(new CategoryRemoved());//TODO: Pass the event data
        }

        public void AddMenuItem(
            Guid categoryId,
            Guid menuItemId,
            string name,
            string description,
            double price,
            bool available)
        {
            var category = GetCategory(categoryId);

            category.AddMenuItem(new MenuItem(
                menuItemId,
                name,
                description,
                price,
                available));

            Emit(new MenuItemCreated());//TODO: Pass the event data
        }

        public void UpdateMenuItem(
            Guid categoryId,
            Guid menuItemId,
            string name,
            string description,
            double price,
            bool available)
        {
            var category = GetCategory(categoryId);

            category.UpdateMenuItem(new MenuItem(
                menuItemId,
                name,
                description,
                price,
                available));

            Emit(new MenuItemChanged());//TODO: Pass the event data
        }

        public void RemoveMenuItem(Guid categoryId, Guid menuItemId)
        {
            var category = GetCategory(categoryId);

            category.RemoveMenuItem(menuItemId);

            Emit(new MenuItemRemoved());//TODO: Pass the event data
        }

        private Category GetCategory(Guid categoryId)
        {
            var category = categories.SingleOrDefault(c => c.Id == categoryId);

            if (category == null)
                CategoryDoesNotExistException.Raise(Id, categoryId);

            return category;
        }

        private void Emit(IDomainEvent domainEvent) { }
    }

    public class DynamoDbGuidConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            Guid id = Guid.Parse(value?.ToString());
            DynamoDBEntry entry = new Primitive
            {
                Value = id.ToString()
            };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            Primitive primitive = entry as Primitive;
            if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            Guid id = Guid.Parse(primitive.Value.ToString());
            return id;
        }
    }
#endif

}
