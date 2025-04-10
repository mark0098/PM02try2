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
    public void TestFuelConsumptionCalculation_Negative()
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


    private double CalculateFuelConsumption(double[,] distances, int start, int end, double fuelRate)
    {
        double shortestDistance = distances[start, end];
        return (shortestDistance * fuelRate) / 100;
    }
}
