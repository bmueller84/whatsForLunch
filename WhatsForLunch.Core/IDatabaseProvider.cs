namespace WhatsForLunch.Core;

public interface IDatabaseProvider
{
    public IAsyncEnumerable<Meal> GetAllMeals();

    public Task<bool> AddMeal(Meal newMeal);
}
