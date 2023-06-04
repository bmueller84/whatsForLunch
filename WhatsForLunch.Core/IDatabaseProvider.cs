namespace WhatsForLunch.Core;

public interface IDatabaseProvider
{
    public IAsyncEnumerable<Meal> GetMeals();

    public Task<Meal?> GetMeal(string name);

    public IAsyncEnumerable<Tag> GetAllTags();

    public IAsyncEnumerable<Ingredient> GetAllIngredients();

    public Task<bool> AddMeal(Meal newMeal);

    public Task<Ingredient> GetOrAddIngredient(Ingredient ingredient);

    public Task<Tag> GetOrAddTag(Tag tag);
}
