# Event Visualizer

## Tecnologias
* .Net Core 3.1
* XUnit e Moq;
* SignalR
* Node.js 14.x
* React
* Mongo Db em Cloud
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

O código acima irá buildar a aplicação e em seguida rodar na porta 500.

## Documentação da Api
Para documentação foi utilizado o Swagger, para acessá-la, acesse a url:
```
http://localhost:5000/swagger/index.html
```

## Mais implementações
Ideias futuras que pensei foram:
* Deixar o Frontend responsivo;
* Devido ao Frontend ser focado em visualização, acredito que uma boa ideia seria criar um Dashboard dinâmico, por exemplo, quando clicássemos em cada Região, os dados seriam filtrados com base na Região clicada, como acontece no Power Bi;
* Criar os Events de domínio para executar alguma regra quando um sensor tem falha X vezes, podendo por exemplo disparar um e-mail ou um alerta;

# Desafio para vaga de analista júnior

## Considerações Gerais

* Sua solução deverá ser desenvolvida em dotnet core 2.1+.

* No seu README, você deverá fazer uma explicação sobre a solução encontrada, tecnologias envolvidas e instrução de uso da solução. 

* É interessante que você também registre ideias que gostaria de implementar caso tivesse mais tempo.

## Problema

Imagine que você ficou responsável por construir um sistema que seja capaz de receber milhares de eventos por segundo de sensores espalhados pelo Brasil, nas regiões norte, nordeste, sudeste e sul. Seu cliente também deseja que na solução ele possa visualizar esses eventos de forma clara.

Um evento é defino por um JSON com o seguinte formato:

```json
{
   "timestamp": <Unix Timestamp ex: 1539112021301>,
   "tag": "<string separada por '.' ex: brasil.sudeste.sensor01 >",
   "valor" : "<string>"
}
```

Descrição:
 * O campo timestamp é quando o evento ocorreu em UNIX Timestamp.
 * Tag é o identificador do evento, sendo composto de pais.região.nome_sensor.
 * Valor é o dado coletado de um determinado sensor (podendo ser numérico ou string).

## Requisitos

* Sua solução deverá ser capaz de armazenar os eventos recebidos.

* Cada sensor envia um evento a cada segundo independente se seu valor foi alterado, então um sensor pode enviar um evento com o mesmo valor do segundo anterior.

* Cada evento poderá ter o estado processado ou erro, caso o campo valor chegue vazio, o status do evento será erro caso contrário processado.

* Para visualização desses dados, sua solução deve possuir:
    * Uma tabela que mostre todos os eventos recebidos. Essa tabela deve ser atualizada automaticamente.
    * Um gráfico apenas para eventos com valor numérico.

* Para seu cliente, é muito importante que ele saiba o número de eventos que aconteceram por região e por sensor. Como no exemplo abaixo:
    * Região sudeste e sul ambas com dois sensores (sensor01 e sensor02):
        * brasil.sudeste - 1000
        * brasil.sudeste.sensor01 - 700
        * brasil.sudeste.sensor02 - 300
        * brasil.sul - 1500
        * brasil.sul.sensor01 - 1250
        * brasil.sul.sensor02 - 250

## Avaliação

Nossa equipe de desenvolvedores irá avaliar código, simplicidade da solução, testes unitários, arquitetura e automatização de tarefas.

Tente automatizar ao máximo sua solução. Isso porque no caso de deploy em vários servidores, não é interessante que tenhamos que entrar de máquina em máquina para instalar cada componente da solução.

Em caso de dúvida, entre em contato com o responsável pelo seu processo seletivo.

## Considerações de Avaliação (Diferenciais)

* Documentaçao do código
* Organização/Legibilidade
* Docker (importante como fator de desequilíbrio)
* README e Makefile (este último não obrigatório, porém recomendado)
* Testes unitários
* Uso correto de dotNet core e de dotNet ef (este último não obrigatório, porém recomendado)
