using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JukeboxApi.Models;
using System;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;

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
        }

        [HttpGet]
        public ActionResult<List<Suggestion>> GetAll()
        {
            return Ok(_context.Suggestions.ToList());
        }

        [HttpGet("{id}", Name = "GetSuggestion")]
        public ActionResult<Suggestion> GetById(int id)
        {
            var item = _context.Suggestions.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SuggestionDto suggestionDto)
        {
            if (ModelState.IsValid)
            {
                if (RecaptchaResponseIsValid(suggestionDto.Recaptcha))
                {
                    Suggestion suggestion = new Suggestion();
                    suggestion.SubmitterName = suggestionDto.SubmitterName;
                    suggestion.SongName = suggestionDto.SongName;
                    suggestion.ArtistName = suggestionDto.ArtistName;
                    suggestion.Added = DateTime.Now;
                    suggestion.IsActive = true;

                    _context.Suggestions.Add(suggestion);
                    _context.SaveChanges();

                    return Ok(suggestion);
                }
            }

            return BadRequest();
        }

        private bool RecaptchaResponseIsValid(string recaptchaResponse)
        {
            string secret = Environment.GetEnvironmentVariable("RECAPTCHASECRET");

            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={recaptchaResponse}").Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}