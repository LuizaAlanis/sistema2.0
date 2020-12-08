-- GRUPO 1 - Restaurante Mexicano --
/*
	INTEGRANTES
    
    Guilherme Bugliani     -- Analista back-end e de banco de dados
    Kayke do Nascimento    -- Analista de sistemas
    Leonardo Biguinatti    -- Analista de infraestrutura
    Luiza Alanis           -- Desenvolvedora full-stack e líder

*/

-- Exclusão de possivel banco existente

Drop database if exists restaurante;

-- Banco de dados
create database restaurante
default character set utf8
default collate utf8_general_ci;

use restaurante;

-- Usuario de banco de dados

create user 'restaurante'@'localhost' identified with mysql_native_password by '123456';
grant all privileges on db_restaurante.* to 'restaurante'@'localhost';

-- Tabelas

create table Cargo(
	id int primary key auto_increment,
    cargo varchar(30) not null,
    descricao mediumtext,
    valor decimal(6,2) not null
);

create table Funcionario(
	id int primary key auto_increment,
    funcionario varchar(30) not null,
    cpf varchar(15) not null,
    dataNascimento date not null,
    sexo varchar(20),
    idCargo int not null,
    constraint fk_cargo foreign key (idCargo) references Cargo(id)
);

create table Categoria(
	id int primary key auto_increment,
    categoria varchar(30) not null
);

create table Produto(
	id int primary key auto_increment,
    produto varchar(50) not null,
    idCategoria int not null,
    valor decimal(6,2) not null,
    info mediumtext,
    validade date not null,
    quantidade int not null,
    promocao enum ('S','N'),
	constraint fk_categoria foreign key (idCategoria) references Categoria(id)
);

create table Celular(
	id int primary key auto_increment,
    proprietario varchar(100),
    observacao varchar(100),
    ddd int not null,
    numero int not null
);

create table Mesa(
	id int primary key
);

create table Relatorio(
    id int primary key auto_increment,
    dataRelatorio date,
	autor varchar(50) not null,
    departamento varchar(30) not null,
    titulo varchar(90) not null,
    corpo longtext not null
);

create table Comanda(
	id int primary key auto_increment,
	dataPedido date not null,
    idFuncionario int,
    idMesa int not null,
    total decimal(6,2),
    constraint fk_funcionario foreign key (idFuncionario) references Funcionario(id),
    constraint fk_mesa_comanda foreign key (idMesa) references Mesa(id)
);

create table ItensComanda(
	id int primary key auto_increment,
    idComanda int not null,
    idProduto int not null,
    quantidade int not null,
    subtotal decimal(6,2),
    constraint fk_comanda foreign key (idComanda) references Comanda(id),
    constraint fk_produto foreign key (idProduto) references Produto(id)
);

create table Delivery(
	id int primary key auto_increment,
	dataEntrega date not null,
    total decimal(6,2),
    destinatario varchar(50),
    endereco varchar(150)
);

create table ItensDelivery(
	id int primary key auto_increment,
    idDelivery int not null,
    idProduto int not null,
    quantidade int not null,
    subtotal decimal(6,2),
    constraint fk_delivery foreign key (idDelivery) references Delivery(id),
    constraint fk_produto_delivery foreign key (idProduto) references Produto(id)
);

create table Reserva(
    id int primary key auto_increment,
    idMesa int not null,
    quantidadeCadeiras int not null,
    dataReserva date not null,
    hora varchar(15) not null,
    idContato int not null,
	constraint fk_contato_reserva foreign key (idContato) references Celular(id),
    constraint fk_mesa_reserva foreign key (idMesa) references Mesa(id)
);

create table Pagamento(
    id int primary key auto_increment,
    idComanda int not null,
	formaPagamento varchar(30),
    constraint fk_comanda_pagamento foreign key (idComanda) references Comanda(id)
);

-- Views

create view viewComanda
as select
	Comanda.id,
    Comanda.idMesa,
    Comanda.dataPedido,
    Produto.produto,
    Produto.valor,
    ItensComanda.quantidade,
    Comanda.total,
    Funcionario.funcionario
from Comanda inner join ComandaItens
	on Comanda.id = ComandaItens.idComanda
inner join Produto
	on ComandaItens.idProduto = Produto.id
inner join Funcionario
	on Comanda.idFuncionario = Funcionario.id;
    
create view viewDelivery
as select
	Delivery.id,
    Delivery.dataEntrega,
    Delivery.destinatario,
    Produto.produto,
    Produto.valor,
    DeliveryItens.quantidade,
    ItensDelivery.subtotal,
    Delivery.total
from Delivery inner join ItensDelivery
	on Delivery.id = ItensDelivery.idDelivery
inner join Produto
	on ItensDelivery.idDelivery = Delivery.id;
    
-- Inserts

Insert into cargo(id, nome, descricao, valor)
values(default, 'Garçom', 'O garçom é responsável por fazer o atendimento aos clientes, fornecendo informações sobre cardápio, anotando os pedidos e servindo as mesas.', 1514.00),
	(default, 'Maitre', 'O maitre é o anfitrião do restaurante. Responsável por agendar reservas e acomodar clientes.', 2069.00),
	(default, 'Caixa', 'Essa função administrativa é responsável por pagamentos, recebimento de valores, fechamento de caixa e emissão de notas fiscais.', 1271.99),
	(default, 'Chefe de Cozinha', 'Profissional responsável por organizar a cozinha e elaborar cardápios.', 2464.00),
	(default, 'Cozinheiro', 'Os cozinheiros são os responsáveis pelo preparo dos pratos, seguindo as orientações dos chefes de cozinha.', 1650.00),
	(default, 'Auxiliar de Cozinha', 'Este é o profissional responsável pelo pré-preparo, higienização, organização e pequenas produções de alimentos dos vários setores de um restaurante.', 1420.00),
	(default, 'Confeiteiro', 'O confeiteiro é o responsável por fazer as sobremesas servidas no restaurante.', 1420.00),
	(default, 'Bartender', 'O bartender é responsável por preparar coquetéis e bebidas.', 1559.00),
	(default, 'Gerente', 'Responsável por coordenar todo o time de funcionários e assim manter o desempenho, padrões de qualidade, saúde e segurança do local.', 2071.64),
	(default, 'Faxineiro', 'Profissional responsável pela higienização do restaurante.', 1381.00);

Insert into categoria(id, categoria)
values(default, 'Entradas'),
	(default, 'Pratos'),
	(default, 'Sobremesas'),
	(default, 'Bebidas');

Insert into mesa(id)
values(1),(2),(3),(4),(5),(7),(8),(9),(10),(11),(12),(13),(14),(15),(17),(18),(19),(20);