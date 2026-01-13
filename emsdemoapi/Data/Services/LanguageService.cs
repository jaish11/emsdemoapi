using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;

namespace emsdemoapi.Data.Services
{
    public class LanguageService : ILanguage
    {
        private readonly AppDbContext _context;
        public LanguageService(AppDbContext context)
        {
            _context = context;
        }
        public bool AddLanguage(Language language)
        {
            _context.Languages.Add(language);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteLanguage(int id)
        {
            Language language = _context.Languages.Find(id);
            _context.Languages.Remove(language);
            _context.SaveChanges();
            return true;
        }

        public List<Language> GetAllLanguages()
        {
            return _context.Languages.ToList();
        }

        public Language GetLanguageById(int id)
        {
            return _context.Languages.Find(id);
        }

        public bool UpdateLanguage(Language language)
        {
            _context.Languages.Update(language);
            _context.SaveChanges();
            return true;
        }
    }
}
