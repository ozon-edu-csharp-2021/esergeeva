using System;
using CommonMerchendisePackType = CSharpCourse.Core.Lib.Enums.MerchType;
using GrpcMerchendisePackType = OzonEdu.MerchendiseService.Grpc.MerchendisePackType;

namespace OzonEdu.MerchendiseService.Presentation.Extensions
{
    internal static class MerchendisePackTypeConverters
    {
        public static CommonMerchendisePackType Convert(this GrpcMerchendisePackType packType)
        {
            return packType switch
            {
                GrpcMerchendisePackType.WelcomePack => CommonMerchendisePackType.WelcomePack,
                GrpcMerchendisePackType.ConferenceListenerPack => CommonMerchendisePackType.ConferenceListenerPack,
                GrpcMerchendisePackType.ConferenceSpeakerPack => CommonMerchendisePackType.ConferenceSpeakerPack,
                GrpcMerchendisePackType.ProbationPeriodEndingPack => CommonMerchendisePackType.ProbationPeriodEndingPack,
                GrpcMerchendisePackType.VeteranPack => CommonMerchendisePackType.VeteranPack,
                _ => throw new ArgumentOutOfRangeException(nameof(packType), packType,
                    "Unexpected merchendise pack type")
            };
        }
        
        public static GrpcMerchendisePackType ConvertToGrpc(this CommonMerchendisePackType packType)
        {
            return packType switch
            {
                CommonMerchendisePackType.WelcomePack => GrpcMerchendisePackType.WelcomePack,
                CommonMerchendisePackType.ConferenceListenerPack => GrpcMerchendisePackType.ConferenceListenerPack,
                CommonMerchendisePackType.ConferenceSpeakerPack => GrpcMerchendisePackType.ConferenceSpeakerPack,
                CommonMerchendisePackType.ProbationPeriodEndingPack => GrpcMerchendisePackType.ProbationPeriodEndingPack,
                CommonMerchendisePackType.VeteranPack => GrpcMerchendisePackType.VeteranPack,
                _ => throw new ArgumentOutOfRangeException(nameof(packType), packType,
                    "Unexpected merchendise pack type")
            };
        }
    }
}