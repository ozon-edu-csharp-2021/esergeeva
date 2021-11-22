namespace OzonEdu.MerchendiseService.DbInfrastructure.Models
{
    public class MerchendiseRequest
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public int PackType { get; set; }
    }
}