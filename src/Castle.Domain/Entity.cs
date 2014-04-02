using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Castle.Domain
{
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the models identity key
        /// </summary>
        public int Id { get; set; }
    }
}