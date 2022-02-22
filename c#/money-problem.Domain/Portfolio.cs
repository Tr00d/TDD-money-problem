namespace money_problem.Domain
{
    public record Portfolio(params Money[] Moneys)
    {
        public Money Evaluate(
            Bank bank,
            Currency toCurrency)
        {
            var convertedMoneys =
                Moneys
                    .Map(m => bank.Convert(m, toCurrency))
                    .ToList();

            return !convertedMoneys.Lefts().Any()
                ? new Money(convertedMoneys.Rights().Fold(0d, (acc, money) => acc + money.Amount), toCurrency)
                : throw new MissingExchangeRatesException(convertedMoneys.Lefts());
        }
    }
}