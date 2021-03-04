namespace Mini_PBX_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientDTOes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 256),
                        phone_number = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientDTOes");
        }
    }
}
