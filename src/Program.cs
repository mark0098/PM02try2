using System;

class Program
{
    static void Main(string[] args)
    {
        // Матрица смежности для графа
        double[,] graph = {
            { 0, 0.94, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.88, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { 0.94, 0, 0.66, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.20, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, 0.66, 0, 1.04, double.PositiveInfinity, 1.70, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, 1.04, 0, double.PositiveInfinity, 0.77, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0, 1.92, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, 1.70, 0.77, 1.92, 0, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.52 },
            { 1.88, 1.20, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0, 0.53, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0.53, 0, 1.54, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.54, 0, 0.86 },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.52, double.PositiveInfinity, double.PositiveInfinity, 0.86, 0 }
        };

        int n = graph.GetLength(0);

        double fuelConsumptionRate = GetFuelConsumptionRateFromUser();

        double[,] distances = Floyd(graph);

        while (true)
        {
            int start = GetPointFromUser("начальной");
            if (start == -1) break;

            int end = GetPointFromUser("конечной");
            if (end == -1) break;

            double shortestDistance = distances[start, end];
            if (shortestDistance == double.PositiveInfinity)
            {
                Console.WriteLine("Путь между указанными точками не существует.\n");
                continue;
            }

            double fuelConsumption = (shortestDistance * fuelConsumptionRate) / 100;
            Console.WriteLine($"Расход топлива для пути между точками {start + 1} и {end + 1}: {fuelConsumption:F3} литров\n");
        }
    }

    private static double[,] Floyd(double[,] a)
    {
        int n = a.GetLength(0);
        double[,] d = new double[n, n];
        d = (double[,])a.Clone();
        for (int i = 1; i <= n; i++)
            for (int j = 0; j <= n - 1; j++)
                for (int k = 0; k <= n - 1; k++)
                    if (d[j, k] > d[j, i - 1] + d[i - 1, k])
                        d[j, k] = d[j, i - 1] + d[i - 1, k];
        return d;
    }

    private static double GetFuelConsumptionRateFromUser()
    {
        while (true)
        {
            Console.Write("Введите расход топлива (л/100км): ");
            try
            {
                return double.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: некорректный формат числа. Пожалуйста, введите число.");
            }
        }
    }

    private static int GetPointFromUser(string pointType)
    {
        while (true)
        {
            Console.Write($"Введите номер {pointType} точки (или 'exit' для выхода): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "exit") return -1;

            try
            {
                int point = int.Parse(input);
                if (point > 0 && point <= 10)
                {
                    return point - 1;
                }
                else
                {
                    Console.WriteLine("Ошибка: номер точки должен быть от 1 до 10.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: некорректный формат числа. Пожалуйста, введите число.");
            }
        }
    }
}
