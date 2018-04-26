using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kodmine.DAL.Migrations
{
    public partial class Dev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostTags_PostTagId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_PostTags_PostTagId",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostTagId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTagId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostTagId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "PostTagId",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "PostTags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "PostTags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");

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
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "PostTagId",
                table: "Tag",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostTagId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostTagId",
                table: "Tag",
                column: "PostTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTagId",
                table: "Posts",
                column: "PostTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostTags_PostTagId",
                table: "Posts",
                column: "PostTagId",
                principalTable: "PostTags",
                principalColumn: "PostTagId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_PostTags_PostTagId",
                table: "Tag",
                column: "PostTagId",
                principalTable: "PostTags",
                principalColumn: "PostTagId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
