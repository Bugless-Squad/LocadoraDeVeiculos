using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCliente",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoPessoa = table.Column<string>(type: "varchar(200)", nullable: false),
                    telefone = table.Column<string>(type: "varchar(200)", nullable: false),
                    estado = table.Column<string>(type: "varchar(200)", nullable: false),
                    cidade = table.Column<string>(type: "varchar(200)", nullable: false),
                    bairro = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(200)", nullable: false),
                    cnpj = table.Column<string>(type: "varchar(200)", nullable: true),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    cpf = table.Column<string>(type: "varchar(200)", nullable: true),
                    rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    cep = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TBFuncionario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    dataAdmissao = table.Column<DateTime>(type: "datetime", nullable: false),
                    salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFuncionario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TBGrupoAutomovel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBGrupoAutomovel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TBParceiro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBParceiro", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TBCondutor",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clienteid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    validadeCnh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    telefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(200)", nullable: false),
                    cpf = table.Column<string>(type: "varchar(200)", nullable: false),
                    cnh = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCondutor", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBCondutor_TBCliente",
                        column: x => x.clienteid,
                        principalTable: "TBCliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBAutomovel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoCombustivel = table.Column<int>(type: "int", nullable: false),
                    capacidadeTanqueLitros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    grupoAutomovelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quilometragem = table.Column<double>(type: "float", nullable: false),
                    modelo = table.Column<string>(type: "varchar(100)", nullable: false),
                    marca = table.Column<string>(type: "varchar(100)", nullable: false),
                    foto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    cor = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAutomovel", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBAutomovel_TBGrupoAutomovel",
                        column: x => x.grupoAutomovelid,
                        principalTable: "TBGrupoAutomovel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBPlanoCobranca",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    grupoAutomovelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoPlano = table.Column<int>(type: "int", nullable: false),
                    precoDiaria = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precoPorKm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    precoPorKmExtrapolado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    kmDisponiveis = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPlanoCobranca", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBPlanoCobranca_TBGrupoAutomovel",
                        column: x => x.grupoAutomovelid,
                        principalTable: "TBGrupoAutomovel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBCupom",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parceiroid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCupom", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBCupom_TBParceiro",
                        column: x => x.parceiroid,
                        principalTable: "TBParceiro",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBAluguel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dataPrevistaDevolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    grupoAutomovelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    valorTotalPrevisto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nivelTanqueLitros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    funcionarioid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    valorTotalFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataLocacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    kmPercorrido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    automovelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kmInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    condutorid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kmFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    clienteid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cupomid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAluguel", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBAutomovel",
                        column: x => x.automovelid,
                        principalTable: "TBAutomovel",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBCliente",
                        column: x => x.clienteid,
                        principalTable: "TBCliente",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBCondutor",
                        column: x => x.condutorid,
                        principalTable: "TBCondutor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBCupom",
                        column: x => x.cupomid,
                        principalTable: "TBCupom",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBFuncionario",
                        column: x => x.funcionarioid,
                        principalTable: "TBFuncionario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBGrupoAutomovel",
                        column: x => x.grupoAutomovelid,
                        principalTable: "TBGrupoAutomovel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBTaxaEServico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipoDoCusto = table.Column<int>(type: "int", nullable: false),
                    Aluguelid1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Aluguelid2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTaxaEServico", x => x.id);
                    table.ForeignKey(
                        name: "FK_TBTaxaEServico_TBAluguel_Aluguelid1",
                        column: x => x.Aluguelid1,
                        principalTable: "TBAluguel",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TBTaxaEServico_TBAluguel_Aluguelid2",
                        column: x => x.Aluguelid2,
                        principalTable: "TBAluguel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TBAluguel_TBTaxaEServico",
                columns: table => new
                {
                    Aluguelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    taxasid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAluguel_TBTaxaEServico", x => new { x.Aluguelid, x.taxasid });
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBTaxaEServico_TBAluguel_Aluguelid",
                        column: x => x.Aluguelid,
                        principalTable: "TBAluguel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBAluguel_TBTaxaEServico_TBTaxaEServico_taxasid",
                        column: x => x.taxasid,
                        principalTable: "TBTaxaEServico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_automovelid",
                table: "TBAluguel",
                column: "automovelid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_clienteid",
                table: "TBAluguel",
                column: "clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_condutorid",
                table: "TBAluguel",
                column: "condutorid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_cupomid",
                table: "TBAluguel",
                column: "cupomid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_funcionarioid",
                table: "TBAluguel",
                column: "funcionarioid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_grupoAutomovelid",
                table: "TBAluguel",
                column: "grupoAutomovelid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAluguel_TBTaxaEServico_taxasid",
                table: "TBAluguel_TBTaxaEServico",
                column: "taxasid");

            migrationBuilder.CreateIndex(
                name: "IX_TBAutomovel_grupoAutomovelid",
                table: "TBAutomovel",
                column: "grupoAutomovelid");

            migrationBuilder.CreateIndex(
                name: "IX_TBCondutor_clienteid",
                table: "TBCondutor",
                column: "clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_TBCupom_parceiroid",
                table: "TBCupom",
                column: "parceiroid");

            migrationBuilder.CreateIndex(
                name: "IX_TBPlanoCobranca_grupoAutomovelid",
                table: "TBPlanoCobranca",
                column: "grupoAutomovelid");

            migrationBuilder.CreateIndex(
                name: "IX_TBTaxaEServico_Aluguelid1",
                table: "TBTaxaEServico",
                column: "Aluguelid1");

            migrationBuilder.CreateIndex(
                name: "IX_TBTaxaEServico_Aluguelid2",
                table: "TBTaxaEServico",
                column: "Aluguelid2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAluguel_TBTaxaEServico");

            migrationBuilder.DropTable(
                name: "TBPlanoCobranca");

            migrationBuilder.DropTable(
                name: "TBTaxaEServico");

            migrationBuilder.DropTable(
                name: "TBAluguel");

            migrationBuilder.DropTable(
                name: "TBAutomovel");

            migrationBuilder.DropTable(
                name: "TBCondutor");

            migrationBuilder.DropTable(
                name: "TBCupom");

            migrationBuilder.DropTable(
                name: "TBFuncionario");

            migrationBuilder.DropTable(
                name: "TBGrupoAutomovel");

            migrationBuilder.DropTable(
                name: "TBCliente");

            migrationBuilder.DropTable(
                name: "TBParceiro");
        }
    }
}
