using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DogAndCatAPI.Models
{

    public class SubmitLead : BaseClass
    {
        [Required]
        public Guid P { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LName { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]//DVIR
        [StringLength(25, MinimumLength = 2)]
        public string City { get; set; }
        [Required]
        [StringLength(35, MinimumLength = 2)] 
        public string Street { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]   
        public string STNumber { get; set; }

        public string FlatNumber { get; set; }

        public string POBox { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; }

        [Required]
        [Range(1, 1)]
        public bool Regulation { get; set; }

        [Required]
        [Range(1, 1)]
        public bool AcceptContent { get; set; }

        ////   [Required]
        //public Guid PetType { get; set; }

        //public DateTime? PetBDay { get; set; }
  
        public string CAP { get; set; }
         
        public SubmitLead() : base() { }
    }

    public class Registered_SubmitLead : BaseClass
    {
        [Required]
        public Guid P { get; set; }

       
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }
        
        public string CAP { get; set; }

        public Registered_SubmitLead() : base() { }
    }

    public class BaseClass
    { 
        /// <summary>
        /// server guid inhstall by developer
        /// </summary>
        [Required]
        public Guid T { get; set; }//knows only to us
          
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string IP { get; set; }
        [Required]
        [StringLength(4000, MinimumLength = 5)]
        public string UA { get; set; }

          
    }
     
    public class in_isAlive: BaseClass
    {
    

        [Required]
        public bool? ND { get; set; }

        public in_isAlive():base() { }
    }


   

  
}