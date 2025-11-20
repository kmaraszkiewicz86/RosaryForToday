using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RosaryForToday.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RosaryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosaryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RosaryTypes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosaryDaySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RosaryTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosaryDaySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RosaryDaySchedules_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RosaryDaySchedules_RosaryTypes_RosaryTypeId",
                        column: x => x.RosaryTypeId,
                        principalTable: "RosaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RosaryReflections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RosaryTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosaryReflections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RosaryReflections_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RosaryReflections_RosaryTypes_RosaryTypeId",
                        column: x => x.RosaryTypeId,
                        principalTable: "RosaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "EN", "English" },
                    { 2, "PL", "Polski" }
                });

            migrationBuilder.InsertData(
                table: "RosaryDaySchedules",
                columns: new[] { "Id", "DayOfWeek", "LanguageId", "RosaryTypeId" },
                values: new object[,]
                {
                    { 2, 1, 1, 5 },
                    { 4, 2, 1, 6 },
                    { 6, 3, 1, 8 },
                    { 8, 4, 1, 7 },
                    { 10, 5, 1, 6 },
                    { 12, 6, 1, 5 },
                    { 14, 0, 1, 8 }
                });

            migrationBuilder.InsertData(
                table: "RosaryReflections",
                columns: new[] { "Id", "Content", "LanguageId", "RosaryTypeId", "Title" },
                values: new object[,]
                {
                    { 21, "n/a", 1, 5, "The Annunciation" },
                    { 22, "n/a", 1, 5, "The Visitation" },
                    { 23, "n/a", 1, 5, "The Nativity (Birth of Our Lord)" },
                    { 24, "n/a", 1, 5, "The Presentation" },
                    { 25, "n/a", 1, 5, "The Finding in the Temple" },
                    { 26, "n/a", 1, 6, "The Agony in the Garden" },
                    { 27, "n/a", 1, 6, "The Scourging at the Pillar" },
                    { 28, "n/a", 1, 6, "The Crowning with Thorns" },
                    { 29, "n/a", 1, 6, "The Carrying of the Cross" },
                    { 30, "n/a", 1, 6, "The Crucifixion and Death of Our Lord" },
                    { 31, "n/a", 1, 7, "The Baptism of Jesus in the Jordan" },
                    { 32, "n/a", 1, 7, "The Wedding at Cana" },
                    { 33, "n/a", 1, 7, "The Proclamation of the Kingdom of God" },
                    { 34, "n/a", 1, 7, "The Transfiguration" },
                    { 35, "n/a", 1, 7, "The Institution of the Eucharist" },
                    { 36, "n/a", 1, 8, "The Resurrection" },
                    { 37, "n/a", 1, 8, "The Ascension" },
                    { 38, "n/a", 1, 8, "The Descent of the Holy Spirit (Pentecost)" },
                    { 39, "n/a", 1, 8, "The Assumption of the Blessed Virgin Mary" },
                    { 40, "n/a", 1, 8, "The Coronation of the Blessed Virgin Mary as Queen of Heaven and Earth" }
                });

            migrationBuilder.InsertData(
                table: "RosaryTypes",
                columns: new[] { "Id", "LanguageId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Tajemnice Radosne" },
                    { 2, 2, "Tajemnice Bolesne" },
                    { 3, 2, "Tajemnice Światła" },
                    { 4, 2, "Tajemnice Chwalebne" }
                });

            migrationBuilder.InsertData(
                table: "RosaryDaySchedules",
                columns: new[] { "Id", "DayOfWeek", "LanguageId", "RosaryTypeId" },
                values: new object[,]
                {
                    { 1, 1, 2, 1 },
                    { 3, 2, 2, 2 },
                    { 5, 3, 2, 4 },
                    { 7, 4, 2, 3 },
                    { 9, 5, 2, 2 },
                    { 11, 6, 2, 1 },
                    { 13, 0, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "RosaryReflections",
                columns: new[] { "Id", "Content", "LanguageId", "RosaryTypeId", "Title" },
                values: new object[,]
                {
                    { 1, "n/a", 2, 1, "Zwiastowanie Pańskie" },
                    { 2, "n/a", 2, 1, "Nawiedzenie św. Elżbiety" },
                    { 3, "n/a", 2, 1, "Narodzenie Pana Jezusa" },
                    { 4, "n/a", 2, 1, "Ofiarowanie w świątyni" },
                    { 5, "n/a", 2, 1, "Odnalezienie Jezusa w świątyni" },
                    { 6, "n/a", 2, 2, "Modlitwa w Ogrójcu" },
                    { 7, "n/a", 2, 2, "Biczowanie Pana Jezusa" },
                    { 8, "n/a", 2, 2, "Cierniem Ukoronowanie" },
                    { 9, "n/a", 2, 2, "Droga Krzyżowa" },
                    { 10, "n/a", 2, 2, "Ukrzyżowanie i śmierć Pana Jezusa" },
                    { 11, "n/a", 2, 3, "Chrzest Jezusa w Jordanie" },
                    { 12, "n/a", 2, 3, "Objawienie się Jezusa w Kanie Galilejskiej" },
                    { 13, "n/a", 2, 3, "Głoszenie Królestwa Bożego" },
                    { 14, "n/a", 2, 3, "Przemienienie Pańskie" },
                    { 15, "n/a", 2, 3, "Ustanowienie Eucharystii" },
                    { 16, "n/a", 2, 4, "Zmartwychwstanie Jezusa" },
                    { 17, "n/a", 2, 4, "Wniebowstąpienie Pana" },
                    { 18, "n/a", 2, 4, "Zesłanie Ducha Świętego" },
                    { 19, "n/a", 2, 4, "Wniebowzięcie Najświętszej Maryi Panny" },
                    { 20, "n/a", 2, 4, "Ukoronowanie Maryi na Królową Nieba i Ziemi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RosaryDaySchedules_LanguageId",
                table: "RosaryDaySchedules",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RosaryDaySchedules_RosaryTypeId_DayOfWeek",
                table: "RosaryDaySchedules",
                columns: new[] { "RosaryTypeId", "DayOfWeek" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RosaryReflections_LanguageId",
                table: "RosaryReflections",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RosaryReflections_RosaryTypeId",
                table: "RosaryReflections",
                column: "RosaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RosaryTypes_LanguageId",
                table: "RosaryTypes",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RosaryDaySchedules");

            migrationBuilder.DropTable(
                name: "RosaryReflections");

            migrationBuilder.DropTable(
                name: "RosaryTypes");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
