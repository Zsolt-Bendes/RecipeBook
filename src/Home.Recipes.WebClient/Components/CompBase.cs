using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Components;

public abstract class CompBase : ComponentBase
{
    [Parameter]
    public string Class { get; set; } = string.Empty;
}
