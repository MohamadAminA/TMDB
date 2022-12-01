using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer;
using IMDB.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Services.Database
{
    public class ReviewRepo
    {
        private readonly ContextDB _context;
        public ReviewRepo(ContextDB context)
        {
            _context = context;
        }
        public async Task<List<MovieReview>> GetByMovieId(int movieId)
        {
           return await _context.Reviews.Include(n=>n.User).Where(n=>n.MovieId == movieId).ToListAsync();
        }
        public async Task<int> AddReview(MovieReview rev)
        {
            rev.CreatedAt = DateTime.Now;
            await _context.Reviews.AddAsync(rev);
            return rev.Id;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
