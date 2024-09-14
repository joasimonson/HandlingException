public sealed class Service
{
    public async Task<Result> Operation(DateTime date, bool valid, CancellationToken cancellationToken)
    {
        if (date > DateTime.Now)
            return ServiceErrors.GreaterThanToday;

        await Task.Delay(1000, cancellationToken);

        if (!valid)
            return ServiceErrors.InvalidOperation;

        return Result.Success();
    }
}

public static class ServiceErrors
{
    public static readonly Error GreaterThanToday = new("Service.GreaterThanToday", "Operation date greater than today");

    public static readonly Error InvalidOperation = new("Service.InvalidOperation", "Invalid operation");
}