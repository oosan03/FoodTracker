using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddMealRecipeFkAndDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Recipe_RecipeId",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Recipes_RecipeId",
                table: "Meals",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Recipes_RecipeId",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Recipe_RecipeId",
                table: "Meals",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
