using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsidersTestTask.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supply = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MarketCapUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VolumeUsd24Hr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangePercent24Hr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vwap24Hr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Explorer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSurename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserCryptos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CryptoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCryptos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCryptos_CryptoInfos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "CryptoInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCryptos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCryptos_CryptoId",
                table: "UserCryptos",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCryptos_UserId",
                table: "UserCryptos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCryptos");

            migrationBuilder.DropTable(
                name: "CryptoInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
