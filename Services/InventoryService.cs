
using InventoryItemEFCore.Data;
using InventoryItemEFCore.Models;

namespace InventoryItemEFCore.Services
{
    public class InventoryService
    {
        public void AddItem()
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter SKU: ");
            string sku = Console.ReadLine() ?? "";

            Console.Write("Enter category: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Enter quantity in stock: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            Console.Write("Enter reorder level: ");
            if (!int.TryParse(Console.ReadLine(), out int reorderLevel) || reorderLevel < 0)
            {
                Console.WriteLine("Invalid reorder level.");
                return;
            }

            Console.Write("Enter unit price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal unitPrice) || unitPrice < 0)
            {
                Console.WriteLine("Invalid unit price.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sku) ||
                string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Name, SKU, and category are required.");
                return;
            }

            try
            {
                using var context = new ApplicationDbContext();

                var item = new InventoryItem
                {
                    Name = name.Trim(),
                    SKU = sku.Trim(),
                    Category = category.Trim(),
                    QuantityInStock = quantity,
                    ReorderLevel = reorderLevel,
                    UnitPrice = unitPrice
                };

                context.InventoryItems.Add(item);
                context.SaveChanges();

                Console.WriteLine("Inventory item added successfully!");
                Console.WriteLine($"Generated Item ID: {item.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not add item: {ex.Message}");
            }
        }
    }
}
