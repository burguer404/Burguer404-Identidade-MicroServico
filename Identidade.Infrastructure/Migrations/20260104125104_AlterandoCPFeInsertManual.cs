using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identidade.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoCPFeInsertManual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Clientes",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Cpf", "DataAtualizacao", "DataCriacao", "Email", "Nome", "PerfilClienteId", "Status" },
                values: new object[,]
                {
                    { 1, "111.111.111-11", null, new DateTime(2026, 1, 4, 9, 51, 3, 788, DateTimeKind.Local).AddTicks(7579), "11111111111@hotmail.com", "admin", 1, true },
                    { 2, "123.456.789-10", null, new DateTime(2026, 1, 4, 9, 51, 3, 788, DateTimeKind.Local).AddTicks(7581), "12345678910@hotmail.com", "Cliente", 1, true }
                });

            migrationBuilder.UpdateData(
                table: "PerfisCliente",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Administrador");

            migrationBuilder.UpdateData(
                table: "PerfisCliente",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descricao",
                value: "Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Clientes",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.UpdateData(
                table: "PerfisCliente",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Cliente Padrão");

            migrationBuilder.UpdateData(
                table: "PerfisCliente",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descricao",
                value: "Cliente VIP");
        }
    }
}
