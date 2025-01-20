## Sobre o projeto

Este projeto é uma solução completa para o gerenciamento de tarefas, composta por uma **API** desenvolvida em **.NET 9** o qual adota os princípios do **Domain-Driven Design (DDD)** e um aplicativo multiplataforma em **.NET MAUI**. O objetivo é oferecer uma ferramenta moderna, segura e colaborativa para criar, gerenciar e compartilhar tarefas de forma eficiente.

A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada, com os dados sendo armazenados de forma segura em um banco de dados **MySQL**. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil

O aplicativo foi desenvolvido com **.NET MAUI**, garantindo suporte a múltiplos dispositivos e sistemas operacionais, como Android, iOS, Windows e macOS. Isso proporciona uma experiência consistente e integrada, permitindo que os usuários acessem e gerenciem suas tarefas de qualquer lugar e a qualquer momento.

Dentre os pacotes NuGet utilizados na API, o **AutoMapper** desempenha um papel essencial ao simplificar o mapeamento entre objetos de domínio e objetos de requisição/resposta. Essa funcionalidade reduz drasticamente a necessidade de escrever código manual e repetitivo, promovendo maior produtividade e clareza no desenvolvimento. O **FluentValidation** também se destaca, permitindo a implementação de regras de validação de maneira simples, intuitiva e diretamente nas classes de requisição. Essa abordagem mantém o código mais limpo, organizado e facilita a manutenção, além de assegurar a consistência das validações em todo o projeto. O **EntityFramework** atua como o **ORM (Object-Relational Mapper)** principal, possibilitando interações eficientes com o banco de dados por meio de objetos .NET, eliminando a necessidade de lidar diretamente com consultas SQL e tornando o gerenciamento de dados mais intuitivo. Para o versionamento do banco de dados, o projeto utiliza o **FluentMigrator**, que oferece uma maneira estruturada de criar, alterar e gerenciar migrações de forma confiável e automatizada.

No aplicativo desenvolvido em **.NET MAUI**, a escolha dos pacotes NuGet foi direcionada para melhorar a experiência do usuário e otimizar o desenvolvimento. O **CommunityToolkit.Maui** foi incluído para estender as capacidades do framework, oferecendo controles adicionais e ferramentas que tornam a construção de interfaces mais dinâmica e responsiva. Para comunicação com a API, o **Refit** foi escolhido, permitindo a definição de interfaces fortemente tipadas que geram automaticamente as chamadas HTTP, simplificando a interação com os endpoints da API. Além disso, o **Newtonsoft.Json** é utilizado para desserializar dados no formato JSON, o que facilita o processamento de informações entre o aplicativo e a API.

A integração em tempo real, um dos diferenciais do aplicativo, é garantida pelo uso do **SignalR Client**, que permite uma comunicação bidirecional eficiente entre o aplicativo e o servidor ao adicionar ou colaboradores.

![hero-image]

### Features

- **Domain-Driven Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Criação e Gerenciamento de Tarefas**: Criação de tarefas com campos personalizados como título, descrição, prazo e anexo de arquivos. Edição e exclusão de tarefas diretamente no aplicativo.
- **Compartilhamento de Tarefas**: Convidar pessoas para colaborar em tarefas.
- **Login com Google**: Autenticação rápida e segura por meio de Google. Experiência simplificada para novos usuários sem a necessidade de criar uma conta do zero e velocidade para o Login.
- **Integração com Azure**: Para armazenamento seguro e escalável de arquivos e para a implementação de filas de mensagens no Azure, possibilitando a comunicação assíncrona e o processamento de tarefas em segundo plano.

### Construído com

![badge-dot-net] ![badge-dot-net-maui] ![badge-swagger] ![badge-windows] ![badge-mac] ![badge-android] ![badge-ios] ![badge-visual-studio] ![badge-rider] ![badge-xcode] ![badge-mysql] ![badge-figma]


## Requisitos

* Visual Studio versão 2022+ ou Rider.
* Windows 10+ ou MacOS com [.NET SDK][dot-net-sdk] e [workload .NET MAUI][maui-workload] instalados.
* MySql Server.
* Dispositivo Android (para iOS vamos usar emulador visto que é pago para usar um iPhone físico)


<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/9.0
[maui-workload]: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-install

<!-- Images -->
[hero-image]: images/heroimage.png

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET%209.0-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-dot-net-maui]: https://img.shields.io/badge/.NET%20MAUI-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge

[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-mac]: https://img.shields.io/badge/mac%20os-000000?style=for-the-badge&logo=apple&logoColor=white

[badge-android]: https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white
[badge-ios]: https://img.shields.io/badge/iOS-000000?style=for-the-badge&logo=ios&logoColor=white

[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-xcode]: https://img.shields.io/badge/Xcode-007ACC?style=for-the-badge&logo=Xcode&logoColor=white

[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
[badge-rider]: https://img.shields.io/badge/Rider-000000?style=for-the-badge&logo=Rider&logoColor=white

[badge-figma]: https://img.shields.io/badge/Figma-F24E1E?style=for-the-badge&logo=figma&logoColor=white