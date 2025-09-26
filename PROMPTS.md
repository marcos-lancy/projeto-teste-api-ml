# Prompts Usados

### Consulta sobre [JsonSerializer.Deserialize e Utf8JsonReader]
```
Estou desenvolvendo um desafio técnico onde preciso criar um CRUD em C# utilizando um arquivo .json como se fosse um banco de dados.

Já implementei a versão básica usando System.Text.Json, lendo o JSON inteiro, convertendo para objetos e mantendo os dados em memória. O CRUD (Create, Read, Update e Delete) funciona corretamente.

Agora, quero melhorar meu código pensando em boas práticas e principalmente performance para cenários com arquivos JSON grandes.

Já usei o Utf8JsonReader, que faz parsing de JSON de forma "streaming" e evita carregar o arquivo inteiro em memória. Preciso entender:

Qual a diferença prática entre usar JsonSerializer.Deserialize e Utf8JsonReader?

Em quais cenários o Utf8JsonReader é mais recomendado?

Como eu poderia adaptar meu CRUD atual para usar Utf8JsonReader na leitura (Read), mantendo JsonSerializer para Create/Update/Delete?

Se possível, me mostre um exemplo completo em C# que lê um JSON grande usando Utf8JsonReader, filtrando registros sem precisar carregar tudo em memória.

Quero que a resposta seja didática, com código pronto para testar, e que destaque as vantagens e desvantagens de cada abordagem.
```

### Padrão de Commits
```
Estou participando de um processo seletivo para o Mercado Livre e quero que meus commits no repositório do teste técnico sigam boas práticas profissionais.

Eu já conheço e utilizo o padrão Conventional Commits
, que define mensagens padronizadas como:

feat: para novos recursos

fix: para correções

refactor: para melhorias internas

docs: para documentação

chore: para mudanças de configuração ou tarefas auxiliares

Gostaria que você me ajudasse a:

Verificar se o Mercado Livre adota oficialmente Conventional Commits ou alguma variação interna desse padrão.

Me dar recomendações de boas práticas usadas em empresas de grande porte para manter um histórico limpo e semântico de commits.

Gerar exemplos práticos de commits, aplicados ao meu desafio técnico de CRUD em JSON, cobrindo casos como:

Implementar uma nova feature

Corrigir um bug na leitura do JSON

Refatorar para melhorar performance com Utf8JsonReader

Ajustar documentação e configuração do projeto

Orientar sobre como organizar os commits de forma que o revisor veja uma linha do tempo clara do desenvolvimento.

Quero que a resposta seja prática, com exemplos de commits reais que eu possa aplicar diretamente neste projeto.
```