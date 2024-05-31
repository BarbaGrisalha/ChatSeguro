# Chat Seguro

O Chat Seguro é uma aplicação de mensagens criptografadas desenvolvida em C# usando o Visual Studio.

## Funcionalidades

1. **Troca de Mensagens Criptografadas**: Os usuários podem enviar e receber mensagens de texto de forma segura, garantindo que apenas o remetente e o destinatário possam ler o conteúdo das mensagens.

2. **Autenticação de Usuários**: Os usuários devem autenticar-se no servidor antes de poderem enviar mensagens. Isso garante que apenas usuários autorizados tenham acesso ao chat.

3. **Envio de Chave Pública**: Os clientes enviam suas chaves públicas para o servidor, permitindo que o servidor crie uma chave simétrica para comunicação segura entre cliente e servidor.

4. **Validação de Mensagens**: Todas as mensagens trocadas são validadas usando assinaturas digitais, garantindo a integridade e autenticidade das mensagens enviadas.

5. **Interface Gráfica (UI)**: O cliente possui uma interface gráfica que facilita a interação com o chat, permitindo o envio e recebimento de mensagens de forma intuitiva.

6. **Armazenamento de Chaves Públicas**: O servidor armazena as chaves públicas dos clientes para autenticar usuários e garantir a segurança das comunicações.


## Instalação e Configuração

### Pré-requisitos
- [.NET Framework](https://dotnet.microsoft.com/download) instalado.
- [Visual Studio](https://visualstudio.microsoft.com/) (ou outro editor de código) instalado.

### Passos de Instalação
1. Clone este repositório para o seu ambiente local.
2. Abra o projeto no Visual Studio.
3. Compile o projeto para gerar o executável.
4. Execute o executável para iniciar o chat seguro.

## Uso

O chat seguro proporciona uma plataforma confiável e segura para comunicação entre usuários. Abaixo estão os passos básicos para utilizar o chat:

1. **Autenticação**: O usuário inicia o aplicativo e é solicitado a autenticar-se no servidor fornecendo suas credenciais de acesso.

2. **Envio de Mensagens**: Após a autenticação bem-sucedida, o usuário pode enviar mensagens para outros usuários conectados ao chat. As mensagens são criptografadas de ponta a ponta para garantir a privacidade e segurança.

3. **Recepção de Mensagens**: O usuário pode visualizar as mensagens recebidas de outros usuários na interface do chat. Todas as mensagens são decodificadas e exibidas de forma legível apenas para o destinatário correto.

4. **Segurança**: Todas as interações no chat são protegidas por criptografia, incluindo a troca de mensagens e autenticação de usuários, garantindo que as comunicações permaneçam confidenciais e seguras.

5. **Gerenciamento de Chaves Públicas**: O chat automatiza o processo de troca e armazenamento de chaves públicas, garantindo que as comunicações sejam autenticadas e seguras.

6. **Encerramento da Sessão**: Ao finalizar a sessão no chat, o usuário pode fazer logout para encerrar a conexão segura com o servidor e proteger suas informações.

Com o chat seguro, os usuários podem comunicar-se livremente, sabendo que suas mensagens estão protegidas contra acesso não autorizado e garantindo a confidencialidade de suas conversas.

## Autores
- [Lucas Siqueira](https://github.com/lucassiqueiraa)
- [Altamir Junior](https://github.com/BarbaGrisalha)
- [João Lains](https://github.com/JoaoLains)
- [Dinis Ruivo](https://github.com/Dinisruivo03)
  
## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para clonar o repositório, fazer melhorias e enviar uma solicitação de pull.

## Licença

Este projeto é licenciado sob a [Licença MIT](LICENSE).

## Contato

Para mais informações, entre em contato pelo email: lucassiqueira0763@hotmail.com.


## Referências


- [Encriptação de dados](https://learn.microsoft.com/pt-pt/dotnet/standard/security/encrypting-data)
- [**ProtoIP**](https://github.com/JoaoAJMatos/ProtoIP): Uma biblioteca para trabalhar com redes e criptografia 

