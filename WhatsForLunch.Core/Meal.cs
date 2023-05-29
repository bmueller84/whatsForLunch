namespace WhatsForLunch.Core;

public record Meal(string Name)
{
    public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public IList<Tag> Tags { get; set; } = new List<Tag>();
}