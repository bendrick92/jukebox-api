using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JukeboxApi.Models;
using System;

namespace JukeboxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionContext _context;

        public SuggestionController(SuggestionContext context)
        {
            _context = context;

            if (_context.Suggestions.Count() == 0)
            {
                _context.Suggestions.Add(new Suggestion
                {
                    SubmitterName = "Ben Walters",
                    SongName = "Castle On The Hill",
                    ArtistName = "Ed Sheeran",
                    Added = DateTime.Now
                });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Suggestion>> GetAll()
        {
            return _context.Suggestions.ToList();
        }

        [HttpGet("{id}", Name = "GetSuggestion")]
        public ActionResult<Suggestion> GetById(int id)
        {
            var item = _context.Suggestions.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(Suggestion suggestion)
        {
            suggestion.Added = DateTime.Now;

            _context.Suggestions.Add(suggestion);
            _context.SaveChanges();

            return CreatedAtRoute("GetSuggestion", new { id = suggestion.Id }, suggestion);
        }
    }
}