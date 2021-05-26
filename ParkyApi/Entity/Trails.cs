using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parky.Entity
{
    public class Trails
    {
        [Key] public int Id { get; set; }
        [Required] public string name { get; set; }
        [Required] public double Distance { get; set; }

        public enum DifficultyType
        {
            Easy,
            Moderate,
            Difficult,
            Expert
        }

        public DifficultyType Difficulty { get; set; }
        [Required] public int NationalParkId { get; set; }

        //Relation 
        [ForeignKey("NationalParkId")] public NationalPark NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}