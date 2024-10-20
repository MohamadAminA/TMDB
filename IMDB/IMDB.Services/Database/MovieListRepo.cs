﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer;
using IMDB.DataLayer.Model;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Services.Database
{
    public class MovieListRepo
    {
        private ContextDB _context { get; }
        public MovieListRepo(ContextDB context)
        {
            _context = context;
        }

        public async Task<int> AddToWatchList(WatchList movie)
        {
            if (!await _context.WatchLists.AnyAsync(n => n.MovieId == movie.MovieId && n.UserId == movie.UserId))
                await _context.WatchLists.AddAsync(movie);
            return movie.Id;
        }

        public void RemoveFromWatchList(WatchList movie)
        {
            _context.WatchLists.Remove(movie);
        }
        public async Task RemoveFromWatchList(int userId, int movieId)
        {
            _context.WatchLists.Remove(await _context.WatchLists.FirstOrDefaultAsync(m => m.MovieId == movieId && m.UserId == userId));
        }
        public async Task<int> AddFavouriteList(string Title, int UserId)
        {
            if (!await _context.FavouriteLists.AnyAsync(n => n.Title.Equals(Title)))
            {
                var user = await _context.Users.FindAsync(UserId);
                var listMovie = new FavouriteList()
                {
                    CreateDate = DateTime.Now,
                    Title = Title,
                    UserId = UserId,
                    UserName = user.Name,
                    User = user,
                };
                await _context.FavouriteLists.AddAsync(listMovie);
                return listMovie.Id;
            }
            return 0;
        }
        public async Task<int> UpdateFavouriteList(string Title, int ListId)
        {
            var list = await _context.FavouriteLists.FindAsync(ListId);
            if (list != null)
            {
                list.Title = Title;
                return list.Id;
            }
            return 0;
        }
        public async Task<int> AddFavouriteList(FavouriteList favouriteList)
        {
            await _context.FavouriteLists.AddAsync(favouriteList);
            return favouriteList.Id;
        }
        public async Task<int> AddMovieToList(int listId, int movieId)
        {
            if (!await _context.FavouriteMovies.AnyAsync(n => n.MovieId == movieId && n.FavouriteListId == listId))
            {
                FavouriteMovie movie = new FavouriteMovie() { FavouriteListId = listId, MovieId = movieId };
                await _context.FavouriteMovies.AddAsync(movie);
                return movie.Id;
            }
            return 0;
        }
        public async Task RemoveMovieFromList(int movieId, int ListId)
        {
            _context.FavouriteMovies.Remove(await _context.FavouriteMovies.FirstOrDefaultAsync(n => n.MovieId == movieId && n.FavouriteListId == ListId));
        }
        public async Task RemoveList(int ListId)
        {
            _context.FavouriteLists.Remove(await _context.FavouriteLists.FindAsync(ListId));
        }
        public async Task RemoveMovieFromList(FavouriteMovie movie)
        {
            _context.FavouriteMovies.Remove(movie);
        }
        public async Task<List<FavouriteList>> GetMovieListsById(int userId)
        {
            return await _context.FavouriteLists.Where(n => n.UserId == userId).Include(n => n.FavouriteMovies).ToListAsync();
        }

        public async Task<List<WatchList>> GetWatchListById(int userId)
        {
            return await _context.WatchLists.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<int> AddRateMovie(int userId, int movieId, int rate)
        {
            var old = await _context.Rates.FirstOrDefaultAsync(n => n.MovieId == movieId && userId == n.UserId);
            if (old != null)
            {
                old.Rate = rate;
            }
            else
            {
                old = new MovieRate()
                {
                    CreatedAt = DateTime.Now,
                    MovieId = movieId,
                    UserId = userId,
                    Rate = rate
                };
                await _context.Rates.AddAsync(old);
            }

            return old.Id;

        }
        public async Task RemoveRateMovie(int userId, int movieId)
        {
            _context.Rates.Remove(await _context.Rates.FirstOrDefaultAsync(n => n.MovieId == movieId && n.UserId == userId));
            return;
        }
        public async Task<List<WatchList>> RemoveRateMovie(int userId)
        {
            return await _context.WatchLists.Where(n => n.UserId == userId).ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
