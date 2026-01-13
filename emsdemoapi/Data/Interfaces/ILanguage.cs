using emsdemoapi.Data.Entities;
using System.Reflection.Emit;

namespace emsdemoapi.Data.Interfaces
{
    public interface ILanguage
    {
        List<Language> GetAllLanguages();
        Language GetLanguageById(int id);
        bool AddLanguage(Language language);
        bool DeleteLanguage(int id);
        bool UpdateLanguage(Language language);
    }
}
