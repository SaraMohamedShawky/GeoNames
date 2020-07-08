namespace GeonameAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoNames",
                c => new
                    {
                        geonameid = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        asciiname = c.String(),
                        alternatenames = c.String(),
                        latitude = c.Single(nullable: false),
                        longitude = c.Single(nullable: false),
                        feature_class = c.String(),
                        feature_code = c.String(),
                        country_code = c.String(),
                        cc2 = c.String(),
                        admin1_code = c.String(),
                        admin2_code = c.String(),
                        admin3_code = c.String(),
                        admin4_code = c.String(),
                        population = c.Long(nullable: false),
                        elevation = c.Int(nullable: false),
                        gtopo30 = c.Int(nullable: false),
                        timezone = c.Int(nullable: false),
                        modification_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.geonameid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeoNames");
        }
    }
}
