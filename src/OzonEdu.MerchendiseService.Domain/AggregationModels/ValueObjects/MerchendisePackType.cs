using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects
{
    public class MerchendisePackType: Enumeration
    {
        public static readonly MerchendisePackType WelcomePack = new(10, nameof(WelcomePack));
        public static readonly MerchendisePackType ConferenceListenerPack = new(20, nameof(ConferenceListenerPack));
        public static readonly MerchendisePackType ConferenceSpeakerPack = new(30, nameof(ConferenceSpeakerPack));
        public static readonly MerchendisePackType ProbationPeriodEndingPack = new(40, nameof(ProbationPeriodEndingPack));
        public static readonly MerchendisePackType VeteranPack = new(50, nameof(VeteranPack));
        public MerchendisePackType(int id, string name) : base(id, name)
        {
        }
    }
}