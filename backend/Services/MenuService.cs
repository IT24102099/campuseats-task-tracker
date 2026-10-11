using CampusEats.Api.Dtos;
using CampusEats.Api.Models;
namespace CampusEats.Api.Services;

public class MenuService : IMenuService
{
    private readonly List<MenuItem> _items;
    private int _nextId;

    // Seed in-memory data
    public MenuService()
    {
        _items = new()
        {
            new() { Id = 1, Name = "Kottu Roti", Price = 750m, Category = "Mains" },
            new() { Id = 2, Name = "Fried Rice", Price = 850m, Category = "Mains" },
            new() { Id = 3, Name = "Watalappan", Price = 350m, Category = "Dessert" }
        };

        _nextId = 4;
    }

    // Convert Model -> DTO
    private static MenuItemDto ToDto(MenuItem m) =>
        new(m.Id, m.Name, m.Price, m.Category, m.Available);

    // Get all menu items
    public IEnumerable<MenuItemDto> GetAll() =>
        _items.Select(ToDto);

    // Get menu item by ID
    public MenuItemDto? GetById(int id)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);
        return m is null ? null : ToDto(m);
    }

    // Create new menu item
    public MenuItemDto Create(CreateMenuItemDto dto)
    {
        var item = new MenuItem
        {
            Id = _nextId++,
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            Available = true
        };

        _items.Add(item);
        return ToDto(item);
    }

    // Update existing menu item
    public bool Update(int id, CreateMenuItemDto dto)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);

        if (m is null)
            return false;

        m.Name = dto.Name;
        m.Price = dto.Price;
        m.Category = dto.Category;

        return true;
    }

    // Delete menu item
    public bool Delete(int id)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);

        if (m is null)
            return false;

        _items.Remove(m);
        return true;
    }
}