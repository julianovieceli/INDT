dentro da pasta "..\INDT\Docker"


 /* Passo 1 Primeiramente subir o container da imagem do mysql: */

/* se noa tiver a imagem na maquina*/
docker run --name teste-INDT -e MYSQL_ROOT_PASSWORD=admin -p 3306:3306 -d mysql:latest

/* Se ja tiver, basta subir*/
docker start teste-INDT


/* Passo 2 Rodar o seguinte Script de BD no MySQL Workbench ou outro cliente MySQL conectado na container */

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


/* Passo 3 rodar a aplicacao(a mesma ira subir os dois servicos)*/


/* Passo 4 criar nesta nesta ordem no servico Insurance.INDT.Api */
 1- Criar um seguro
 2- Criar um cliente
 3- Criar uma proposta para o cliente e seguro criado

/* Passo 5 Contratar uma proposta no servico Insurance.ProposalHire.INDT.Api */
    1- Contratar uma proposta para o cliente e seguro criado no passo 4
    2- Listar propostas contratadas


OBSERVACOES:
 1- Os testes unitarios foi feito somente da classe de servico ClientServiceTest.cs para se ter um exemplo.
 2- Existem VARIAS validacoes a serem feitas, nao foi focado nisso e sim na divisao de responsibilidades das camadas e servicos.




