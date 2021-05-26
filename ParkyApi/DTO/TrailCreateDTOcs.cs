using System.ComponentModel.DataAnnotations;
using Parky.Entity;

namespace Parky.DTO
{
    public class TrailCreatetDTO
    {
        [Required] public string name { get; set; }
        [Required] public double Distance { get; set; }
        public Trails.DifficultyType Difficulty { get; set; }
        [Required] public int NationalParkId { get; set; }
    }
}