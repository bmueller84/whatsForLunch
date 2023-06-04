namespace WhatsForLunch.Core;

public class FakeDatabaseProvider : IDatabaseProvider
{
    private readonly Dictionary<string, Tag> tags;

    private readonly Dictionary<string, Ingredient> ingredients;

    private readonly Dictionary<string, Meal> meals;

    public FakeDatabaseProvider()
    {
        var defaultTags = new[] { "Aufwändig", "Vegan", "Kindertauglich" };
        tags = new(defaultTags.ToDictionary(t => t, t => new Tag(t)), StringComparer.OrdinalIgnoreCase);

        var defaultIngredients = new[] { "Kartoffel", "Nudel", "Reis", "Butter" };
        ingredients = new(defaultIngredients.ToDictionary(i => i, i => new Ingredient(i)), StringComparer.OrdinalIgnoreCase);

        var defaultMeals = new[]
        {
            new Meal("Empty meal"),
            new Meal("Meal with ingredient")
            {
                Ingredients = new List<Ingredient>() { ingredients["Kartoffel"] }
            },
            new Meal("Meal with tag")
            {
                Tags = new List<Tag>() { tags["Kindertauglich"] }
            },
            new Meal("Meal with ingredients and tags")
            {
                Ingredients = new List<Ingredient>() { ingredients["Nudel"], ingredients["Butter"] },
                Tags = new List<Tag>() { tags["Aufwändig"], tags["Vegan"] }
            }
        };
        meals = new(defaultMeals.ToDictionary(m => m.Name, m => m), StringComparer.OrdinalIgnoreCase);
    }

    public Task<Ingredient> GetOrAddIngredient(Ingredient ingredient)
    {
        if (ingredients.TryGetValue(ingredient.Name, out var existingIngredient))
        {
            return Task.FromResult(existingIngredient);
        }

        ingredients.Add(ingredient.Name, ingredient);
        return Task.FromResult(ingredient);
    }

    public Task<Tag> GetOrAddTag(Tag tag)
    {
        if (tags.TryGetValue(tag.Name, out var existingTag))
        {
            return Task.FromResult(existingTag);
        }

        tags.Add(tag.Name, tag);
        return Task.FromResult(tag);
    }

    public async Task<bool> AddMeal(Meal newMeal)
    {
        if (await GetMeal(newMeal.Name) is not null)
        {
            return false;
        }

        var ingredientTasks = newMeal.Ingredients.Select(GetOrAddIngredient);
        var tagTasks = newMeal.Tags.Select(GetOrAddTag);

        await Task.WhenAll(ingredientTasks);
        await Task.WhenAll(tagTasks);

        var consolidatedMeal = new Meal(newMeal.Name)
        {
            Ingredients = ingredientTasks.Select(i => i.Result).ToList(),
            Tags = tagTasks.Select(t => t.Result).ToList(),
        };

        meals.Add(consolidatedMeal.Name, consolidatedMeal);
        return true;
    }

    public Task<Meal?> GetMeal(string name)
        => Task.FromResult(meals.TryGetValue(name, out Meal? meal) ? meal : null);

    public async IAsyncEnumerable<Meal> GetMeals()
    {
        foreach (var meal in meals.Values)
        {
            yield return meal;
        }
    }

    public async IAsyncEnumerable<Tag> GetAllTags()
    {
        foreach (var tag in meals.Values.SelectMany(m => m.Tags).Distinct())
        {
            yield return tag;
        }
    }

    public async IAsyncEnumerable<Ingredient> GetAllIngredients()
    {
        foreach (var ingredient in meals.Values.SelectMany(m => m.Ingredients).Distinct())
        {
            yield return ingredient;
        }
    }
}
