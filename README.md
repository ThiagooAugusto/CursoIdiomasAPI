# CursoIdiomasAPI

## Escopo
Api rest feita feita com Asp .Net Core para operações CRUD com alunos e operações de Listagem, Criação e Exclusão de turmas.

## Requisitos Atendidos
* A API deve utilizar o Entity Framework com Code First Mapping para trabalhar com o banco de dados; 

* A solução deve possuir pelo menos as seguintes informações: 

    * Aluno (Nome, CPF e e-mail) 

    * Turma (Código, nível) 
* A API possui os seguintes endpoints:
    * Alunos:
        * Cadastro
        * Edição
        * Listagem
        * Exclusão
    * Turmas
        * Cadastro
        * Listagem
        * Exclusão

### Requisitos Bônus Atendidos
* Restringir o cadastro de aluno repetido (pelo CPF) 
* Garantir o cadastro do aluno é obrigatório que ele esteja sendo matriculado em uma turma; 
* Permitir o mesmo aluno ser matriculado em várias turmas diferentes, porém restringir matrícula repetida na mesma turma; 
* Uma turma vai ter um número máximo de 5 alunos. Quando esse número for atingido, não deve ser permitido cadastrar mais nenhum aluno novo;
* Restringir exclusão de turma se ela possuir alunos; 
* Incluir filtros nas listagens. Podendo filtrar não só pelas informações obrigatórias, como também por informações adicionadas a sua escolha. 

## Padrões e técnicas utilizados no projeto
 * Injeção de dependência
 * Padrão repositório
 * Padrão Unit of Work
 * Padrão DTO
 * Paginação e filtragem de dados
 * Abordagem Code-first para EF Core
 * Aplicação dos princípios SOLID 
