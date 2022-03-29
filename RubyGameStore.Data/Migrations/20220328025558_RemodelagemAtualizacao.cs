using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RubyGameStore.Data.Migrations
{
    public partial class RemodelagemAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuaUsuario",
                table: "UsuariosCNPJ",
                newName: "LogradouroEmpresa");

            migrationBuilder.RenameColumn(
                name: "NomeCNPJ",
                table: "UsuariosCNPJ",
                newName: "NomeEmpresa");

            migrationBuilder.RenameColumn(
                name: "EstadoUsuario",
                table: "UsuariosCNPJ",
                newName: "EstadoEmpresa");

            migrationBuilder.RenameColumn(
                name: "CidadeUsuario",
                table: "UsuariosCNPJ",
                newName: "CidadeEmpresa");

            migrationBuilder.RenameColumn(
                name: "CEPUsuario",
                table: "UsuariosCNPJ",
                newName: "CEPEmpresa");

            migrationBuilder.AddColumn<int>(
                name: "CNPJEmpresa",
                table: "UsuariosCNPJ",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TelefoneEmpresa",
                table: "UsuariosCNPJ",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJEmpresa",
                table: "UsuariosCNPJ");

            migrationBuilder.DropColumn(
                name: "TelefoneEmpresa",
                table: "UsuariosCNPJ");

            migrationBuilder.RenameColumn(
                name: "NomeEmpresa",
                table: "UsuariosCNPJ",
                newName: "NomeCNPJ");

            migrationBuilder.RenameColumn(
                name: "LogradouroEmpresa",
                table: "UsuariosCNPJ",
                newName: "RuaUsuario");

            migrationBuilder.RenameColumn(
                name: "EstadoEmpresa",
                table: "UsuariosCNPJ",
                newName: "EstadoUsuario");

            migrationBuilder.RenameColumn(
                name: "CidadeEmpresa",
                table: "UsuariosCNPJ",
                newName: "CidadeUsuario");

            migrationBuilder.RenameColumn(
                name: "CEPEmpresa",
                table: "UsuariosCNPJ",
                newName: "CEPUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
