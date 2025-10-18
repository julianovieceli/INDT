/* 1- Para rodar tudo dentro do docker, executar dentro da pasta "..\INDT" */
    docker compose -f docker-compose.yml down
    -- docker compose -f docker-compose.yml up -d

    docker compose up --build --force-recreate insurance.indt.api -d
    OR 
    docker compose up --build --force-recreate -d  /* all */

    // Sem recriar
    // docker compose up -d --force-recreate



/* 2 - Para containers separados(standalone), executar dentro da pasta "..\INDT\Docker" */

-- Executar

docker compose -f docker-compose-sbus-emulator.yml down
docker compose -f docker-compose-sbus-emulator.yml up -d

docker compose -f docker-compose-mysql.yml down
docker compose -f docker-compose-mysql.yml up -d

docker compose -f docker-compose-MongoDb.yml down
docker compose -f docker-compose-MongoDb.yml up -d



/* 3 - Passo 3 Rodar o seguinte Script de BD no MySQL Workbench ou outro cliente MySQL conectado na container */

CREATE DATABASE INDT
    DEFAULT CHARACTER SET = 'utf8mb4';

use INDT;

 create table Client
   (id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   name varchar(200), 
   docto varchar(14),  
   age int);

   
create table Insurance
   (id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   name varchar(200), 
   creationDate datetime);


 create table Proposal
   (id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   clientId int not null, 
   insuranceId int not null,
   value DECIMAL(10, 2) NOT NULL,
   statusId smallint not null  CHECK (statusId IN (1,2,3)),
   creationDate datetime,
   expirationDate datetime,
   FOREIGN KEY (clientId) REFERENCES Client(id),
   FOREIGN KEY (insuranceId) REFERENCES Insurance(id));


     create table ProposalHire
   (id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   proposalId int not null,
   description varchar(200) not null,
   creationDate datetime,
   expirationDate datetime,
   FOREIGN KEY (proposalId) REFERENCES Proposal(id));


/* Passo 4 rodar a aplicacao(a mesma ira subir os dois servicos)*/


/* Passo 5 criar nesta nesta ordem no servico Insurance.INDT.Api */
 1- Criar um seguro
 2- Criar um cliente
 3- Criar uma proposta para o cliente e seguro criado

/* Passo 6 Contratar uma proposta no servico Insurance.ProposalHire.INDT.Api */
    1- Contratar uma proposta para o cliente e seguro criado no passo 4
    2- Listar propostas contratadas


OBSERVACOES:
 1- Os testes unitarios foi feito somente da classe de servico ClientServiceTest.cs para se ter um exemplo.
 2- Existem VARIAS validacoes a serem feitas, nao foi focado nisso e sim na divisao de responsibilidades das camadas e servicos.




