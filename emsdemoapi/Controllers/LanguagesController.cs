using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguage _language;
        public LanguagesController(ILanguage language)
        {
            _language = language;
        }

        [HttpGet]
        public IActionResult GetAllLanguage()
        {
            return Ok(_language.GetAllLanguages());
        }

        [HttpGet("{id}")]
        public IActionResult GetLanguageById(int id)
        {
            return Ok(_language.GetLanguageById(id));
        }

        [HttpPost]
        public IActionResult AddLanguage(Language language)
        {
            return Ok(_language.AddLanguage(language));
        }

        [HttpPut]
        public IActionResult UpdateLanguage(Language language)
        {
            return Ok(_language.UpdateLanguage(language));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            return Ok(_language.DeleteLanguage(id));
        }


    }
}
