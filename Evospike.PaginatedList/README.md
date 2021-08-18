# PaginatedList
Add pagination that only returns a list that only contains the requested page

Example of a controller using the extension method

```csharp
[Route("[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly ApplicationDbContext _context;

    public ItemsController(ILogger<ItemsController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet] //RETURN A TYPE DataCollection<T> | add using Evospike.PaginatedList.Models
    public async Task<DataCollection<Item>> GetAllAsync(int page = 1, int take = 50)
    {

        var collection = await _context.Items
                            .OrderByDescending(i => i.ItemId)
                            .GetPagedAsync(page, take); //EXTENSION METHOD HERE

        return collection;
    }

    [HttpGet("test2")] //RETURN A TYPE DataCollection<T> | add using Evospike.PaginatedList.Models
    public DataCollection<Item> GetAllAsync(int page = 1, int take = 50)
    {
        var lista = new List<string> { "test1", "test2", "test3" };
        var collection = lista.GetPaged(page, take); //EXTENSION METHOD HERE

        return collection;
    }
}
```
