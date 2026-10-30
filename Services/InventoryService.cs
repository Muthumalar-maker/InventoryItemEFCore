using InventoryItemEFCore.Services;
using InventoryItemEFCore.Data;
using InventoryItemEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;



namespace InventoryItemEFCore.Services
{
    public class InventoryService
    {
        private readonly AuditLogService auditLogService = new AuditLogService();
        public void AddItem()
        {
            Console.Write("Enter item name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter SKU: ");
            string sku = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter category: ");
            string category = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sku) ||
                string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Name, SKU, and category are required.");
                return;
            }

            Console.Write("Enter quantity in stock: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) ||
                quantity < 0)
            {
                Console.WriteLine("Enter a valid non-negative quantity.");
                return;
            }

            Console.Write("Enter reorder level: ");
            if (!int.TryParse(Console.ReadLine(), out int reorderLevel) ||
                reorderLevel < 0)
            {
                Console.WriteLine("Enter a valid non-negative reorder level.");
                return;
            }

            Console.Write("Enter unit price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal unitPrice) ||
                unitPrice < 0)
            {
                Console.WriteLine("Enter a valid non-negative unit price.");
                return;
            }

            try
            {
                using var context = new ApplicationDbContext();

                // Check database connectivity first.
                if (!context.Database.CanConnect())
                {
                    Console.WriteLine(
                        "Cannot connect to SQL Server. Check that SQL Server Express is running.");
                    return;
                }

                // Check for an existing SKU.
                bool skuExists = context.InventoryItems
                    .Any(item => item.SKU == sku);

                if (skuExists)
                {
                    Console.WriteLine(
                        "This SKU already exists. Please enter a different SKU.");
                    return;
                }

                var item = new InventoryItem
                {
                    Name = name,
                    SKU = sku,
                    Category = category,
                    QuantityInStock = quantity,
                    ReorderLevel = reorderLevel,
                    UnitPrice = unitPrice
                };

                context.InventoryItems.Add(item);
                context.SaveChanges();

                auditLogService.LogActivity(
                    item.Id,
                    "INSERT",
                    $"{item.Name} item added successfully"
                );

                Console.WriteLine("\nInventory item added successfully!");
                Console.WriteLine($"Generated Item ID: {item.Id}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Database could not save the inventory item.");
                Console.WriteLine($"Details: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Server error occurred.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred.");
                Console.WriteLine($"Details: {ex}");
            }
        }


        public void SearchItemById()
        {
            Console.Write("Enter Item ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid Item ID.");
                return;
            }

            using var context = new ApplicationDbContext();

            var item = context.InventoryItems.Find(id);

            if (item == null)
            {
                Console.WriteLine("Inventory item not found.");
                return;
            }

            Console.WriteLine("\n=== INVENTORY ITEM DETAILS ===");
            Console.WriteLine($"ID            : {item.Id}");
            Console.WriteLine($"Name          : {item.Name}");
            Console.WriteLine($"SKU           : {item.SKU}");
            Console.WriteLine($"Category      : {item.Category}");
            Console.WriteLine($"Quantity      : {item.QuantityInStock}");
            Console.WriteLine($"Reorder Level : {item.ReorderLevel}");
            Console.WriteLine($"Unit Price    : {item.UnitPrice:F2}");
        }

        public void SearchItemBySKU()
        {
            Console.Write("Enter Item SKU: ");
            string sku = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine("SKU cannot be empty.");
                return;
            }

            using var context = new ApplicationDbContext();

            var item = context.InventoryItems
                .FirstOrDefault(item => item.SKU == sku);

            if (item == null)
            {
                Console.WriteLine("Inventory item not found.");
                return;
            }

            Console.WriteLine("\n=== INVENTORY ITEM DETAILS ===");
            Console.WriteLine($"ID            : {item.Id}");
            Console.WriteLine($"Name          : {item.Name}");
            Console.WriteLine($"SKU           : {item.SKU}");
            Console.WriteLine($"Category      : {item.Category}");
            Console.WriteLine($"Quantity      : {item.QuantityInStock}");
            Console.WriteLine($"Reorder Level : {item.ReorderLevel}");
            Console.WriteLine($"Unit Price    : {item.UnitPrice:F2}");
        }



        public void UpdateItem()
        {
            Console.Write("Enter Item ID to update: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid Item ID.");
                return;
            }

            using var context = new ApplicationDbContext();

            var item = context.InventoryItems.Find(id);

            if (item == null)
            {
                Console.WriteLine("Inventory item not found.");
                return;
            }

            Console.WriteLine("\n=== CURRENT ITEM DETAILS ===");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"SKU: {item.SKU}");
            Console.WriteLine($"Category: {item.Category}");
            Console.WriteLine($"Quantity: {item.QuantityInStock}");
            Console.WriteLine($"Reorder Level: {item.ReorderLevel}");
            Console.WriteLine($"Unit Price: {item.UnitPrice:F2}");

            Console.WriteLine("\nEnter the new details:");

            Console.Write("Enter new name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter new category: ");
            string category = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Name and category cannot be empty.");
                return;
            }

            Console.Write("Enter new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) ||
                quantity < 0)
            {
                Console.WriteLine("Enter a valid non-negative quantity.");
                return;
            }

            Console.Write("Enter new reorder level: ");
            if (!int.TryParse(Console.ReadLine(), out int reorderLevel) ||
                reorderLevel < 0)
            {
                Console.WriteLine("Enter a valid non-negative reorder level.");
                return;
            }

            Console.Write("Enter new unit price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal unitPrice) ||
                unitPrice < 0)
            {
                Console.WriteLine("Enter a valid non-negative unit price.");
                return;
            }

            item.Name = name;
            item.Category = category;
            item.QuantityInStock = quantity;
            item.ReorderLevel = reorderLevel;
            item.UnitPrice = unitPrice;

            try
            {
                context.SaveChanges();
                Console.WriteLine("\nInventory item updated successfully!");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine("Unable to update the inventory item.");
                Console.WriteLine(
                    ex.InnerException?.Message ?? ex.Message);
            }
        }


        public void DeleteItem()
        {
            Console.Write("Enter Item ID to delete: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid Item ID.");
                return;
            }

            using var context = new ApplicationDbContext();

            var item = context.InventoryItems.Find(id);

            if (item == null)
            {
                Console.WriteLine("Inventory item not found.");
                return;
            }

            Console.WriteLine("\n=== ITEM DETAILS ===");
            Console.WriteLine($"ID       : {item.Id}");
            Console.WriteLine($"Name     : {item.Name}");
            Console.WriteLine($"SKU      : {item.SKU}");
            Console.WriteLine($"Category : {item.Category}");

            Console.Write("\nAre you sure you want to delete this item? (Y/N): ");
            string confirmation = (Console.ReadLine() ?? "").Trim();

            if (!confirmation.Equals(
                "Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Delete operation cancelled.");
                return;
            }

            try
            {
                context.InventoryItems.Remove(item);
                context.SaveChanges();

                Console.WriteLine("Inventory item deleted successfully!");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine("Unable to delete the inventory item.");
                Console.WriteLine(
                    ex.InnerException?.Message ?? ex.Message);
            }
        }



    }
}
