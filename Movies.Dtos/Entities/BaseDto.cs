using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Dtos.Entities
{
    [Serializable]
    public class BaseDto
    {
        public long Id { get; set; }
    }
}
