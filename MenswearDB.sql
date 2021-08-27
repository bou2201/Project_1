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
					('Bo doi', 'admin2002', '2472edb1a3628b9f17107fac9ab81825');
					-- pass : PF13VTCAcademy
                    
create table Categories(
	category_id int auto_increment primary key,
    category_name varchar(100) not null
);

create table Sizes(
	size_id int auto_increment primary key,
    size_name varchar(50) not null
);

create table Colors(
	color_id int auto_increment primary key,
    color_name varchar(50) not null
);

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

create table Invoices(
	invoice_no int auto_increment primary key,
    order_date datetime default now() not null,
    total_due double(5, 2) not null,
	-- customer_name varchar(100) not null,
	-- seller_name varchar(100) not null,
	-- constraint fk_customer_name foreign key(customer_name) references Customers(customer_name),
	-- constraint fk_seller_name foreign key(seller_name) references Sellers(seller_name)
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
    price decimal(20,2) not null,
    quantity int not null
);

select * from Sellers;

