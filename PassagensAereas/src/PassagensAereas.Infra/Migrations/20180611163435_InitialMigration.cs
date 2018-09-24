using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassagensAereas.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassesVoo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    ValorFixo = table.Column<double>(nullable: false),
                    ValorMilha = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesVoo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    LatitudeLocal = table.Column<double>(nullable: false),
                    LongitudeLocal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrimeiroNome = table.Column<string>(maxLength: 100, nullable: false),
                    UltimoNome = table.Column<string>(maxLength: 100, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(maxLength: 10, nullable: false),
                    Login = table.Column<string>(maxLength: 128, nullable: false),
                    Senha = table.Column<string>(maxLength: 128, nullable: false),
                    Admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trechos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Distancia = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    LocalAId = table.Column<int>(nullable: true),
                    LocalBId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trechos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trechos_Locais_LocalAId",
                        column: x => x.LocalAId,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trechos_Locais_LocalBId",
                        column: x => x.LocalBId,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrechoId = table.Column<int>(nullable: true),
                    ClasseVooId = table.Column<int>(nullable: true),
                    ValorTotal = table.Column<double>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_ClassesVoo_ClasseVooId",
                        column: x => x.ClasseVooId,
                        principalTable: "ClassesVoo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Trechos_TrechoId",
                        column: x => x.TrechoId,
                        principalTable: "Trechos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opcionais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    ReservaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opcionais_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservaOpcional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReservaId = table.Column<int>(nullable: true),
                    OpcionalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaOpcional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaOpcional_Opcionais_OpcionalId",
                        column: x => x.OpcionalId,
                        principalTable: "Opcionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaOpcional_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opcionais_ReservaId",
                table: "Opcionais",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaOpcional_OpcionalId",
                table: "ReservaOpcional",
                column: "OpcionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaOpcional_ReservaId",
                table: "ReservaOpcional",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClasseVooId",
                table: "Reservas",
                column: "ClasseVooId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_TrechoId",
                table: "Reservas",
                column: "TrechoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trechos_LocalAId",
                table: "Trechos",
                column: "LocalAId");

            migrationBuilder.CreateIndex(
                name: "IX_Trechos_LocalBId",
                table: "Trechos",
                column: "LocalBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaOpcional");

            migrationBuilder.DropTable(
                name: "Opcionais");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "ClassesVoo");

            migrationBuilder.DropTable(
                name: "Trechos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Locais");
        }
    }
}
