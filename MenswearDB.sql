drop database if exists MenswearDB;

create database MenswearDB;

use MenswearDB;

create table Sellers(
	seller_id int auto_increment primary key,
    seller_name varchar(100) not null,
    user_name varchar(50) not null unique,
    user_pass varchar(100) not null
);
insert into Sellers(seller_name, user_name, user_pass) values 
					('Chuc', 'admin1', '91f84bf10761457fceb181f33c00a23d'),
                    ('Nam', 'admin2', '91f84bf10761457fceb181f33c00a23d');
					-- pass : Menswear22@
select * from Sellers;

create user if not exists 'admin'@'localhost' identified by 'admin2002';
grant all on MenswearDB.* to 'admin'@'localhost';
                    
create table Categories(
	category_id int auto_increment primary key,
    category_name varchar(100) not null
);
insert into Categories(category_name) values ('Shirts'), ('Pants');
select * from Categories;

create table Sizes(
	size_id int auto_increment primary key,
    size_name varchar(50) not null
);
insert into Sizes(size_name) values ('S'), ('M'), ('L'), ('XL'), ('XXL');
select * from Sizes;

create table Colors(
	color_id int auto_increment primary key,
    color_name varchar(50) not null
);
insert into Colors(color_name) values ('White'), ('Black'), ('Red'), ('Blue'), ('Green'), ('Gray'), ('Pink');
select * from Colors;

create table Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null,
    customer_phone varchar(11) not null
);

create table Menswears(
	menswear_id int auto_increment primary key,
    menswear_name varchar(100) not null,
    menswear_description varchar(500) not null,
    menswear_brand varchar(50) not null,
    menswear_material varchar(50) not null,
    menswear_price decimal(20,2) not null,
    category_id int,
    constraint fk_category_id foreign key(category_id) references Categories(category_id)
);
insert into Menswears(menswear_name, menswear_description, menswear_brand, menswear_material, menswear_price, category_id) values 
						('Polo', '', 'ONTOP', 'cotton', '249.000', 1),
                        ('Polo', '', 'DAVIES', 'cotton', '199.000', 1),
                        ('Polo', '', 'COOLMATE', 'cotton', '199.000', 1),
                        ('Plaid Shirt', '', 'SSSTUTTER', 'cotton', '289.000', 1),
						('Sport Shirt', '', 'COOLMATE', 'cotton', '259.000', 1),
                        ('Short-sleeved Shirt', '', 'COOLMATE', 'cotton', '199.000', 1),
                        ('T-Shirt', '', 'SSSTUTTER', 'cotton', '199.000', 1),
                        ('T-Shirt', '', 'COOLMATE', 'cotton', '199.000', 1),
                        ('T-Shirt', '', 'ONTOP', 'cotton', '199.000', 1),
                        ('Plaid T-Shirt', '', 'SSSTUTTER', 'cotton', '199.000', 1),
                        ('Tanktop', '', 'COCCACH', 'cotton', '149.000', 1),
                        ('Tanktop', '', 'SSSTUTTER', 'cotton', '149.000', 1),
                        ('Tanktop', '', 'DOCMENSWEAR', 'synthetic fabric', '149.000', 1),
                        ('Blazer', '', 'ZARA', 'mixed wool', '739.000', 1),
                        ('Blazer', '', 'H2T', 'flannel fabric', '639.000', 1),
                        ('Blazer', '', 'TORANO', 'raw cloth', '639.000', 1),
                        ('Kaki', '', 'COCCACH', 'cotton', '405.000', 2),
                        ('Kaki', '', '5THEWAY', 'cotton', '405.000', 2),
                        ('Jogger', '', '5THEWAY ', 'cotton', '249.000', 2),
                        ('Jogger', '', '5THEWAY', 'cotton', '349.000', 2),
                        ('Jean', '', 'ONTOP', 'cotton, spandex', '342.000', 2),
                        ('Jean', '', 'COOLMATE', 'cotton', '468.000', 2),
                        ('Short', '', 'DIRTY COINS', 'cotton', '249.000', 2),
                        ('Short Jean', '', 'SSSTUTTER', 'cotton', '249.000', 2),
                        ('Short Kaki', '', 'DEGREY', 'cotton', '149.000', 2);
select * from Menswears;                
                        
create table MenswearTables(
	menswear_id int,
    size_id int,
    color_id int,
    quantity int not null,
    constraint pk_table primary key(menswear_id, size_id, color_id),
    constraint fk_table_menswear foreign key(menswear_id) references Menswears(menswear_id),
    constraint fk_table_size foreign key(size_id) references Sizes(size_id),
    constraint fk_table_color foreign key(color_id) references Colors(color_id)
);
insert into MenswearTables(menswear_id, size_id, color_id, quantity) values
						(1, 4, 1, 2),
                        (2, 2, 2, 2),
                        (3, 3, 3, 3),
                        (4, 3, 6, 4),
                        (5, 4, 7, 3),
                        (6, 1, 3, 2),
                        (7, 3, 2, 3),
                        (8, 4, 1, 3),
                        (9, 5, 3, 2),
                        (10, 3, 5, 4),
                        (11, 3, 4, 3),
                        (12, 4, 1, 3),
                        (13, 5, 3, 5),
                        (14, 4, 1, 2),
                        (15, 4, 6, 2),
                        (16, 5, 7, 2),
                        (17, 2, 2, 4),
                        (18, 2, 2, 5),
                        (19, 3, 7, 3),
                        (20, 4, 4, 4),
                        (21, 1, 4, 4),
                        (22, 3, 1, 5),
                        (23, 2, 2, 5),
                        (24, 5, 2, 6),
                        (25, 5, 6, 4);
select * from MenswearTables;

create table Invoices(
	invoice_no int auto_increment primary key,
    order_date datetime default now() not null,
    total_due double(5, 2) not null,
    customer_id int,
    seller_id int,
    constraint fk_customer_name foreign key(customer_id) references Customers(customer_id),
	constraint fk_seller_name foreign key(seller_id) references Sellers(seller_id)
);

create table InvoiceDetails(
	invoice_no int,
    menswear_id int,
    constraint pk_detail primary key(invoice_no, menswear_id),
    constraint fk_detail_invoice foreign key(invoice_no) references Invoices(invoice_no),
    constraint fk_detail_menswear foreign key(menswear_id) references Menswears(menswear_id),
    price double(20,2) not null,
    quantity int not null
);




