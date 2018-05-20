using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kodmine.DAL.Migrations
{
    public partial class upd21052018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_RubricId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RubricId",
                table: "Posts",
                column: "RubricId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_RubricId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RubricId",
                table: "Posts",
                column: "RubricId",
                unique: true);
        }
    }
}
