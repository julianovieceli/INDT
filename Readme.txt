Primeiramente subir o container da imagem do mysql:

/* se noa tiver a imagem na maquina*/
docker run --name teste-INDT -e MYSQL_ROOT_PASSWORD=admin -p 3306:3306 -d mysql:latest

/* Se ja tiver, basta subir*/
docker start teste-INDT


-- Script

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
