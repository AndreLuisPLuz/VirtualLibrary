# Avaliacao 1 - Arquitetura de Software

# Alunos
André Luis Pereira da Luz

# Virtual Library API
O propósito dessa API, construída em dotnet com o uso de Entity Framework Core, é simular funções de uma livraria virtual. Um dos objetivos do projeto era aplicar a arquitetura DDD com sucesso e fundamentar as suas características.

# Fundamentos DDD
Uma aplicação DDD (Domain-Driven Design) tem como objetivo definir um domínio, isso é, uma linguagem comum entre especialistas em software e especialistas em negócios, bem-definida e livre de ambiguidades, que possa simplificar processos de comunicação e validação de requisitos. Funciona, na prática, como uma arquitetura de camadas clássica, sendo divida em quatro camadas.

### 1. Camada de Domínio
A camada de domínio contém a lógica de negócio principal e o modelo de domínio. Inclui entidades, representação de objeto e interfaces definidindo o contrato dos serviços da aplicação.

### 2. Camada de Aplicação
A camada de aplicação coordena as atividades da aplicação. É responsável pela lógica específica da aplicação, não possuindo regras de negócio dentro de si, pois estas estão na camada de Domínio. Quando falando de Web APIs, como esse projeto, essa é a camada em que nossos endpoints são implementados, e que faz chamadas aos nossos serviços.

### 3. Camada de Dados
A camada de dados oferece às outras camadas acesso a dados, gerenciando, por exemplo, as regras de coleta e alteração dos dados em BDs. No projeto, ela exclusivamente acessa um banco de dados SQL Server; em situações mais complexas, no entanto, ela não precisa se limitar somente a isso. Poderia, por exemplo, prover acesso a dados de sistemas externos de terceiros, através do consumo de APIs.

### 4. Camada de Serviços
A camada de Serviços faz o uso das estruturas e regras definidas no Domínio para implementar as regras de negócio do sistema, empregando os métodos de acesso a dados da camada de Dados para executá-las, e tipicamente disponibiliza as respostas de servidor para a camada de Aplicativos.

# Endpoints
`POST /api/v1/login` => envia os detalhes do usuário e retorna um bearer token.

`POST /api/v1/user` => cria um usuário.
`GET /api/v1/user/{id}` => trás os detalhes de um usuário.
`PUT /api/v1/user/{id}` => atualiza os detalhes de um usuário.

`POST /api/v1/gender` => cria um gênero.
`GET /api/v1/gender` => trás uma lista de todos os gêneros.
`PUT api/v1/gender/{id}` => atualiza os detalhes de um gênero.

`POST /api/v1/author` => cria um autor.
`GET /api/v1/author` => trás uma lista paginada de autores.
`GET /api/v1/author/{id}` => trás os detalhes de um autor.
`PATCH api/v1/author/{id}` => atualiza os detalhes de um autor.

`POST /api/v1/book` => cria um livro, sem autores ou gêneros no corpo.
`POST /api/v1/book/{id}/gender/{genderId}` => adiciona um gênero a um livro existente.
`POST /api/v1/book/{id}/author/{authorId}` => adiciona um gênero a um livro existente.
`GET /api/v1/book` => trás uma lista paginadas de livros.
`GET /api/v1/book/{id}` => trás os detalhes de um livro.
`DELETE /api/v1/book/{bookId}/gender/{genderId}` => remove um gênero de um livro existente.
