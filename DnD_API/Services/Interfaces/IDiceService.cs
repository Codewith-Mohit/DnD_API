using DnD_API.Models;

namespace DnD_API.Services.Interfaces
{
    public interface IDiceService
    {
        int Roll(string formula, int? seed = null);
    }
}
