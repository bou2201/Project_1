drop database if exists MenswearDB;

create database MenswearDB;

use MenswearDB;

/*Create Table*/
create table Sellers(
	seller_id int auto_increment primary key,
    seller_name varchar(100) not null,
    user_name varchar(50) not null unique,
    user_pass varchar(100) not null
);

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
    customer_phone varchar(20) not null
);

create table Menswears(
	menswear_id int auto_increment primary key,
    menswear_name varchar(100) not null,
    menswear_description varchar(500) not null,
    menswear_brand varchar(50) not null,
    menswear_material varchar(50) not null,
    menswear_price decimal not null,
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
    constraint fk_table_color foreign key(color_id) references Colors(color_id)-- sua lai di
);

create table Invoices(
	invoice_no int auto_increment primary key,
    invoice_date datetime default now() not null,
    invoice_status int not null,
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
    total_due decimal not null,
    quantity int not null
);

delimiter $$
create trigger tg_before_insert before insert
	on MenswearTables for each row
    begin
		if new.quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_CheckAmount
	before update on MenswearTables
	for each row
	begin
		if new.quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckAmount: amount must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create procedure sp_createCustomer(IN customerName varchar(100), IN customerPhone varchar(20), OUT customerId int)
begin
	insert into Customers(customer_name, customer_phone) values (customerName, customerPhone); 
    select max(customer_id) into customerId from Customers;
end $$
delimiter ;


/*Insert Data*/
insert into Sellers(seller_name, user_name, user_pass) values 
					('Chuc', 'admin1', '91f84bf10761457fceb181f33c00a23d'),
                    ('Nam', 'admin2', '91f84bf10761457fceb181f33c00a23d');
					-- pass : Menswear22@

insert into Categories(category_name) values 
				('Shirts'), ('Pants');
select * from Categories;

insert into Sizes(size_name) values 
				('S'), ('M'), ('L'), ('XL'), ('XXL');
select * from Sizes;

insert into Colors(color_name) values 
				('White'), ('Black'), ('Red'), ('Blue'), ('Green'), ('Gray'), ('Pink');
select * from Colors;

insert into Customers(customer_name, customer_phone) values
				('Nguyen Lam Chuc', '0328482434'),
                ('Nguyen Van A','0123456789'),
                ('Tran Thi B','0321654987'),
                ('Dinh Hai Nam', '0823772298');
select * from Customers;

insert into Menswears(menswear_name, menswear_description, menswear_brand, menswear_material, menswear_price, category_id) values 
				('Polo Venus', 'Vest collar, soft, masculine, luxurious silk material.',
                         'ONTOP', 'cotton', '249000', 1),
				('Polo Xiao', 'The slim-fit form fits snugly and flatters the figure.',
                         'DAVIES', 'cotton', '199000', 1),
				('Polo Vip', 'The shirt has detailed leather pockets on the chest to create accents.',
                         'COOLMATE', 'cotton', '199000', 1),
				('Plaid Shirt Nuri', 'Medium stretch, durable quality no need to last long.',
                         'SSSTUTTER', 'cotton', '289000', 1),
				('Sport Shirt Shazam', 'Shirt used for activities, gym or daily wear.',
                         'COOLMATE', 'cotton', '259000', 1),
				('Short-sleeved Shirt', 'Super soft fabric, this fitted shirt features a button-down .',
                         'COOLMATE', 'cotton', '199000', 1),
				('T-Shirt ABS', '180gsm round spun cotton t-shirt, round collar, Jersey neckband.',
                         'SSSTUTTER', 'cotton', '199000', 1),
				('T-Shirt CBS', 'Short-sleeve round-neck T-shirt with good absorbency to bring comfort.',
                         'COOLMATE', 'cotton', '199000', 1),
				('T-Shirt Lega', 'Short-sleeved round-neck t-shirt brings youthfulness and dynamism.',
                         'ONTOP', 'cotton', '199000', 1),
				('Plaid T-Shirt', 'Breathable material shirt, collar with buttons all around.',
                         'SSSTUTTER', 'cotton', '199000', 1),
				('Tanktop 2020', 'Short-sleeved round neck shirt brings strength and coolness.',
                         'COCCACH', 'cotton', '149000', 1),
				('Tanktop Model', 'Round neck short sleeve top, designed with high quality soft. ',
                         'SSSTUTTER', 'cotton', '149000', 1),
				('Tanktop 2019', 'A sleeveless tank top with Nirvana print on the chest.',
                        'DOCMENSWEAR', 'synthetic fabric', '149000', 1),
				('Blazer Mozi', 'Collared shirt, simple design, wide form, shapes, concealer.',
                         'ZARA', 'mixed wool', '739000', 1),
				('Blazer Ociput', 'The jacket is made of soft, breathable and lightweight fabric.',
                        'H2T', 'flannel fabric', '639000', 1),
				('Blazer Mile', 'Easy to operate, and easy to combine with a variety of clothes.',
                         'TORANO', 'raw cloth', '639000', 1),
				('Kaki Habbit', 'Flat front and elasticated back, side seam pockets.',
                         'COCCACH', 'cotton', '405000', 2),
				('Kaki LPL', 'The pants are super durable, super soft.',
                         '5THEWAY', 'cotton', '405000', 2),
				('Jogger Version', 'The pants are designed with two-pipe, heat-insulating.',
                         '5THEWAY ', 'cotton', '249000', 2),
				('Jogger Lusi', 'The pants are designed to be soft with two tubes, good insulation.',
                         '5THEWAY', 'cotton', '349000', 2),
				('Jean Guggi', 'Pants with elastic waistband, size zip pocket and gold.',
                         'ONTOP', 'cotton, spandex', '342000', 2),
				('Jean Balance', 'Blue pants. Low floor. Fading, patchy, and painful throughout.',
                         'COOLMATE', 'cotton', '468000', 2),
				('Short Moon', 'Garments are dyed & washed with enzymes.',
                         'DIRTY COINS', 'cotton', '249000', 2),
				('Short Jean ABS', 'Zip/button front front, high waist for a snug fit.',
                         'SSSTUTTER', 'cotton', '249000', 2),
				('Short Kaki Nuguri', 'Pants designed for style, comfort.',
                         'DEGREY', 'cotton', '149000', 2),
                ('Long Kaki Nugu', 'Flat zip pockets, sophisticated branding and durable.',
                         'DEGREY', 'cotton', '249000', 2),
				('Sport Shirt Luis', 'Shirt used for activities, smooth fabric, waistband.',
                         'Luis Vuiton', 'cotton', '149000', 1),
                ('Long-sleeved Shirt', 'Super soft fabric, this fitted shirt features a button-down.',
                         'DEGREY', 'cotton', '149000', 1),
                ('Puppo T-Shirt', 'Shirt used for activities, gym or daily wear, smooth fabric.',
                         'DEGREY', 'cotton', '149000', 1),
				('Jean Version 2019', 'Pants with elastic waistband, size zip pocket and gold trim .',
                         'DEGREY', 'cotton', '249000', 2),
                ('Kaki Noisy', 'The pants are super durable, creating a unique and unique .',
                         'ONTOP', 'cotton', '149000', 2),
                ('T-Shirt ConPo', 'Short-sleeve round-neck T-shirt with good absorbency.',
                         'TORADO', 'cotton', '149000', 1),
				('T-Shirt Cicko', '240gsm round spun cotton t-shirt, round collar, Jersey neckband.',
                         'ONTOP', 'cotton', '149000', 1),
                ('JOGGER MUSI', 'Good insulation, and have 2 zippered back pockets.',
                         'BAD HABBIT', 'cotton', '449000', 2),
                ('Jean Basic', 'Black pants. Low floor. Fading and painful throughout.',
                         'DEGREY', 'cotton', '349000', 2),
                ('Tanktop Loppy ', 'A sleeveless tank top with Nirvana print on the chest.',
                         'SWE', 'cotton', '249000', 1),
                ('T-Shirt Koppa ', 'Printed with a lovely artistic baby image.',
                         'ONTOP', 'cotton', '149000', 1),
                ('Blazer Local', 'The loose-fitting shirt without shoulder straps ',
                         '5THEWAY', 'cotton', '149000', 1),
                ('Blazer Lobby', 'Easy to operate, and easy to combine with a variety of clothes.',
                         'SSSTUTTER', 'cotton', '349000', 1),
                ('Long Shirt Paly', 'Shirt used for activities, gym or daily wear.',
                         'DEGREY', 'cotton', '149000', 1),
                ('T-Shirt rare', 'Featuring the goddess Victorya on the front.',
                         'DIRTYCOIN', 'cotton', '149000', 1),
                ('Jean Migger', '2 zippered back pockets to keep valuables safe.',
                         '5THEWAY', 'cotton', '149000', 2),
                ('Long Short', 'Garments are dyed & washed with enzymes, side pockets.',
                         'ONTOP', 'cotton', '149000', 2),
                ('Polo Manage', 'Shirt has a cool fabric, the slim-fit form fits snugly',
                         'DAVIES', 'cotton', '249000', 1),
                ('Blazer 2021', 'The jacket is made of soft, breathable and lightweight.',
                         'COOLMATE', 'cotton', '349000', 1),
                ('T-Shirt Gucci', 'T-shirt with good absorbency to bring comfort.',
                         'Gucci', 'cotton', '349000', 1),
                ('Kaki Limited 2019', 'The pants are super durable creating a unique and unique personality.',
                         'DAVIES', 'cotton', '599000', 2),
                ('Jogger Middle', 'Good insulation, and have 2 zippered back pockets.',
                         'DAVIES', 'cotton', '249000', 2),
                ('T-Shirt Monai', 'Short-sleeve round-neck featuring the goddess Victorya on the front',
                         'DEGREY', 'cotton', '249000', 1),
                ('Jogger Mionsa', 'The pants are designed with two-pipe, heat-insulating.',
                         'DAVIES', 'cotton', '449000', 2),
                ('Jean basic 2019', 'Pants with elastic waistband, size zip pocket and gold.',
                         'DIRTY COIN', 'cotton', '249000', 2),
                ('Tanktop Moli', 'Neck shirt brings strength and coolness, with logo.',
                         'ONTOP', 'cotton', '149000', 1),
                ('KaKi Vision', 'Flat front and elasticated back, side seam pockets.',
                         '5THEWAY', 'cotton', '349000', 2),
                ('T-Shirt Maowong', 'Short-sleeve round-neck featuring the goddess Victorya on the front.',
                         'DAVIES', 'cotton', '249000', 1),
                ('Arr-sleeved Shirt', 'Super soft fabric, this fitted shirt features a button-down collar.',
                         'DAVIES', 'cotton', '149000', 1),
                ('Arr T-Shirt ', 'Short-sleeve round-neck T-shirt with good absorbency.',
                         'DAVIES', 'cotton', '249000', 1),
                ('Plaid Shirt 2020', 'Soft fabric, medium stretch, durable quality no need to last long.',
                         'DAVIES', 'cotton', '299000', 1),
                ('Jean Ovantine', 'The pants are super soft, not afraid of losing their form.',
                         'ONTOP', 'cotton', '269000', 2),
                ('Sport Shirt', 'Shirt used for activities, gym or daily wear, not hot, waistband.',
                         '5THEWAY', 'cotton', '189000', 1),
				('T-Shirt Asstron', 'Short-sleeve round-neck T-shirt with good absorbency to bring comfort.',
                         'DEGREY', 'cotton', '249000', 1);
                -- ('T-Shirt Profence', 'The shirt has detailed leather pockets on the chest to create accents, respecting the wearer figure.',
--                          'DIRTY COIN', 'cotton', '249000', 1),
--                 ('Short Jean Pali', 'Brown stitched pants, hip and back pockets. Zip/button front front, high waist for a snug fit.',
--                          'ONTOP', 'cotton', '149000', 2),
--                 ('Blazer Version 2021', 'he loose-fitting shirt without shoulder straps makes it comfortable to wear, easy to operate, and easy to combine with a variety of clothes.',
--                          'DAVIES', 'cotton', '149000', 1);
select * from Menswears;
                         
insert into MenswearTables(menswear_id, size_id, color_id, quantity) values
						(1, 4, 1, 100),
                        (2, 2, 2, 100),
                        (3, 3, 3, 100),
                        (4, 3, 6, 100),
                        (5, 4, 7, 100),
                        (6, 1, 3, 100),
                        (7, 3, 2, 100),
                        (8, 4, 1, 100),
                        (9, 5, 3, 100),
                        (10, 3, 5, 100),
                        (11, 3, 4, 100),
                        (12, 4, 1, 100),
                        (13, 5, 3, 100),
                        (14, 4, 1, 100),
                        (15, 4, 6, 100),
                        (16, 5, 7, 100),
                        (17, 2, 2, 100),
                        (18, 2, 2, 100),
                        (19, 3, 7, 100),
                        (20, 4, 4, 100),
                        (21, 1, 4, 100),
                        (22, 3, 1, 100),
                        (23, 2, 2, 100),
                        (24, 5, 2, 100),
                        (25, 5, 6, 100),
                        (26, 2, 3, 100),
                        (27, 3, 6, 100),
                        (28, 3, 2, 100),
                        (29, 5, 4, 100),
                        (30, 2, 6, 100),
                        (31, 5, 2, 100),
                        (32, 2, 1, 100),
                        (33, 2, 7, 100),
                        (34, 4, 4, 100),
                        (35, 4, 4, 100),
                        (36, 1, 5, 100),
                        (37, 2, 2, 100),
                        (38, 3, 3, 100),
                        (39, 3, 3, 100),
                        (40, 1, 1, 100),
                        (41, 1, 6, 100),
                        (42, 5, 1, 100),
                        (43, 5, 6, 100),
                        (44, 1, 7, 100),
                        (45, 5, 7, 100),
                        (46, 2, 1, 100),
                        (47, 5, 5, 100),
                        (48, 1, 4, 100),
                        (49, 1, 4, 100),
                        (50, 2, 3, 100),
                        (51, 5, 1, 100),
                        (52, 2, 2, 100),
                        (53, 4, 1, 100),
                        (54, 4, 2, 100),
                        (55, 4, 6, 100),
                        (56, 2, 7, 100),
                        (57, 3, 2, 100),
                        (58, 1, 3, 100),
                        (59, 2, 2, 100),
                        (60, 3, 1, 100);
                        
drop user if exists 'admin'@'localhost';
create user if not exists 'admin'@'localhost' identified by 'admin2002';
grant all on MenswearDB.* to 'admin'@'localhost';

select * from MenswearTables;

insert into Invoices(customer_id, invoice_status) values
			(1, 1), (2, 1), (3, 1);
select * from Invoices;
                        
select * from Menswears, Categories where Menswears.category_id = Categories.category_id;

select * from Menswears, MenswearTables, Categories
where Menswears.menswear_id = 1 and Menswears.menswear_id = MenswearTables.menswear_id
								and Menswears.category_id = Categories.category_id;
                                
select menswear_id from Menswears where menswear_id=1;

select * from Invoices, Customers 
where Customers.customer_id = Invoices.customer_id;



