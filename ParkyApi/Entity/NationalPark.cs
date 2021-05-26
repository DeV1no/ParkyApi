using System;
using System.ComponentModel.DataAnnotations;

namespace Parky.Entity
{
    public class NationalPark
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string state { get; set; }
        public DateTime Created { get; set; }
        public DateTime Establishment { get; set; }
    }
}