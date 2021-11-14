using FluentMigrator;

namespace OzonEdu.MerchendiseService.Migrator.Migrations
{
    [Migration(1)]
    public class InitialMigration: Migration {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE employees(
                    id BIGSERIAL PRIMARY KEY,
                    hiring_date DATE NOT NULL
                );
            ");
            
            Execute.Sql(@"
                CREATE TABLE merchendise_pack_types(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL
                );
            ");
            
            Execute.Sql(@"
                CREATE TABLE sku_types(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL
                );
            ");
            
            Execute.Sql(@"                
                CREATE TYPE merchendise_item AS (
                    sku_type INT,
                    sku_id INT,
                    quantity INT
                );
            ");

            Execute.Sql(@"
                CREATE TABLE merchendise_packs(
                    id BIGSERIAL PRIMARY KEY,
                    pack_type INT,
                    merchendise_items merchendise_item[] 
                );
            ");
            
            Execute.Sql(@"
                CREATE TABLE merchendise_request_statuses(
                    id BIGSERIAL PRIMARY KEY,
                    name TEXT NOT NULL
                );
            ");
            
            Execute.Sql(@"
                CREATE TABLE merchendise_requests(
                    id BIGSERIAL PRIMARY KEY,
                    status INT NOT NULL,
                    employee_id INT NOT NULL,
                    merchendise_pack_type INT NOT NULL
                )
            ");
        }

        public override void Down()
        {
            Execute.Sql(@"
                DROP TABLE employees;
                DROP TABLE merchendise_pack_types;
                DROP TABLE sku_types;
                DROP TABLE merchendise_packs;
                DROP TABLE merchendise_request_statuses;
                DROP TABLE merchendise_requests;
            ");
        }
    }
}