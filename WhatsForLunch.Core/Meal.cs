namespace WhatsForLunch.Core;

public record Meal(string Name)
{
    public ImmutableList<Ingredient> Ingredients { get; } = ImmutableList<Ingredient>.Empty;

    public ImmutableList<Tag> Tags { get; } = ImmutableList<Tag>.Empty;
}