using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain = xxAMIDOxx.xxSTACKSxx.Domain;

namespace xxAMIDOxx.xxSTACKSxx.CQRS.Queries.GetMenuById
{
    public class Menu
    {
        [Required]
        public Guid Id { get; private set; }

        public Guid TenantId { get; private set; }

        [Required]
        public string Name { get; private set; }

        public string Description { get; private set; }

        [Required]
        public List<Category> Categories { get; private set; }

        [Required]
        public bool? Enabled { get; private set; }

        public static Menu FromDomain(Domain.Menu menu)
        {
            return new Menu()
            {
                Id = menu.Id,
                TenantId = menu.TenantId,
                Name = menu.Name,
                Description = menu.Description,
                Enabled = menu.Enabled,
                Categories = menu.Categories?.Select(c =>
                    new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Items = c.Items?.Select(i =>
                            new MenuItem()
                            {
                                Id = i.Id,
                                Name = i.Name,
                                Description = i.Description,
                                Price = i.Price,
                                Available = i.Available
                            }
                        ).ToList()
                    }).ToList()
            };
        }
    }
}
