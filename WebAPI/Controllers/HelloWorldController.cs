using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly ICalculationService _calculationService;

        public HelloWorldController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }


        /// <summary>
        /// Task1.1-TextHelloWorld : Returns a response containing the string "Hello, World!".
        /// </summary>
        /// <returns>A response containing the string "Hello, World!".</returns>
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }


        /// <summary>
        /// Task1.2-CalculateAverage : Calculates the average of the numbers provided in an array.
        /// </summary>
        /// <param name="numbers">An array of integers.</param>
        /// <returns> An HTTP response indicating the success or failure of the calculation.
        ///           If successful, it returns the average value of the provided numbers.
        ///           If the input array is empty or null, it returns a BadRequest response with an error message.
        ///           If an exception occurs during the calculation, it returns a BadRequest response with the error details.</returns>
        [HttpPost("CalculateAverage")]
        public IActionResult CalculateAverage(int[] numbers)
        {
            try
            {
                double average = _calculationService.CalculateAverage(numbers);
                return Ok(new { average });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = "An error occurred: " + ex.Message });
            }
        }
    }
}
