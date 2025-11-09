using DnD_API.Services.Interfaces;
using System.Text.RegularExpressions;

namespace DnD_API.Services
{
    public class DiceService : IDiceService
    {
        private static readonly Regex DiceRegex = new(@"(\d*)d(\d+)([+-]\d+)?", RegexOptions.Compiled);

        public int Roll(string formula, int? seed = null)
        {
            var match = DiceRegex.Match(formula);
            if (!match.Success) throw new ArgumentException("Invalid dice formula");

            int count = string.IsNullOrEmpty(match.Groups[1].Value) ? 1 : int.Parse(match.Groups[1].Value);
            int sides = int.Parse(match.Groups[2].Value);
            int modifier = string.IsNullOrEmpty(match.Groups[3].Value) ? 0 : int.Parse(match.Groups[3].Value);

            var rng = seed.HasValue ? new Random(seed.Value) : new Random();
            int result = Enumerable.Range(0, count).Sum(_ => rng.Next(1, sides + 1)) + modifier;

            return result;
        }
    }
}
