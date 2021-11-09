using System;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Extensions
{
    internal static class MerchendiseConverterExtensions
    {
        public static MerchType Convert(this MerchendisePackType merchType)
        {
            return merchType switch
            {
                _ when merchType == MerchendisePackType.WelcomePack => MerchType.WelcomePack,
                _ when merchType == MerchendisePackType.ProbationPeriodEndingPack =>
                    MerchType.ProbationPeriodEndingPack,
                _ when merchType == MerchendisePackType.VeteranPack => MerchType.VeteranPack,
                _ when merchType == MerchendisePackType.ConferenceListenerPack => MerchType.ConferenceListenerPack,
                _ when merchType == MerchendisePackType.ConferenceSpeakerPack => MerchType.ConferenceSpeakerPack,
                _ => throw new ArgumentOutOfRangeException(nameof(merchType), merchType.Name,
                    "Unexpected merchendise type")
            };
        }

        public static MerchendisePackType ConvertToDomain(this MerchType merchType)
        {
            return merchType switch
            {
                MerchType.WelcomePack => MerchendisePackType.WelcomePack,
                MerchType.ConferenceListenerPack => MerchendisePackType.ConferenceListenerPack,
                MerchType.ConferenceSpeakerPack => MerchendisePackType.ConferenceSpeakerPack,
                MerchType.ProbationPeriodEndingPack => MerchendisePackType.ProbationPeriodEndingPack,
                MerchType.VeteranPack => MerchendisePackType.VeteranPack,
                _ => throw new ArgumentOutOfRangeException(nameof(merchType), merchType,
                    "Unexpected merchendise type")
            };
        }

        public static RequestStatus Convert(this MerchendiseRequestStatus status)
        {
            return status switch
            {
                _ when status == MerchendiseRequestStatus.InProgress => RequestStatus.InProgress,
                _ when status == MerchendiseRequestStatus.Queued => RequestStatus.Queued,
                _ when status == MerchendiseRequestStatus.Done => RequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status.Name,
                    "Unexpected merchendise request status")
            };
        }

        public static MerchendiseRequestStatus Convert(RequestStatus status)
        {
            return status switch
            {
                RequestStatus.InProgress => MerchendiseRequestStatus.InProgress,
                RequestStatus.Queued => MerchendiseRequestStatus.Queued,
                RequestStatus.Done => MerchendiseRequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status,
                    "Unexpected merchendise request status")
            };
        }
    }
}