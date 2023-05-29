namespace WhatsForLunch.Core;

public interface IDatabaseProvider
{
    public IEnumerable<Meal> GetAllMeals();
}
