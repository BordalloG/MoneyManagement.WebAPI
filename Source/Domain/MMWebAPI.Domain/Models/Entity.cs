using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Domain.Models
{
    public abstract class Entity 
    {
        public int Id { get; set; }
        public Entity()
        {

        }
        public Entity(int id)
        {
            Id = id;
        }
    }
}
