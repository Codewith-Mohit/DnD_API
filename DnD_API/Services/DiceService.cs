using DnD_API.Models;
using DnD_API.Services.Interfaces;
using System.Text.RegularExpressions;

namespace DnD_API.Services
{
    public class DiceService : IDiceService
    {
        //It is lightweight parser. It handles common forms like "2d6+1" or "1d20-2". It can be extended if needed.
        private static readonly Regex DiceRegex = new(@"(\d*)d(\d+)([+-]\d+)?", RegexOptions.Compiled);

        public DiceRollResult Roll(string formula, int? seed = null)
        {
            // Very small parser: supports [XdY][+/-Z], e.g., "2d6+1", "1d20-2"
            if (string.IsNullOrWhiteSpace(formula))
                throw new ArgumentException("Formula is required");

            int total = 0;
            var rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

            // parse
            int dIndex = formula.IndexOf('d');
            if (dIndex <= 0)
                throw new ArgumentException("Invalid formula");

            int numDice = int.Parse(formula.Substring(0, dIndex));
            int sides = int.Parse(new string(formula.Skip(dIndex + 1).TakeWhile(char.IsDigit).ToArray()));

            int offsetIndex = formula.IndexOfAny(new[] { '+', '-' });
            int modifier = 0;
            if (offsetIndex >= 0)
            {
                var modPart = formula.Substring(offsetIndex);
                modifier = int.Parse(modPart);
            }

            var dice = new List<int>();
            for (int i = 0; i < numDice; i++)
            {
                int roll = rng.Next(1, sides + 1);
                dice.Add(roll);
                total += roll;
            }
            total += modifier;

            return new DiceRollResult
            {
                Formula = formula,
                Result = total,
                Dice = dice,
                Modifier = modifier
            };
        }
    }
}