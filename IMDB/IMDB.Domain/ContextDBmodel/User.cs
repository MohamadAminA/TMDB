using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMDB.Domain.DTOs.MovieDTO;

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
        #region Foreign Key
        public List<FavouriteList> FavouriteMovieLists { get; set; }
        public List<MovieRate> Rates { get; set; }
        public WatchList WatchList { get; set; }
        #endregion
    }
    public class WatchList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        #region foreign key
        public User User { get; set; }
        #endregion
    }
    public class FavouriteList
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        #region Foregin Key
        public User User { get; set; }
        public List<FavouriteMovie> FavouriteMovies { get; set; }
        #endregion

    }
    public class FavouriteMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int FavouriteListId { get; set; }

        #region MyRegion
        public FavouriteList FavouriteList { get; set; }
        #endregion
    }
    public class MovieRate
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        #region ForeignKeys Relation
        public User User { get; set; }
        #endregion
    }

}
