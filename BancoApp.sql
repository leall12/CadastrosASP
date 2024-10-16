-- drop database dbBancoApp; --
create database dbBancoApp;
use dbBancoApp;
create table usuario
(
IdUsu int primary key auto_increment,
nomeUsu varchar(50) not null,
Cargo varchar(50) not null,
DataNasc datetime
);

create table cliente(
IdCli int primary key auto_increment,
data_nascimento datetime,
nome varchar(50) not null,
endereco varchar(75),
cpf decimal(11,0),
telefone varchar(18)
)
/* insert into usuario(nomeUsu,Cargo,DataNasc)
 values('Robison','Gerente','1978-05-01'),
('Luisao','Colaborador','2000-12-10'); */


-- select * from usuario; --