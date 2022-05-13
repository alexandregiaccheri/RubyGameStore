![Logodark](logo_dark.png#gh-dark-mode-only)
![Logolight](logo_light.png#gh-light-mode-only)

  Um site de comércio eletrônico construído com ASP.NET Core para a venda de games (inicialmente mídias físicas) inspirado em lojas como Steam, Nuuvem e Kabum!
  #### Link: https://www.rgs.xande.dev

  ## Principais recursos da aplicação

  - Autenticação em duas etapas (2FA) opcional (código escrito ou QR)
  - Busca por game com filtros (plataformas, gêneros, título, etc)
  - Cadastro/login com email/senha
  - Cadastro/login com redes sociais (Facebook, Google e Twitter)
  - Carrinho de compras e sistema de cupons
  - Diferentes níveis de autorização (cliente, admin, etc)
  - Envio de emails automáticos e confirmação de email
  - Gerenciamento de pedidos (processamento, envio, cancelamento)
  - Gerenciamento de produtos (edição, adição e exclusão de produtos)
  - Informações persistidas em banco de dados SQL    
  - Pagamentos e reembolsos com Stripe    

  <hr>

  ## Principais tecnologias e ferramentas

  #### Elementares:
  - C#, JavaScript
  - HTML, CSS e Bootstrap v5

  #### Framework ASP.NET Core (.NET 6):
  - Entity Framework Core
  - Identity Framework
  - ASP.NET Core MVC
  - Razor Pages
  - SQL Server

  #### Ferramentas:
  - Google Chrome DevTools
  - Microsoft SSMS
  - Visual Studio 2022

  #### Deploy:
  - GitHub Actions (CI/CD)
  - Microsoft Azure (App Service e SQL Server)

  #### Padrões/arquitetura de desenvolvimento

  * N-Tier Architecture
  * Repository Pattern
  * Unit of Work Pattern

  <hr>

  ## Instalação/Uso local

  **Requisitos mínimos**
  * SGBD Relacional
    * É possível utilizar qualquer banco de dados relacional suportado pelo EF Core 6, porém 
  * Windows 10 com .NET 6
    * Ajustes adicionais são necessários para sistemas não baseados em Windows

  **Instruções e informações**

  Devido as integrações e complexidade do projeto, para que o mesmo funcione corretamente algumas configurações serão necessárias:  

  **I - Configuração com Banco de Dados**:  

  * Criar um arquivo de configurações (appsettings.json) para o projeto "RubyGameStoreWeb"  
    (Você pode utilizar o modelo abaixo)  
  * Neste arquivo, defina uma conexão com um banco de dados (connection string)
    

  **II - Configure ou Desabilite**:  

    1 - Autenticação com redes sociais  
    2 - Emails automáticos  
    3 - Integração com Stripe  

  ### 1 - Redes Sociais:
  * **Configurar**: substitua os campos "sua-implementação" no arquivo de configuração (appsettings.json) com suas chaves pessoais (dos portais de desenvolvedor de cada rede social que deseja implementar), então configure as URLs nos serviços configurados de acordo com o seu ambiente local.
  * **Desabilitar**: remova o método associado com a rede desejada do arquivo "Program.cs" ou remova completamente o método "AddAuthentication" para desativar todas as redes.
  ```csharp
  // Método responsável por adicionar as autenticações com redes sociais no arquivo "Program.cs"
  builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("FacebookLogin:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("FacebookLogin:AppSecret");
    }).AddTwitter(options =>
    {
        options.ConsumerKey = builder.Configuration.GetValue<string>("TwitterLogin:ClientId");
        options.ConsumerSecret = builder.Configuration.GetValue<string>("TwitterLogin:ClientSecret");
    }).AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("GoogleLogin:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("GoogleLogin:ClientSecret");
    });
  ```

  ### 2 - Emails automáticos:  
  * **Configurar**: forneça o email e senha desejados no arquivo de configuração (appsettings.json), então vá para o arquivo "EmailSender.cs" no projeto " RubyGameStore.Helper" e altere as informações do método "SendEmailAsync" de acordo com as configurações do provedor de email que decidiu usar.
  ```csharp
  // RubyGameStore.Helper/EmailSender.cs  
  public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Email, Password)
            };
            using (var message = new MailMessage(Email, email)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
            return Task.CompletedTask;
        }
  ```
  * **Desabilitar**: crie uma implementação "falsa" para o método "SendEmailAsync", que não recebe parâmetro algum e simplesmente retorna "Task.CompletedTask".  
  ```csharp
  // Exemplo de implementação "falsa"
  public Task SendEmailAsync()
        {            
            return Task.CompletedTask;
        }
  ```
  ### 3 - Integração com Stripe:  
  * **Configurar**: modifique as informações do arquivo de configuração (appsettings.json) com suas chaves pessoais (você pode criar uma conta gratuitamente em https://stripe.com/en-br). Então altere a variável "domain" do método **post** "CriarPedido", no arquivo RubyGameStoreWeb/Areas/User/Controllers/CarrinhoController.cs (linha 344) de acordo com as configurações do seu ambiente local.
  * **Desabilitar**: remova o microsserviço "StripeConfiguration" da pipeline no arquivo "Program.cs", então modifique o método **post** "CriarPedido" no arquivo RubyGameStoreWeb/Areas/User/Controllers/CarrinhoController.cs, para que o mesmo sempre retorne para a página de confirmação/pedido realizado com sucesso após a conclusão da lógica de criação do pedido. Em caso de dúvidas, siga o exemplo abaixo:
  ```csharp
  if (User.IsInRole("Empresa"))
            {
                return RedirectToAction("Confirmado", "Carrinho", new { id = CarrinhoVM.PedidoCabecalho.Id });
            }
  // Altere a condição do if (linha 336) para:
  if (true) 
            {
                return RedirectToAction("Confirmado", "Carrinho", new { id = CarrinhoVM.PedidoCabecalho.Id });
            }
  ```


  ### Modelo "appsettings.json":
  ```json
  {
      "ConnectionStrings": {
          "DefaultConnection": "connection-string"
      },
      "EmailSender": {
          "Email": "sua-implementação",
          "Password": "sua-implementação"
      },
      "FacebookLogin": {
          "AppId": "sua-implementação",
          "AppSecret": "sua-implementação"
      },
      "GoogleLogin": {
          "ClientId": "sua-implementação",
          "ClientSecret": "sua-implementação"
      }
      "TwitterLogin": {
          "ClientId": "sua-implementação",
          "ClientSecret": "sua-implementação"
      },
      "StripeKeys": {
          "SecretKey": "sua-implementação",
          "PublishableKey": "sua-implementação"
      }
  }
  ```

  <hr>

  ## Instruções de primeiro uso

  Uma vez que a aplicação iniciar com sucesso e a conexão com o banco de dados for estabelecida, o método Initialize() (arquivo RubyGameStore.Data/DbInitializer/DbInitializer.cs) vai rodar e realizar as seguintes tarefas para que o site possa funcionar:  
  1) Atualizar o banco de dados, aplicando as migrations necessárias:
```csharp
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
            
            [...]
            
            return;
        }
```
  2) Criar as (quatro) roles atualmente implementadas no site:
```csharp
public void Initialize()
        {
            [...]
            
            // Criar Roles caso ainda não tenham sido criadas
            if (!_roleManager.RoleExistsAsync(Autorizacao.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Cliente)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Empresa)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Autorizacao.Funcionario)).GetAwaiter().GetResult();
                
                [...]
            }
            
            return;
        }
```
  3) Criar a conta padrão de Admin
```csharp
public void Initialize()
        {
            [...]
            
            // Criar Roles caso ainda não tenham sido criadas
            if (!_roleManager.RoleExistsAsync(Autorizacao.Admin).GetAwaiter().GetResult())
            {
                [...]
                
                // Cria conta de ADM

                _userManager.CreateAsync(new Usuario
                {
                    UserName = "admin@ruby.com",
                    Email = "admin@ruby.com",
                    NomeUsuario = "Ruby",
                    SobrenomeUsuario = "Admin"
                }, "Admin1234*").GetAwaiter().GetResult();

                Usuario usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Email == "admin@ruby.com");

                _userManager.AddToRoleAsync(usuario, Autorizacao.Admin).GetAwaiter().GetResult();
            }
            
            return;
        }
```

<br>

  Para começar a cadastrar produtos e gerenciar a loja, faça login com a conta de administrador criada acima. Você pode alterar as informações dessa conta ou criar outras contas de administrador a partir do menu exclusivo "Admin" na barra de navegação do site (quando logado como admin).

  <hr>

  ## Outros recursos utilizados

   - [Bootstrap Icons](https://icons.getbootstrap.com/)
   - [Bootswatch](https://bootswatch.com/)
   - [DataTables](https://datatables.net/)
   - [Google Fonts](https://fonts.google.com/)
   - [QRCode.js](https://github.com/davidshimjs/qrcodejs)
   - [SweetAlert2](https://sweetalert2.github.io/)
   - [TinyMCE](https://www.tiny.cloud/)
   - [Toastr](https://github.com/CodeSeven/toastr)

  <hr>

  ## Agradecimentos

  * [Bhrugen Patel (@bhrugen)](https://github.com/bhrugen) @ [DotNetMastery](https://www.dotnetmastery.com/) pelo incrível [curso de ASP.NET Core MVC](https://www.dotnetmastery.com/Home/Details?courseId=9) e demais recursos
  * [Edwilson Cruz (@EdwilsonCruz)](https://github.com/EdwilsonCruz) por ajudar com dúvidas/problemas de código
  * [Jean Muniz (@Shifungo)](https://github.com/Shifungo) por ajudar a testar a aplicação
  * [João Arthur (@bolotaime)](https://github.com/bolotaime) por ajudar com dicas e sugestões de UI/UX
  * [Lucas Santos (@makenchi)](https://github.com/makenchi) por ajudar com dúvidas/problemas de código
  * [Marco Giaccheri (@MarcoGiaccheri)](https://github.com/MarcoGiaccheri) por ajudar a testar a aplicação

  <hr>

  ## Feedback

  Qualquer dúvida, problema, sugestão ou qualquer outro assunto entre em contato através do email: "[contato@xande.dev](contato@xande.dev)"
