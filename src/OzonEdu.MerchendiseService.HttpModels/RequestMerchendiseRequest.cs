using CSharpCourse.Core.Lib.Enums;

namespace OzonEdu.MerchendiseService.HttpModels
{
    public record RequestMerchendiseRequest
    {
        public long EmployeeId { get; init; }
        public MerchType MerchendisePackType { get; init; }
    }
}