using Xunit;

public class FuelConsumptionTests
{
    [Fact]
    public void TestFuelConsumptionCalculation_Positive()
    {
        // Arrange
        double[,] distances = {
            { 0, 0.94, double.PositiveInfinity },
            { 0.94, 0, 0.66 },
            { double.PositiveInfinity, 0.66, 0 }
        };
        double fuelRate = 10.0; // литров на 100 км
        int startPoint = 0;
        int endPoint = 1;
        double expectedFuelConsumption = 0.094; // ожидаемый расход топлива для пути между точками 1 и 2

        // Act
        double actualFuelConsumption = CalculateFuelConsumption(distances, startPoint, endPoint, fuelRate);

        // Assert
        Assert.Equal(expectedFuelConsumption, actualFuelConsumption, 3);
    }

    [Fact]
    public void TestFuelConsumptionCalculation_Negative_NoPath()
    {
        // Arrange
        double[,] distances = {
            { 0, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, 0, 0.66 },
            { double.PositiveInfinity, 0.66, 0 }
        };
        double fuelRate = 10.0; // литров на 100 км
        int startPoint = 0;
        int endPoint = 1;

        // Act
        double actualFuelConsumption = CalculateFuelConsumption(distances, startPoint, endPoint, fuelRate);

        // Assert
        Assert.Equal(double.PositiveInfinity, actualFuelConsumption);
    }

    [Theory]
    [InlineData(0, 1, 0.1, 0.000001)] // Минимальные значения
    [InlineData(1, 2, 1000.0, 10.0)] // Максимальные значения
    public void TestFuelConsumptionCalculation_BoundaryValues(int startPoint, int endPoint, double fuelRate, double expectedFuelConsumption)
    {
        // Arrange
        double[,] distances = {
        { 0, 0.001, double.PositiveInfinity },
        { 0.001, 0, 1 }, // Изменено расстояние между точками 1 и 2 на 1
        { double.PositiveInfinity, 1, 0 }
    };

        // Act
        double actualFuelConsumption = CalculateFuelConsumption(distances, startPoint, endPoint, fuelRate);

        // Assert
        Assert.Equal(expectedFuelConsumption, actualFuelConsumption, 3);
    }


    [Fact]
    public void TestFuelConsumptionCalculation_Negative_InvalidInput()
    {
        // Arrange
        double[,] distances = {
            { 0, -0.94, double.PositiveInfinity },
            { -0.94, 0, 0.66 },
            { double.PositiveInfinity, 0.66, 0 }
        };
        double fuelRate = -10.0; // литров на 100 км
        int startPoint = 0;
        int endPoint = 1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalculateFuelConsumption(distances, startPoint, endPoint, fuelRate));
    }

    private double CalculateFuelConsumption(double[,] distances, int start, int end, double fuelRate)
    {
        if (fuelRate < 0)
        {
            throw new ArgumentException("Fuel rate cannot be negative.");
        }

        double shortestDistance = distances[start, end];
        if (shortestDistance == double.PositiveInfinity)
        {
            return double.PositiveInfinity;
        }

        return (shortestDistance * fuelRate) / 100;
    }
}
