// Объявляем делегат
public delegate void AccountHandler(string message);
public class Account
{
    int sum;
    // Создаем переменную делегата
    AccountHandler? taken;
    public Account(int sum) => this.sum = sum;
    
    // Регистрируем делегат
    public void RegisterHandler(AccountHandler del)
    {
        taken = del;
    }
    public void Add(int sum) => this.sum += sum;
    public void Take(int sum)
    {
        if (this.sum >= sum)
        {
            this.sum -= sum;
            // вызываем делегат, передавая ему сообщение
            taken?.Invoke($"Со счета списано {sum} у.е.");
        }
        else
        {
            taken?.Invoke($"Недостаточно средств. Баланс: {this.sum} у.е.");
        }
    }
}