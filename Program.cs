
using InventoryItemEFCore.Services;

namespace InventoryItemEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inventoryService = new InventoryService();

            Console.WriteLine("=== INVENTORY MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Add Inventory Item");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    inventoryService.AddItem();
                    break;

                case "0":
                    Console.WriteLine("Exiting application...");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
