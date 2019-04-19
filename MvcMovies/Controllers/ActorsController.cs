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
    public class ActorsController : Controller
    {
        private readonly MvcMoviesContext _context;

        public ActorsController(MvcMoviesContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorID == id);
            if (actor == null)
            {
                return NotFound();
            }
            var Movie = await _context.Movies.FirstAsync(m => m.MovieID == actor.MovieId);
            ViewBag.Movie = Movie.Name;
            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            var MoviesList = from m in _context.Movies select m.Name;
            ViewBag.MoviesList = new SelectList(MoviesList.AsEnumerable());
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorID,Sex,DOB,Bio,Name,MovieName")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                var movies = from p in _context.Movies select p;
                var temp = movies.Where(x => x.Name == actor.MovieName);
                actor.MovieId = temp.Select(t => new { MovieID = t.MovieID}).Single().MovieID;
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            var MoviesList = from m in _context.Movies select m.Name;
            ViewBag.MoviesList = new SelectList(MoviesList.AsEnumerable());
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorID,Sex,DOB,Bio,Name,MovieName")] Actor actor)
        {
            if (id != actor.ActorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movies = from p in _context.Movies select p;
                    var temp = movies.Where(x => x.Name == actor.MovieName);
                    actor.MovieId = temp.Select(t => new { MovieID = t.MovieID }).Single().MovieID;
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorID))
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorID == id);
        }
    }
}
