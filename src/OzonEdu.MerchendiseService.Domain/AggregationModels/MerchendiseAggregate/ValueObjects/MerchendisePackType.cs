using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects
{
    public sealed class MerchendisePackType: Enumeration
    {
        public static readonly MerchendisePackType WelcomePack = new(1, nameof(WelcomePack));
        public static readonly MerchendisePackType ConferenceListenerPack = new(2, nameof(ConferenceListenerPack));
        public static readonly MerchendisePackType ConferenceSpeakerPack = new(3, nameof(ConferenceSpeakerPack));
        public static readonly MerchendisePackType ProbationPeriodEndingPack = new(4, nameof(ProbationPeriodEndingPack));
        public static readonly MerchendisePackType VeteranPack = new(5, nameof(VeteranPack));
        public MerchendisePackType(long id, string name) : base(id, name)
        {
        }
    }
}