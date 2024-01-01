﻿// <auto-generated/>
namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedBorrowedBooksInBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BorrowedBooks_Id", "dbo.BorrowedBooks");
            DropIndex("dbo.Books", new[] { "BorrowedBooks_Id" });
            CreateTable(
                "dbo.BorrowedBooksBooks",
                c => new
                    {
                        BorrowedBooks_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BorrowedBooks_Id, t.Book_Id })
                .ForeignKey("dbo.BorrowedBooks", t => t.BorrowedBooks_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.BorrowedBooks_Id)
                .Index(t => t.Book_Id);
            
            DropColumn("dbo.Books", "BorrowedBooks_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BorrowedBooks_Id", c => c.Int());
            DropForeignKey("dbo.BorrowedBooksBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BorrowedBooksBooks", "BorrowedBooks_Id", "dbo.BorrowedBooks");
            DropIndex("dbo.BorrowedBooksBooks", new[] { "Book_Id" });
            DropIndex("dbo.BorrowedBooksBooks", new[] { "BorrowedBooks_Id" });
            DropTable("dbo.BorrowedBooksBooks");
            CreateIndex("dbo.Books", "BorrowedBooks_Id");
            AddForeignKey("dbo.Books", "BorrowedBooks_Id", "dbo.BorrowedBooks", "Id");
        }
    }
}