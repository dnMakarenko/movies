using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Data.Core.Models
{
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// Primary key of the entity
        /// </summary>
        public long Id { get; set; }
    }
}
