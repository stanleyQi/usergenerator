using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class User
    {
        [Key]
        public Int64 Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Birthday { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Thumbnail { get; set; }

        [Required]
        public string LargeImage { get; set; }

        public string Other { get; set; }

        //public ICollection<Image> Reports { get; set; }
    }
}
