using Home.Recipes.TestHelpers.Builders.Events;

namespace Home.Recipes.TestHelpers.ObjectMothers;

public static class ObjectMother
{
    public static class Events
    {
        public static class RecipeCreated
        {
            public static RecipeCreatedBuilder Default => _builder;

            private static RecipeCreatedBuilder _builder => new();
        }
    }
}
