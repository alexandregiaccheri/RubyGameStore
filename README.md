![Logodark](logo_dark.png#gh-dark-mode-only)
![Logolight](logo_light.png#gh-light-mode-only)

  Um site de comércio eletrônico construído com ASP.NET Core para a venda de games (inicialmente mídias físicas) inspirado em lojas como Steam, Nuuvem e Kabum!
  #### Link: https://www.rgs.xande.dev

  ## Principais recursos da aplicação

  - Cadastro/login com email/senha
  - Cadastro/login com redes sociais (Facebook, Google e Twitter)
  - Autenticação em duas etapas (2FA) opcional (código escrito ou QR)
  - Diferentes níveis de autorização (cliente, admin, etc)
  - Informações persistidas em banco de dados SQL
  - Busca por game com filtros (plataformas, gêneros, título, etc)
  - Carrinho de compras e sistema de cupons
  - Pagamentos e reembolsos com Stripe
  - Envio de emails automáticos e confirmação de email
  - Gerenciamento de pedidos (processamento, envio, cancelamento)
  - Gerenciamento de produtos (edição, adição e exclusão de produtos)

  ## Principais tecnologias e ferramentas

  Linguagens:
  - C#
  - JavaScript 

  Outros: 
  - HTML5 e CSS3
  - Bootstrap v5

  Framework ASP.NET Core (.NET 6):
  - Entity Framework Core
  - Identity Framework
  - ASP.NET Core MVC
  - Razor Pages
  - SQL Server

  Ferramentas:
  - Google Chrome DevTools
  - Microsoft SSMS
  - Visual Studio 2022

  Deploy:
  - GitHub Actions (CI/CD)
  - Microsoft Azure (App Service e SQL Server)
  ## Padrões/arquitetura de desenvolvimento

  * N-Tier Architecture
  * Repository
  * Unit of Work


  ## Uso local

  **Requisitos mínimos**
  * SGBD SQL
  * Windows 10 com .NET 6

  **Instruções e informações**

  Devido as integrações e complexidade do projeto, para que o mesmo funcione corretamente algumas configurações são necessárias:  
  * Criar um arquivo de configurações (appsettings.json) para o projeto "RubyGameStoreWeb"
  * Neste arquivo, definir uma conexão com um banco de dados (connection string)
    Então configurar ou desabilitar os seguintes serviços:
  * Autenticação com Google
  * Autenticação com Facebook
  * Autenticação com Twitter
  * EmailSender SMTP
  * Integração com Stripe

  Você pode usar o modelo a seguir para o arquivo de configuração (appsettings.json):
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
  ### Função de emails automáticos: 
  * **Configurar**: vá para o arquivo "EmailSender.cs" no projeto " RubyGameStore.Helper" e altere as informações do método "SendEmailAsync" de acordo com as configurações do provedor de email que decidiu usar.  
  * **Desabilitar**: crie uma implementação "falsa" para o método "SendEmailAsync", que não recebe parâmetro algum e simplesmente retorna "Task.CompletedTask".  
  ### Integração com Stripe:
  * **Configurar**: modifique as informações do modelo acima com suas chaves pessoais (você pode criar uma conta gratuitamente em https://stripe.com/en-br). Então altere o método **post** "CriarPedido", no arquivo CarrinhoController.cs (projeto RubyGameStoreWeb, em Areas/User/Controllers) de acordo com as configurações do seu ambiente local.
  * **Desabilitar**: remova o microsserviço "StripeConfiguration" da pipeline no arquivo "Program.cs", então modifique o método **post** "CriarPedido" no arquivo CarrinhoController.cs (projeto RubyGameStoreWeb, em Areas/User/Controllers), de modo que após a lógica de criação do pedido, sempre retorne:
  ```csharp
  return RedirectToAction("Confirmado", "Carrinho", new { id = CarrinhoVM.PedidoCabecalho.Id })
  ```
  ### Login com Redes Sociais:
  * **Configurar**: substitua os campos "sua-implementação" no arquivo de configuração json fornecido acima com suas informações pessoais dos portais de desenvolvedor de cada rede social que deseja implementar, então configure as URLs nos serviços configurados de acordo com o seu ambiente local.
  * **Desabilitar**: remova o método associado com a rede desejada do arquivo "Program.cs" ou remova completamente o método "AddAuthentication" para desativar todas as redes.
  ## Outros recursos utilizados

   - [Bootstrap Icons](https://icons.getbootstrap.com/)
   - [Bootswatch](https://bootswatch.com/)
   - [DataTables](https://datatables.net/)
   - [Google Fonts](https://fonts.google.com/)
   - [QRCode.js](https://github.com/davidshimjs/qrcodejs)
   - [SweetAlert2](https://sweetalert2.github.io/)
   - [TinyMCE](https://www.tiny.cloud/)
   - [Toastr](https://github.com/CodeSeven/toastr)
  ## Agradecimentos

  * [Edwilson Cruz (@EdwilsonCruz)](https://github.com/EdwilsonCruz) por ajudar com dúvidas/problemas de código
  * [Jean Muniz (@Shifungo)](https://github.com/Shifungo) por ajudar a testar a aplicação
  * [João Arthur (@bolotaime)](https://github.com/bolotaime) por ajudar com dicas e sugestões de UI/UX
  * [Lucas Santos (@makenchi)](https://github.com/makenchi) por ajudar com dúvidas/problemas de código
  * [Marco Giaccheri (@MarcoGiaccheri)](https://github.com/MarcoGiaccheri) por ajudar a testar testar a aplicação
   
  ## Feedback

  Qualquer dúvida, problema, sugestão ou qualquer outro assunto entre em contato através do email: "[contato@xande.dev](contato@xande.dev)"
