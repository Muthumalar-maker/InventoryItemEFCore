
using InventoryItemEFCore.Services;

namespace InventoryItemEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inventoryService = new InventoryService();

            string choice;

            do
            {
                Console.WriteLine("\n=== INVENTORY MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Add Inventory Item");
                Console.WriteLine("2. Search Item by ID");
                Console.WriteLine("3. Search Item by SKU");
                Console.WriteLine("4. Update Inventory Item");
                Console.WriteLine("5. Delete Inventory Item");
                Console.WriteLine("6. View Audit Logs");
                Console.WriteLine("0. Exit");

                Console.Write("\nChoose an option: ");

                choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        inventoryService.AddItem();
                        break;

                    case "2":
                        inventoryService.SearchItemById();
                        break;

                    case "3":
                        inventoryService.SearchItemBySKU();
                        break;

                    case "4":
                        inventoryService.UpdateItem();
                        break;

                    case "5":
                        inventoryService.DeleteItem();
                        break;

                    case "6":
                        var auditLogService = new AuditLogService();
                        auditLogService.ViewAllAuditLogs();
                        break;

                    case "0":
                        Console.WriteLine("\nExiting application...");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }

            } while (choice != "0");
        }
    }
}
