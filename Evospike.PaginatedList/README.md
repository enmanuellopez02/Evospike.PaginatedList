# PaginatedList
Add pagination that only returns a list that only contains the requested page

Example of a controller using the extension method

```csharp
[Route("[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<DataCollection<Item>> GetAllAsync(int page = 1, int take = 50)
    {

        var collection = await _context.Items
                            .OrderByDescending(i => i.ItemId)
                            .GetPagedAsync(page, take); //EXTENSION METHOD HERE

        return collection;
    }
}
```