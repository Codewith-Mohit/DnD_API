using DnD_API.Models;
using DnD_API.Services;
using DnD_API.Services.Interfaces;

namespace DnD_API.Tests;

public class DiceServiceTests
{
    [Fact]
    public void Roll_2d6_plus_1_Returns_expected_range_and_breakdown()
    {
        IDiceService service = new DiceService(); 
        string formula = "2d6+1"; 
        int seed = 42;

        // act
        DiceRollResult result = service.Roll(formula, seed);
        // assert
        Assert.Equal(formula, result.Formula);
        Assert.Equal(2, result.Dice.Count);
        Assert.Equal(1, result.Modifier);
        Assert.True(result.Result >= 2 + 1);
        Assert.True(result.Result <= 12 + 1);
        Assert.Equal(2, result.Dice.Count);
    }


    [Fact]
    public void Roll_d20_plus_5_with_seed_is_deterministic()
    {
        IDiceService service = new DiceService();
        var r1 = service.Roll("1d20+5", 7);
        var r2 = service.Roll("1d20+5", 7);

        Assert.Equal(r1.Result, r2.Result);
        Assert.Equal(r1.Dice, r2.Dice);
        Assert.Equal(r1.Formula, r2.Formula);
    }
}
