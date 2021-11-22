using FluentMigrator;

namespace OzonEdu.MerchendiseService.Migrator.Migrations
{
    [Migration(3)]
    public class FillMerchendisePacks: ForwardOnlyMigration {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO merchendise_packs (id, pack_type, sku_items)
                VALUES 
                    (1, 1, ARRAY[8, 4]),
                    (2, 2, ARRAY[7, 6, 2]),
                    (3, 3, ARRAY[8, 4, 1, 9]),
                    (4, 4, ARRAY[8, 4, 3, 9]),
                    (5, 5, ARRAY[7, 5, 3, 7])
            ");
        }
    }
}