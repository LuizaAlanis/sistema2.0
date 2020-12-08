-- GRUPO 1 - Restaurante Mexicano --
/*
	INTEGRANTES
    
    Guilherme Bugliani     -- Analista back-end e de banco de dados
    Kayke do Nascimento    -- Analista de sistemas
    Leonardo Biguinatti    -- Analista de infraestrutura
    Luiza Alanis           -- Desenvolvedora full-stack e líder

*/

-- Exclusão de possivel banco existente

Drop database if exists db_restaurante;

-- Banco de dados
create database db_restaurante
default character set utf8
default collate utf8_general_ci;

use db_restaurante;

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
    valorUnitario decimal(6,2),
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
from Comanda inner join ItensComanda
	on Comanda.id = ItensComanda.idComanda
inner join Produto
	on ItensComanda.idProduto = Produto.id
inner join Funcionario
	on Comanda.idFuncionario = Funcionario.id;
    
create view viewDelivery
as select
	Delivery.id,
    Delivery.dataEntrega,
    Delivery.destinatario,
    Produto.produto,
    Produto.valor,
    ItensDelivery.quantidade,
    ItensDelivery.subtotal,
    Delivery.total
from Delivery inner join ItensDelivery
	on Delivery.id = ItensDelivery.idDelivery
inner join Produto
	on ItensDelivery.idDelivery = Delivery.id;
    
-- Inserts

Insert into cargo(id, cargo, descricao, valor)
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
values(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),(15),(17),(18),(19),(20);

Insert into Funcionario(funcionario,cpf,dataNascimento,sexo,idCargo)
values('Isabella Drummond','57742691117','1989/07/24','F','2'),
('Alexandre Silva','03380790095','1991/08/12','M','1'),
('Rita Oliveira','22842558090','1986/03/14','F','1'),
('Isabelly Costa','62438425334','1984/12/13','F','3'),
('Henry Aragão','21428182500','1968/10/22','M','2'),
('Luan Porto','34709493014','1991/12/13','M','3'),
('Jaqueline Paranhos','22984895720','1993/08/20','F','1'),
('Luan Fagundes','30992376556','1986/11/07','F','4'),
('Felipe Galvão','14406172017','1992/09/12','F','2'),
('Júlia Araújo','19738521955','1998/10/13','F','5');

Insert into Celular(proprietario, observacao,ddd,numero)
values('Isabella Silva','Cliente','11','982421992'),
('Pietro Silva','Cliente','11','997749998'),
('Maria Oliveira','Cliente','11','996487993'),
('Odair Costa','Cliente','19','999577492'),
('Ricardo Aragão','Fonecedor','11','988579544'),
('Fernando Porto','Cliente','11','997640996'),
('Roberto Paranhos','Cliente','12','981173108'),
('Luana Fagundes','Cliente','11','999600235'),
('Túlio Galvão','Cliente','14','983142825'),
('Júlia Araújo','Funcionária','11','997526390');

Insert into Reserva(idMesa,quantidadeCadeiras,dataReserva,hora,idContato)
values(1,4,'2020/12/12','21:30',1),
(3,4,'2020/12/08','19:15',2),
(2,4,'2020/12/23','20:00',3),
(5,4,'2020/12/12','17:45',4),
(6,4,'2020/12/09','16:45',5),
(4,6,'2020/12/24','18:30',6),
(8,8,'2020/12/27','19:35',7),
(9,4,'2020/12/27','23:00',8),
(10,6,'2020/12/27','21:30',9),
(7,6,'2020/12/12','23:00',10);

Insert into Produto(produto,idCategoria,valor,info,validade,quantidade,promocao)
values('Guacamole','1','37.00','Prato','2021/02/22','300','N'),
('Burrito','1','32.00','Prato','2021/01/31','300','N'),
('Taco','1','25.00','Prato','2021/01/31','300','N'),
('Mixiote','1','31.00','Prato','2021/01/31','300','N'),
('Chilli con Carne','1','41.00','Prato','2021/01/31','300','N'),
('Alegría','3','21.00','Sobremesa','2021/09/05','300','N'),
('Polvorón','3','13.00','Sobremesa','2021/09/05','300','N'),
('Garapiñados','3','17.00','Sobremesa','2021/09/05','300','S'),
('Licuados','2','12.00','Bebida','2021/10/10','340','N'),
('Águas Frescas','2','8.00','Bebida','2021/10/10','340','N'),
('Pulque','2','29.00','Bebida','2022/11/20','340','N'),
('Atole','2','7.00','Bebida','2021/07/20','340','N'),
('Mole Poblano','1','26.00','Prato','2021/07/20','340','N'),
('Pozole','1','28.00','Prato','2021/09/05','340','N'),
('Cochinita Pibil','1','39.90','Prato','2021/08/07','320','N'),
('Horchata','1','28.90','Prato','2021/05/09','320','N'),
('Smothie de Cacto','2','17.50','Bebida','2021/10/10','320','N'),
('Mojito','2','15.00','Bebida','2021/10/10','320','N'),
('Chicarrón','1','27.00','Prato','2020/10/12','320','N'),
('Tinga de Pollo','1','25.00','Prato','2021/01/31','320','N'),
('Palanqueta','3','7.00','Sobremesa','2021/09/05','320','N'),
('Margarita','2','30.00','Bebida','2021/09/05','320','N'),
('Quesadillas','1','29.90','Prato','2021/09/05','320','N'),
('Tortilla de Milho','1','35.00','Prato','2021/02/02','80','N'),
('Tortilla de Trigo','1','45.00','Prato','2021/03/05','90','N'),
('Nachos Assados','1','25.00','Prato','2020/12/31','150','S'),
('Nachos Fritos','1','23.00','Prato','2021/01/03','250','S'),
('Michelada','2','12.00','Bebida','2021/08/07','470','N'),
('Tequila','2','18.00','Bebida','2021/08/07','680','N'),
('Mezcal','2','18.00','Bebida','2021/08/07','470','N');