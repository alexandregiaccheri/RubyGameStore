using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RubyGameStore.Data.Data;
using RubyGameStore.Helper.StaticNames;
using RubyGameStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RubyGameStoreDbContext _dbContext;

        public DbInitializer(UserManager<IdentityUser> userManager,
                             RoleManager<IdentityRole> roleManager,
                             RubyGameStoreDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            // Verifica se existem Migrations pendentes
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex) { };
            
            // Criar Roles caso ainda não tenham sido criadas
            if (!_roleManager.RoleExistsAsync(Autorizacao.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Cliente)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Empresa)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Funcionario)).GetAwaiter().GetResult();

                // Cria conta de ADM

                _userManager.CreateAsync(new Usuario
                {
                    UserName = "alexandre@xande.dev",
                    Email = "alexandre@xande.dev",
                    NomeUsuario = "Alexandre",
                    SobrenomeUsuario = "Giaccheri"
                }, "Admin1234*").GetAwaiter().GetResult();

                Usuario usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Email == "alexandre@xande.dev");

                _userManager.AddToRoleAsync(usuario, Autorizacao.Admin).GetAwaiter().GetResult();
            }
            
            return;

        }
    }
}
