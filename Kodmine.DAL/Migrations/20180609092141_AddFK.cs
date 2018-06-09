using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kodmine.DAL.Migrations
{
    public partial class AddFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Posts_Rubrics_RubricId",
            //    table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Rubric",
                table: "Posts",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "RubricId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Rubric",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Rubrics_RubricId",
                table: "Posts",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "RubricId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
