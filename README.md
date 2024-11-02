<h1> Projeto de Gerador de certificados desenvolvido em .NET com padrões MVC </h1>
</br>

<h2>Lista de dependencias que precisam ser criadas localmente:</h2>
  - dotnet add package DinkToPdf</br>
  - dotnet add package Microsoft.EntityFrameworkCore</br>
  - dotnet add package Microsoft.EntityFrameworkCore.Design</br>
  - dotnet add package Microsoft.EntityFrameworkCore.SqlServer</br>
  - dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design</br>
  - dotnet add package RabbitMQ.Client</br>
  - dotnet add package Swashbuckle.AspNetCore</br>
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
{  </br>
  "nome": "Carlos Alberto",  </br>
  "nacionalidade": "Malasio",  </br>
  "estado": "XA",  </br>
  "dataNascimento": "2000-01-10",  </br>
  "documento": "2222-00",  </br>
  "dataConclusao": "2024-08-11",  </br>
  "curso": "JAVA Starter",  </br>
  "cargaHoraria": 12,  </br>
  "dataEmissao": "2024-11-02",  </br>
  "nomeAssinatura": "Marcos Sem Minie",  </br>
  "cargo": "Doutor",  </br>
  "caminhoPDF": ""  </br>
}
