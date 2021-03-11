namespace Mini_PBX_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Client : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClientDTOes", newName: "Client");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Client", newName: "ClientDTOes");
        }
    }
}
