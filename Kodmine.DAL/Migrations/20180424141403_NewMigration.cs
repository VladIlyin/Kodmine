using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kodmine.DAL.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "PostTags",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostTags",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RubricId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rubrics",
                columns: table => new
                {
                    RubricId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrics", x => x.RubricId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RubricId",
                table: "Posts",
                column: "RubricId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Posts_Rubrics_RubricId",
            //    table: "Posts",
            //    column: "RubricId",
            //    principalTable: "Rubrics",
            //    principalColumn: "RubricId",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Rubrics_RubricId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropTable(
                name: "Rubrics");

            migrationBuilder.DropIndex(
                name: "IX_Posts_RubricId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RubricId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "PostTags",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostTags",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
