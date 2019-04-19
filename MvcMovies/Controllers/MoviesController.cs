using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMoviesContext _context;

        public MoviesController(MvcMoviesContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            // ProducerId , ActorId
            var Producer = await _context.Producer.FirstAsync(m => m.ProducerID == movie.ProducerId);
            ViewBag.Producer = Producer.Name;
            var Actor = await _context.Actors.FirstAsync(m => m.ActorID == movie.ActorId);
            ViewBag.Actor = Actor.Name;
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            var ProducersList = from m in _context.Producer select m.Name;
            ViewBag.ProducersList = new SelectList( ProducersList.AsEnumerable());
            var ActorsList = from a in _context.Actors select a.Name;
            ViewBag.ActorsList = new SelectList(ActorsList);
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Name,ReleaseDate,Plot,Poster,ProducerName,SelectedActors")] Movie movie)
        {
            if (ModelState.IsValid && !movie.ProducerName.Equals(null))
            {
                var producers = from p in _context.Producer select p;
                var temp = producers.Where(x => x.Name == movie.ProducerName);
                movie.ProducerId = temp.Select(t => new { ProducerId = t.ProducerID }).Single().ProducerId;
                
                var actors = from a in _context.Actors select a;
                var tempa = actors.Where(x => x.Name == movie.SelectedActors);
                movie.ActorId = tempa.Select(t => new { ActorId = t.ActorID }).Single().ActorId;
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            var ProducersList = from m in _context.Producer select m.Name;
            ViewBag.ProducersList = new SelectList(ProducersList.AsEnumerable());
            var ActorsList = from a in _context.Actors select a.Name;
            ViewBag.ActorsList = new SelectList(ActorsList);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,Name,ReleaseDate,Plot,Poster,ProducerName,SelectedActors")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var producers = from p in _context.Producer select p;
                    var temp = producers.Where(x => x.Name == movie.ProducerName);
                    movie.ProducerId = temp.Select(t => new { ProducerId = t.ProducerID }).Single().ProducerId;

                    var actors = from a in _context.Actors select a;
                    var tempa = actors.Where(x => x.Name == movie.SelectedActors);
                    movie.ActorId = tempa.Select(t => new { ActorId = t.ActorID }).Single().ActorId;
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }
    }
}
