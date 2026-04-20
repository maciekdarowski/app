using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace sledzeniewydatkow
{
    public class ExpenseService
    {
        private readonly string path = "expenses.json";
        public List<Expense> Load()
        {
            if (!File.Exists(path))
                return new List<Expense>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
        }
        public void Save(List<Expense> expenses)
        {
            var json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}