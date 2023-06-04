namespace WhatsForLunch.Core;

public class FakeDatabaseProvider : IDatabaseProvider
{
    private readonly List<Meal> meals = new()
    {
        new Meal("Empty meal"),
        new Meal("Meal with ingredient")
        {
            Ingredients = new List<Ingredient>() { new Ingredient("Kartoffel") }
        },
        new Meal("Meal with tag")
        {
            Tags = new List<Tag>() { new Tag("Kindertauglich") }
        },
        new Meal("Meal with ingredients and tags")
        {
            Ingredients = new List<Ingredient>() { new Ingredient("Nudel"), new Ingredient("Butter") },
            Tags = new List<Tag>() { new Tag("Aufwändig"), new Tag("Vegan") }
        }
    };

    public Task<bool> AddMeal(Meal newMeal)
    {
        meals.Add(newMeal);
        return Task.FromResult(true);
    }

    public async IAsyncEnumerable<Meal> GetAllMeals()
    {
        foreach (var meal in meals)
        {
            yield return meal;
        }
    }
}
