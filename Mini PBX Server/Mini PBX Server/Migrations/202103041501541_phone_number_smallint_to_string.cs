namespace Mini_PBX_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phone_number_smallint_to_string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientDTOes", "userName", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.ClientDTOes", "phone_number", c => c.String(nullable: false, maxLength: 3, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientDTOes", "phone_number", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.ClientDTOes", "userName", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
