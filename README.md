<h1> Projeto de Gerador de certificados desenvolvido em .NET com padrões MVC </h1>
</br>

<h2>Lista de dependencias que precisam ser criadas localmente:</h2>
  - dotnet add package DinkToPdf
  - dotnet add package Microsoft.EntityFrameworkCore
  - dotnet add package Microsoft.EntityFrameworkCore.Design
  - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  - dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
  - dotnet add package RabbitMQ.Client
  - dotnet add package Swashbuckle.AspNetCore
  - dotnet add package System.Text.Json

  </br>
<h2>Auxiliar: Métodos, URLs, JSON</h2>

//Método responsável por realizar a operação POST.
https://localhost:7282/api/Api/custom/post

//Método responsável pore realizar buscas de todos os dados gravados
https://localhost:7282/api/Api/custom/getAll

//Método responsável pore realizar buscas individuais dos dados gravados
https://localhost:7282/api/Api/2

//URL onde é possível consultar os Certificados Gerados.
https://localhost:7282/certificado/layout/1

JSON [POST]

{
  "nome": "Carlos Alberto",
  "nacionalidade": "Malasio",
  "estado": "XA",
  "dataNascimento": "2000-01-10",
  "documento": "2222-00",
  "dataConclusao": "2024-08-11",
  "curso": "JAVA Starter",
  "cargaHoraria": 12,
  "dataEmissao": "2024-11-02",
  "nomeAssinatura": "Marcos Sem Minie",
  "cargo": "Doutor",
  "caminhoPDF": ""
}
