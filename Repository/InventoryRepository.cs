
using InventoryItemEFCore.Data;
using InventoryItemEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryItemEFCore.Repository
{
    public class InventoryRepository
    {
        // 1. Add Inventory Item
        public void Add(InventoryItem item)
        {
            using var context = new ApplicationDbContext();

            context.InventoryItems.Add(item);
            context.SaveChanges();
        }

        // 2. Get All Inventory Items
        public List<InventoryItem> GetAll()
        {
            using var context = new ApplicationDbContext();

            return context.InventoryItems
                .OrderBy(item => item.Id)
                .ToList();
        }

        // 3. Get Inventory Item by ID
        public InventoryItem? GetById(int id)
        {
            using var context = new ApplicationDbContext();

            return context.InventoryItems.Find(id);
        }

        // 4. Get Inventory Item by SKU
        public InventoryItem? GetBySKU(string sku)
        {
            using var context = new ApplicationDbContext();

            return context.InventoryItems
                .FirstOrDefault(item => item.SKU == sku);
        }

        // 5. Update Inventory Item
        public void Update(InventoryItem item)
        {
            using var context = new ApplicationDbContext();

            context.InventoryItems.Update(item);
            context.SaveChanges();
        }

        // 6. Delete Inventory Item
        public void Delete(InventoryItem item)
        {
            using var context = new ApplicationDbContext();

            context.InventoryItems.Remove(item);
            context.SaveChanges();
        }

        // 7. Get Low Stock Items
        public List<InventoryItem> GetLowStockItems()
        {
            using var context = new ApplicationDbContext();

            return context.InventoryItems
                .Where(item => item.QuantityInStock <= item.ReorderLevel)
                .OrderBy(item => item.QuantityInStock)
                .ToList();
        }
    }
}
