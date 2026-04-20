using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
namespace sledzeniewydatkow
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ExpenseService service = new ExpenseService();
        public ObservableCollection<Expense> Expenses { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public decimal Total => Expenses.Sum(e => e.Amount);
        public MainViewModel()
        {
            Expenses = new ObservableCollection<Expense>(service.Load());
            AddCommand = new RelayCommand(AddExpense);
            DeleteCommand = new RelayCommand(DeleteExpense);
        }
        private void AddExpense()
        {
            var expense = new Expense
            {
                Name = Name,
                Category = Category,
                Amount = Amount,
                Date = Date
            };
            Expenses.Add(expense);
            Save();
            OnPropertyChanged(nameof(Total));
        }
        private void DeleteExpense()
        {
            if (Expenses.Any())
            {
                Expenses.Remove(Expenses.Last());
                Save();
                OnPropertyChanged(nameof(Total));
            }
        }
        private void Save()
        {
            service.Save(new System.Collections.Generic.List<Expense>(Expenses));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}