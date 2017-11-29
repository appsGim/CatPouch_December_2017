using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using DogAndCatAPI.DAL;

namespace DogAndCatAPI.Models
{

    public class UIOut
    {
        public int ID { get; set; }
        public string MSG { get; set; }
        [JsonIgnore]
        public int ProjectID { get; set; }
    }

    public class PetOut
    {
        [JsonIgnore]
        public int ID { get; set; }
        public Guid T { get; set; }

        [JsonIgnore]
        public Guid TOld { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int ProjectID { get; set; }
    }

}