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
						('Polo Venus', 'Vest collar, soft, masculine, luxurious silk material, with logo on the left chest.',
                         'ONTOP', 'cotton', '249000', 1),
                        ('Polo Xiao', 'The shirt has a cool fabric, the slim-fit form fits snugly and flatters the figure.',
                         'DAVIES', 'cotton', '199000', 1),
                        ('Polo Vip', 'The shirt has detailed leather pockets on the chest to create accents, respecting the wearer figure.',
                         'COOLMATE', 'cotton', '199000', 1),
                        ('Plaid Shirt Nuri', '97% cotton, 3% spandex, soft fabric, medium stretch, durable quality no need to last long.',
                         'SSSTUTTER', 'cotton', '289000', 1),
						('Sport Shirt Shazam', 'Shirt used for activities, gym or daily wear, smooth fabric, not hot, stable elasticity.',
                         'COOLMATE', 'cotton', '259000', 1),
                        ('Short-sleeved Shirt', 'Super soft fabric, this fitted shirt features a button-down collar.',
                         'COOLMATE', 'cotton', '199000', 1),
                        ('T-Shirt ABS', '180gsm round spun cotton t-shirt, round collar, Jersey neckband.',
                         'SSSTUTTER', 'cotton', '199000', 1),
                        ('T-Shirt CBS', 'Short-sleeve round-neck T-shirt with good absorbency to bring comfort to the wearer, featuring the goddess Victorya on the front.',
                         'COOLMATE', 'cotton', '199000', 1),
                        ('T-Shirt Lega', 'Short-sleeved round-neck t-shirt brings youthfulness and dynamism, printed with a lovely artistic baby image with interesting expressions.',
                         'ONTOP', 'cotton', '199000', 1),
                        ('Plaid T-Shirt', 'Breathable material shirt, collar with buttons all around, two chest pockets with one button closure on each pocket.',
                         'SSSTUTTER', 'cotton', '199000', 1),
                        ('Tanktop 2020', 'Short-sleeved round neck shirt brings strength and coolness, with logo on the left corner of the shirt.',
                         'COCCACH', 'cotton', '149000', 1),
                        ('Tanktop Model', 'Round neck short sleeve top, designed with high quality soft, stretchy mesh fabric. ',
                         'SSSTUTTER', 'cotton', '149000', 1),
                        ('Tanktop 2019', 'A sleeveless tank top with Nirvana print on the chest, woven stamps adorning the armpit hem.',
                        'DOCMENSWEAR', 'synthetic fabric', '149000', 1),
                        ('Blazer Mozi', 'Collared shirt, simple design, wide form, suitable for many body shapes, concealer.',
                         'ZARA', 'mixed wool', '739000', 1),
                        ('Blazer Ociput', 'The jacket is made of soft, breathable and lightweight fabric. Short body with simple design, wide form, suitable for many body shapes, creating a youthful and dynamic appearance. ',
                        'H2T', 'flannel fabric', '639000', 1),
                        ('Blazer Mile', 'The loose-fitting shirt without shoulder straps makes it comfortable to wear, easy to operate, and easy to combine with a variety of clothes.',
                         'TORANO', 'raw cloth', '639000', 1),
                        ('Kaki Habbit', 'Flat front and elasticated back, side seam pockets, tapered with shirt tail detail at leg hem.',
                         'COCCACH', 'cotton', '405000', 2),
                        ('Kaki LPL', 'The pants are super durable, super soft, not afraid of losing their form when left in the closet for a long time, creating a unique and unique personality.',
                         '5THEWAY', 'cotton', '405000', 2),
                        ('Jogger Version', 'The pants are designed with two-pipe, heat-insulating, two zippered back pockets to keep your valuables safe.',
                         '5THEWAY ', 'cotton', '249000', 2),
                        ('Jogger Lusi', 'The pants are designed to be soft with two tubes, good insulation, and have 2 zippered back pockets to keep valuables safe.',
                         '5THEWAY', 'cotton', '349000', 2),
                        ('Jean Guggi', 'Pants with elastic waistband, size zip pocket and gold trim on the outside leg, casual, classy.',
                         'ONTOP', 'cotton, spandex', '342000', 2),
                        ('Jean Balance', 'Blue pants. Low floor. Fading, patchy, and painful throughout. Five-pocket design. Belt loop at the waistband. Signature embroidered detail in off-white color on the back pocket, flatters the wearer figure. ',
                         'COOLMATE', 'cotton', '468000', 2),
                        ('Short Moon', 'Garments are dyed & washed with enzymes, subject to slight variation in colour. Ribbed elastic waistband with slotted drawstring, side pockets.',
                         'DIRTY COINS', 'cotton', '249000', 2),
                        ('Short Jean ABS', 'Brown stitched pants, hip and back pockets. Zip/button front front, high waist for a snug fit.',
                         'SSSTUTTER', 'cotton', '249000', 2),
                        ('Short Kaki Nuguri', 'Pants designed for style, comfort. Flat zip pockets, sophisticated branding and durable drawstring waistband.',
                         'DEGREY', 'cotton', '149000', 2);
select * from Menswears;    
select menswear_id, menswear_name,ifnull(menswear_description, '') as menswear_description,
                         menswear_material, menswear_brand, menswear_price, category_id
                        from Menswears where menswear_id=1;
                        
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




