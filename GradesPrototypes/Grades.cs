public enum Subject
{
    Математика,
    Физика,
    Химия,
    Биология,
    История
}

// Структура для оценки
public struct Grade
{
    public Subject Subject { get; set; } // Предмет
    public int Score { get; set; }       // Оценка
    public DateTime Date { get; set; }   // Дата

    // Конструктор для инициализации
    public Grade(Subject subject, int score, DateTime date)
    {
        Subject = subject;
        Score = score;
        Date = date;
    }

    // Переопределение ToString для удобного вывода
    public override string ToString()
    {
        return $"{Subject}: {Score} ({Date.ToShortDateString()})";
    }
}
