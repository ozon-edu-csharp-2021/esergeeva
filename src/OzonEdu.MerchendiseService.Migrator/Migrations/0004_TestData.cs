using System;
using FluentMigrator;

namespace OzonEdu.MerchendiseService.Migrator.Migrations
{
    [Migration(4)]
    public class TestData: ForwardOnlyMigration {
        public override void Up()
        {
            Insert
                .IntoTable("employees")
                .Row(new { id = 1, hiring_date = new DateTime(2020, 01, 05)})
                .Row(new { id = 2, hiring_date = new DateTime(2000, 01, 05)})
                .Row(new { id = 3, hiring_date = new DateTime(2021, 01, 05)});
        }
    }
}