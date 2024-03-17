namespace WebAPI.Services;

public class CalculationManager : ICalculationService
{
    public double CalculateAverage(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
        {
            throw new ArgumentException("Please provide a valid array.");
        }

        double sum = 0;
        foreach (var number in numbers)
        {
            sum += number;
        }

        return sum / numbers.Length;
    }
}
