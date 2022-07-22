using RapidPay.Data.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Utilities
{
    public sealed class Utils
    {
        private static readonly Lazy<Utils> lazy = new Lazy<Utils>(() => new Utils());

        public static Utils Instance { get { return lazy.Value; } }

        private Utils()
        {
        }

        public async Task<decimal> FeedCalculator(IPaymentFeeRepository paymentFeeRepository)
        {
            var (fee, updatedDate) = await paymentFeeRepository.GetLastFee();
            decimal paymentFee = fee;
            if ((updatedDate - DateTime.UtcNow).TotalHours > 1 || paymentFee == 0)
            {
                var newFee = CalculateRandomUniversalFeeExchange(0,2);
                if (await paymentFeeRepository.AddPaymentFee(newFee))
                {
                    paymentFee = newFee * (fee == default ? 1 : fee);
                }
            }

            return paymentFee;
        }

        public string GenerateNumberCard()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private decimal CalculateRandomUniversalFeeExchange(double minValue, double maxValue)
        {
            Random randomValue = new Random();
            var next = randomValue.NextDouble();
            return (decimal) (minValue + (next * (maxValue - minValue)));
        }
    }
}
