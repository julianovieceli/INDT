-- Script

CREATE DATABASE INDT
    DEFAULT CHARACTER SET = 'utf8mb4';

use INDT;

 create table Client
   (id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   name varchar(200), 
   docto varchar(14),  
   age int);

