using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.DataLayer.Model
{
    public class MovieReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MovieId { get; set; }
        public int? ReplayParent { get; set; }

        #region Relations
        public User User { get; set; }

        [ForeignKey("ReplayParent")]
        public MovieReview Parent { get; set; }

        [InverseProperty("Parent")]
        public List<MovieReview> Replys { get; set; } 
        #endregion
    }
}
