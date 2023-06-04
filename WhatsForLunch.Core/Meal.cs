namespace WhatsForLunch.Core;

public record Meal(string Name, DateTime Added)
{
    public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public IList<Tag> Tags { get; set; } = new List<Tag>();
}