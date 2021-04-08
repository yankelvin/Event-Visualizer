# Event Visualizer

## Tecnologias
* .Net Core 3.1
* XUnit e Moq;
* SignalR
* Node.js 14.x
* React
* MongoDb com MongoDb.Driver
* Docker

## Por ser uma solução em cima de cadastro e visualização de eventos em tempo real foram tomadas algumas decisões:
* Utilização do MongoDb para armazenamento, visto que não era necessário haver relacionamentos e a sua leitura se tornaria mais rápida. Hoje temos várias soluções de Cloud gratuitas para o mongo através do Mongo Atlas, então utilizei o MongoDb hospedado no GCP (Google) em São Paulo, não precisa se preocupar pois está sem restrição de Ip;
* Utilização de um Bus em memória para atuar como serviço de comunicação entre as camadas, essa decisão foi tomada visto que poderia haver um aumento no número de requisições para inserção de eventos e então poderia plugar esse Bus em alguma solução de Bus em Nuvem;
* O SignalR foi escolhido para que pudessemos trabalhar com websocket, ou seja, crio uma conexão entre o Frontend e o Backend de forma que não tenha necessidade de "requisições" ou pollings para que a visualização seja atualizada automáticamente;
* O React foi escolhido devido a sua manipulação de estados, assim juntamente com o SignalR quando recebo uma nova atualização preciso apenas inserir o dado no meu "State" e então o componente já é atualizado em tempo real, além disso, o React é uma ótima ferramenta para trabalhar de forma componentizada.

## Sobre a solução
A solução foi feita em cima de uma arquitetura em camadas com base no DDD (Domain Drive Design). Ao longo de toda escrita do código foi pensado nos princípios do SOLID e deixando o código mais Clean possível. Então temos o Core da aplicação que é onde tem os objetos de domínio, serviço de comunicação, acesso a dados e validações em comúns para toda a aplicação, para cada domínio temos a camada de domínio onde está a entidade (no meu caso o Document) para ser persistido no banco, como também os Commands que é onde ficam as regras que demonstram intenção de manipular o banco. Acima dela tem a camada de acesso a dados que é onde o repositório é implementado para acesso aos dados e inserção/atualização, para isso foi utilizado o padrão Repository junto com o UnitOfWork para mapear as transações e realizar um Commit no banco único. Acima dela temos a camada de aplicação que é onde as fontes externas vão estar chamando, como por exemplo os Controllers, nela temos os Mappers necessários, as ViewModels e o AppService em si.

## Instruções de Uso
Para rodar a aplicação é necessário ter o Docker instalado, configurado para receber Linux Containers. Tendo as necessidades previamente instaladas, necessita apenas rodar alguns comandos.

```bash
docker build -t radix .
docker run --name=radix  -p 5000:5000 -d radix -e NODE_ENV=production
```

O código acima irá buildar a aplicação e em seguida rodar na porta 5000.

## Documentação da Api
Para documentação foi utilizado o Swagger, para acessá-la, acesse a url:
```
http://localhost:5000/swagger/index.html
```
