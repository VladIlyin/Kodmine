using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kodmine.DAL.Migrations
{
    public partial class Dev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostTags_BlogId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostTagId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PostTagId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_PostTags_PostTagId",
                        column: x => x.PostTagId,
                        principalTable: "PostTags",
                        principalColumn: "PostTagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTagId",
                table: "Posts",
                column: "PostTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostTagId",
                table: "Tag",
                column: "PostTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostTags_PostTagId",
                table: "Posts",
                column: "PostTagId",
                principalTable: "PostTags",
                principalColumn: "PostTagId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostTags_PostTagId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTagId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostTagId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PostTags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostTags_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "PostTags",
                principalColumn: "PostTagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
