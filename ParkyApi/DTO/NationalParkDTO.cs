using System;
using System.ComponentModel.DataAnnotations;

namespace Parky.DTO
{
    public class NationalParkDTO
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string state { get; set; }
        public DateTime Created { get; set; }
        public DateTime Establishment { get; set; }
    }
}