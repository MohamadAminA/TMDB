using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.DataLayer.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        public long? Phone { get; set; }
        public string Session { get; set; }
    }
    
}
