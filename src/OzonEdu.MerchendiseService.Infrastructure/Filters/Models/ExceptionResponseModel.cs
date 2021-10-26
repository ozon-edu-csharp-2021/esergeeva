namespace OzonEdu.MerchendiseService.Infrastructure.Filters.Models
{
    internal readonly struct ExceptionResponseModel
    {
        public string Message { get; init; }
        public string? ExceptionType { get; init; }
        public string? StackTrace { get; init; }
    }
}