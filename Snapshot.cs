using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class Snapshot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime? date { get; set; }
        public string data { get; set; }
        public byte? tram { get; set; }
        public byte? bus { get; set; }
        [NotMapped]
        public List<Position> Positions => data != null ? JsonConvert.DeserializeObject<List<Position>>(data) : new List<Position>();
    }
}