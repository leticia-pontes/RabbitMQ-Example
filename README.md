# Exemplo de Integração com RabbitMQ

Este projeto demonstra uma solução com **RabbitMQ** para comunicação entre serviços:

- **API REST** (`ConsumerApi`): Recebe dados JSON e salva no banco de dados.
- **Subscriber** (`RabbitMQ.Subscriber`): Consome mensagens da fila e envia à API.
- **Publisher** (`RabbitMQ.Publisher`): Publica mensagens JSON na fila RabbitMQ.


## 🚀 Como Executar

> Observação:
Inclua sua URI do RabbitMQ (protocolo amqps) no arquivo `.env`.
Esse arquivo deve ser colocado dentro do diretório `bin/Debug/net8.0/`, que é onde o executável da aplicação é gerado.
>
> Exemplo do `.env`:
> ```env
> RABBITMQ_URI=amqps://user123:abcDEF456@lion.rmq.cloudamqp.com/user123
> # Ver https://www.cloudamqp.com/ para saber mais
> ```

1. **Inicie a API** (`ConsumerApi`), que estará ouvindo requisições para salvar os dados no banco.
2. **Inicie o Subscriber** (`RabbitMQ.Subscriber`), que ficará escutando a fila RabbitMQ e enviando os dados para a API.
3. **Inicie o Publisher** (`RabbitMQ.Publisher`), que envia mensagens JSON para a fila.


## 💾 Requisitos

- .NET 8
- RabbitMQ Server (pode usar Docker)
- SQL Server


## 🧪 Testando

1. Inicie os serviços na ordem: API → Subscriber → Publisher.
2. O Publisher envia um JSON para a fila.
3. O Subscriber consome a mensagem e envia para a API.
4. Verifique o banco para confirmar os dados inseridos.
