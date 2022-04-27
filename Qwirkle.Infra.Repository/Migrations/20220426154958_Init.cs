using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qwirkle.Infra.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Help = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastPlayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameOver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shape = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDaoUserDao",
                columns: table => new
                {
                    BookmarkedById = table.Column<int>(type: "int", nullable: false),
                    BookmarkedOpponentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDaoUserDao", x => new { x.BookmarkedById, x.BookmarkedOpponentsId });
                    table.ForeignKey(
                        name: "FK_UserDaoUserDao_AspNetUsers_BookmarkedById",
                        column: x => x.BookmarkedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDaoUserDao_AspNetUsers_BookmarkedOpponentsId",
                        column: x => x.BookmarkedOpponentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    LastTurnPoints = table.Column<byte>(type: "tinyint", nullable: false),
                    GameTurn = table.Column<bool>(type: "bit", nullable: false),
                    GamePosition = table.Column<byte>(type: "tinyint", nullable: false),
                    LastTurnSkipped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TileOnBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TileId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileOnBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TileOnBag_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TileOnBag_Tile_TileId",
                        column: x => x.TileId,
                        principalTable: "Tile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TileOnBoard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TileId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PositionX = table.Column<short>(type: "smallint", nullable: false),
                    PositionY = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileOnBoard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TileOnBoard_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TileOnBoard_Tile_TileId",
                        column: x => x.TileId,
                        principalTable: "Tile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TileOnPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TileId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    RackPosition = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileOnPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TileOnPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TileOnPlayer_Tile_TileId",
                        column: x => x.TileId,
                        principalTable: "Tile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "5bdc0a4d-900b-4dfd-801c-afafc1132457", "Bot", "BOT" },
                    { 2, "c3029150-bb16-4b67-8c99-f941f494ade1", "Admin", "ADMIN" },
                    { 3, "9019946d-650e-4b41-9df8-b27610caacd6", "Guest", "GUEST" },
                    { 4, "2a8c07b3-ea6b-42de-8cb3-02bd05638284", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "GamesPlayed", "GamesWon", "Help", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "Pseudo" },
                values: new object[,]
                {
                    { 1, 0, "917c8e70-ee8e-458d-8013-510628a40c66", "bot1@bot", false, null, 0, 0, 0, null, false, null, null, "BOT1", null, null, false, 0, null, false, "bot1" },
                    { 2, 0, "0bd428e7-213b-4871-bc51-4070ba1a19ff", "bot2@bot", false, null, 0, 0, 0, null, false, null, null, "BOT2", null, null, false, 0, null, false, "bot2" },
                    { 3, 0, "65665a48-93e7-467f-9b8c-207b4ea82e82", "bot3@bot", false, null, 0, 0, 0, null, false, null, null, "BOT3", null, null, false, 0, null, false, "bot3" },
                    { 4, 0, "56c1dc8b-f87d-4334-a8b6-3ebb381de554", "bot4@bot", false, null, 0, 0, 0, null, false, null, null, "BOT4", null, null, false, 0, null, false, "bot4" },
                    { 5, 0, "7e740ecb-0971-483d-86c9-c8dfc3ac4a1e", "thomas@newtomsoft.fr", false, "Thomas", 0, 0, 0, "Vuille", false, null, "THOMAS@NEWTOMSOFT.FR", "TOM", "AQAAAAEAACcQAAAAED29kKSVgjTdA6s6pXQ0a+7iy9MJ5Y1byxFl2MWZnX4WE6lw1SsR9FGeGypraM3G+g==", null, false, 0, "3250B471DF55411F984E2A529912AAA9", false, "Tom" },
                    { 6, 0, "9890ecac-716e-4143-9954-109b6bc3b848", "jc@jc.fr", false, "Jean Charles", 0, 0, 0, "Gouleau", false, null, "JC@JC.FR", "JC", "AQAAAAEAACcQAAAAEJOr0iSf9bL59UJqwWyCpcjdampHsvulqOZ/NTApuuwLJsc1Sf9xRquQWPIz2S8rUQ==", null, false, 0, "66953B52AC7F4FDF88696C39FA475BBB", false, "JC" }
                });

            migrationBuilder.InsertData(
                table: "Tile",
                columns: new[] { "Id", "Color", "Shape" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 1, 6 },
                    { 7, 2, 1 },
                    { 8, 2, 2 },
                    { 9, 2, 3 },
                    { 10, 2, 4 },
                    { 11, 2, 5 },
                    { 12, 2, 6 },
                    { 13, 3, 1 },
                    { 14, 3, 2 },
                    { 15, 3, 3 },
                    { 16, 3, 4 },
                    { 17, 3, 5 },
                    { 18, 3, 6 },
                    { 19, 4, 1 },
                    { 20, 4, 2 },
                    { 21, 4, 3 },
                    { 22, 4, 4 },
                    { 23, 4, 5 },
                    { 24, 4, 6 },
                    { 25, 5, 1 },
                    { 26, 5, 2 },
                    { 27, 5, 3 },
                    { 28, 5, 4 },
                    { 29, 5, 5 },
                    { 30, 5, 6 },
                    { 31, 6, 1 },
                    { 32, 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tile",
                columns: new[] { "Id", "Color", "Shape" },
                values: new object[,]
                {
                    { 33, 6, 3 },
                    { 34, 6, 4 },
                    { 35, 6, 5 },
                    { 36, 6, 6 },
                    { 37, 1, 1 },
                    { 38, 1, 2 },
                    { 39, 1, 3 },
                    { 40, 1, 4 },
                    { 41, 1, 5 },
                    { 42, 1, 6 },
                    { 43, 2, 1 },
                    { 44, 2, 2 },
                    { 45, 2, 3 },
                    { 46, 2, 4 },
                    { 47, 2, 5 },
                    { 48, 2, 6 },
                    { 49, 3, 1 },
                    { 50, 3, 2 },
                    { 51, 3, 3 },
                    { 52, 3, 4 },
                    { 53, 3, 5 },
                    { 54, 3, 6 },
                    { 55, 4, 1 },
                    { 56, 4, 2 },
                    { 57, 4, 3 },
                    { 58, 4, 4 },
                    { 59, 4, 5 },
                    { 60, 4, 6 },
                    { 61, 5, 1 },
                    { 62, 5, 2 },
                    { 63, 5, 3 },
                    { 64, 5, 4 },
                    { 65, 5, 5 },
                    { 66, 5, 6 },
                    { 67, 6, 1 },
                    { 68, 6, 2 },
                    { 69, 6, 3 },
                    { 70, 6, 4 },
                    { 71, 6, 5 },
                    { 72, 6, 6 },
                    { 73, 1, 1 },
                    { 74, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tile",
                columns: new[] { "Id", "Color", "Shape" },
                values: new object[,]
                {
                    { 75, 1, 3 },
                    { 76, 1, 4 },
                    { 77, 1, 5 },
                    { 78, 1, 6 },
                    { 79, 2, 1 },
                    { 80, 2, 2 },
                    { 81, 2, 3 },
                    { 82, 2, 4 },
                    { 83, 2, 5 },
                    { 84, 2, 6 },
                    { 85, 3, 1 },
                    { 86, 3, 2 },
                    { 87, 3, 3 },
                    { 88, 3, 4 },
                    { 89, 3, 5 },
                    { 90, 3, 6 },
                    { 91, 4, 1 },
                    { 92, 4, 2 },
                    { 93, 4, 3 },
                    { 94, 4, 4 },
                    { 95, 4, 5 },
                    { 96, 4, 6 },
                    { 97, 5, 1 },
                    { 98, 5, 2 },
                    { 99, 5, 3 },
                    { 100, 5, 4 },
                    { 101, 5, 5 },
                    { 102, 5, 6 },
                    { 103, 6, 1 },
                    { 104, 6, 2 },
                    { 105, 6, 3 },
                    { 106, 6, 4 },
                    { 107, 6, 5 },
                    { 108, 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Pseudo",
                table: "AspNetUsers",
                column: "Pseudo",
                unique: true,
                filter: "[Pseudo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnBag_GameId",
                table: "TileOnBag",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnBag_TileId",
                table: "TileOnBag",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnBoard_GameId",
                table: "TileOnBoard",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnBoard_TileId",
                table: "TileOnBoard",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnPlayer_PlayerId",
                table: "TileOnPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TileOnPlayer_TileId",
                table: "TileOnPlayer",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDaoUserDao_BookmarkedOpponentsId",
                table: "UserDaoUserDao",
                column: "BookmarkedOpponentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TileOnBag");

            migrationBuilder.DropTable(
                name: "TileOnBoard");

            migrationBuilder.DropTable(
                name: "TileOnPlayer");

            migrationBuilder.DropTable(
                name: "UserDaoUserDao");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Tile");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
