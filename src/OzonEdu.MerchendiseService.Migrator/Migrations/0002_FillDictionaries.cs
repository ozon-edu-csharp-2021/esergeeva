using FluentMigrator;

namespace OzonEdu.MerchendiseService.Migrator.Migrations
{
    [Migration(2)]
    public class FillDictionaries: ForwardOnlyMigration {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO merchendise_pack_types (id, name)
                VALUES 
                    (1, 'WelcomePack'),
                    (2, 'ConferenceListenerPack'),
                    (3, 'ConferenceSpeakerPack'),
                    (4, 'ProbationPeriodEndingPack'),
                    (5, 'VeteranPack')
            ");
            
            Execute.Sql(@"
                INSERT INTO merchendise_request_statuses (id, name)
                VALUES 
                    (1, 'Unknown'),
                    (2, 'InProgress'),
                    (3, 'Queued'),
                    (4, 'Done')
            ");
        }
    }
}