USE master
GO

IF EXISTS (SELECT * FROM SYS.DATABASES WHERE name = 'Petshop')
	DROP DATABASE Petshop

CREATE DATABASE Petshop
	ON PRIMARY (
		NAME = StudentsData, 
		FILENAME = 'C:\Users\Public\Petshop.mdf', SIZE = 10 MB, MAXSIZE = 100, FILEGROWTH = 10)
		LOG ON 
		(
			NAME = StudentsLog,
			FILENAME = 'C:\Users\Public\PetshopLog.ldf', SIZE = 10 MB, MAXSIZE = 100, FILEGROWTH = 10
		)

GO

USE Petshop
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Admins')
	DROP TABLE Admins

CREATE TABLE Admins (
	id_admin INT PRIMARY KEY IDENTITY(1,1),
	surname NVARCHAR(50) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	patronymic NVARCHAR(50),
	e_mail NVARCHAR(50) NOT NULL,
	phone_number NVARCHAR(12) NOT NULL,
	login NVARCHAR(50) NOT NULL,
	password NVARCHAR(50) NOT NULL
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Clients')
	DROP TABLE Clients

CREATE TABLE Clients (
	id_client INT PRIMARY KEY IDENTITY(1,1),
	surname NVARCHAR(50) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	patronymic NVARCHAR(50),
	e_mail NVARCHAR(50) NOT NULL,
	phone_number NVARCHAR(12) NOT NULL,
	login NVARCHAR(50) NOT NULL,
	password NVARCHAR(50) NOT NULL
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Categories')
	DROP TABLE Categories

CREATE TABLE Categories (
	id_category INT PRIMARY KEY IDENTITY(1,1),
	category NVARCHAR(50) NOT NULL
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Firms')
	DROP TABLE Firms

CREATE TABLE Firms (
	id_firm INT PRIMARY KEY IDENTITY(1,1),
	firm NVARCHAR(50) NOT NULL
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Products')
	DROP TABLE Products

CREATE TABLE Products (
	id_product INT PRIMARY KEY IDENTITY(1,1),
	product NVARCHAR(150) NOT NULL,
	id_category INT NOT NULL,
	id_firm INT NOT NULL,
	price MONEY NOT NULL,
	characteristics NVARCHAR(MAX),
	picture NVARCHAR(MAX),	
	FOREIGN KEY(id_category) REFERENCES Categories(id_category) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(id_firm) REFERENCES Firms(id_firm) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT chk_products_chrcts CHECK (ISJSON(characteristics) = 1)
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Orders')
	DROP TABLE Orders

CREATE TABLE Orders (
	id_order INT PRIMARY KEY IDENTITY(1,1),
	date_of_order DATETIME,
	date_of_issue DATETIME,
	delivery_address NVARCHAR(60),
	id_client INT NOT NULL,
	FOREIGN KEY (id_client) REFERENCES Clients(id_client) ON DELETE CASCADE ON UPDATE CASCADE
)
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name = 'Products_in_order')
	DROP TABLE Products_in_order

CREATE TABLE Products_in_order (
	id_product INT,
	id_order INT,
	count_of_products INT
	FOREIGN KEY(id_product) REFERENCES Products(id_product) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(id_order) REFERENCES Orders(id_order) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT PK_prodnord PRIMARY KEY CLUSTERED(id_product, id_order)
)
GO

INSERT INTO dbo.Firms (firm) VALUES ('�������');
INSERT INTO dbo.Firms (firm) VALUES ('Royal Canin');
INSERT INTO dbo.Firms (firm) VALUES ('Polly Pet Toys');
INSERT INTO dbo.Firms (firm) VALUES ('Purina One');
INSERT INTO dbo.Firms (firm) VALUES ('�������');
INSERT INTO dbo.Firms (firm) VALUES ('Petmax');

INSERT INTO dbo.Categories (category) VALUES ('�����������');
INSERT INTO dbo.Categories (category) VALUES ('�����');
INSERT INTO dbo.Categories (category) VALUES ('�������');
INSERT INTO dbo.Categories (category) VALUES ('������');
INSERT INTO dbo.Categories (category) VALUES ('������');

INSERT INTO dbo.Admins (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '������','�������','fomichev@mail.ru','79554608712', 'admin1', 'E00CF25AD42683B3DF678C61F42C6BDA')
INSERT INTO dbo.Admins (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '����','�����','kulikov@mail.ru','79544623701', 'admin2', 'C84258E9C39059A89AB77D846DDAB909')
INSERT INTO dbo.Admins (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('������', '�����','������','suslov@mail.ru','79164142154', 'admin3', '32CACB2F994F6B42183A1300D9A3E8D6')
INSERT INTO dbo.Admins (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('��������', '������','�����������','bogdanov@mail.ru','79594322154', 'admin4', 'FC1EBC848E31E0A68E868432225E3C82')
INSERT INTO dbo.Admins (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�����', '������','���������','belov@mail.ru','79543232215', 'admin5', '26A91342190D515231D7238B0C5438E1')

INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('������', '�����','���������','avdeev@mail.ru','79554608712', 'avdeev@mail.ru', '871EADAC6D99FC29742CDD82CC9AA74E')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '�������','����������','nikulin@mail.ru','79544623701', 'nikulin@mail.ru', '640D569455F0A46B2AF62F7400E23C64')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('����������', '�������','���������','mitro@mail.ru','79164142154', 'mitro@mail.ru', '4F092A49A122F2A061B5761B2EAF5ED1')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '����','��������������','petrova@mail.ru','79594322154', 'petrova@mail.ru', 'EDB964911D3C1ABE265D6F10548D5A11')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '�����','����������','zloba@mail.ru','79543232215', 'zloba@mail.ru', '35111B7928986053E79FFE856E727953')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('������', '������','��������','isacov@mail.ru','79543231234', 'isacov@mail.ru', 'DA14DBE844E16A1A68E1A2E966DE94C1')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('������', '��������','���������','larina@mail.ru','79543239999', 'larina@mail.ru', 'B4D7F3581E4A3B10D1F51584244FE9F4')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('��������', '�����','��������������','nikulina@mail.ru','79541543324', 'nikulina@mail.ru', '0F5CB8A59C577E91140A435FE07A3AE8')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('��������', '���������','�������������','vorobey@mail.ru','79173536215', 'vorobey@mail.ru', '8C2EAFD5D5C70D3756CFC379DA872393')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('��������', '�����','��������','davydova@mail.ru','79544234224', 'davydova@mail.ru', 'FB0571E1ED949B418F9A8B7A5D562117')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('������', '�������','����������','pavlov@mail.ru','79543142345', 'pavlov@mail.ru', '12E219338D163A8B74E8CED3F0A6B4A3')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '��������','������������','ivanova@mail.ru','79543632244', 'ivanova@mail.ru', '0E0A8AE10C3F471A6443F6651647DAB5')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('��������', '�����','��������','danilova@mail.ru','79521533113', 'danilova@mail.ru', 'B3B2CCDBD415C9BEEB5BAF63E70B6EDD')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '�������','������������','romanov@mail.ru','79543123581', 'romanov@mail.ru', '2BA2F8A53E6861F8B754EF4F5C8EE471')
INSERT INTO dbo.Clients (surname, name, patronymic, e_mail, phone_number, login, password) VALUES ('�������', '������','��������','dorohov@mail.ru','79543257563', 'dorohov@mail.ru', '8BCAF23483C88FB77DA10B4FAC252E29')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('Royal Canin British Shorthair Adult', 2,2,8798, 
'{"���":"���� �����","����":"�����","��� (� �������)":8000,"���� �������� (���)":365,"������������ ":"�����"}',
'C:\Users\�������\Desktop\��������\source\images\Royal Canin 8kg.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('ROYAL CANIN Maine Coon', 2,2,2247,
'{"���":"���� �������","����":"�����","��� (� �������)":2000,"���� �������� (���)":730,"������������ ":"�����"}',
'C:\Users\�������\Desktop\��������\source\images\Canin Maine Coon.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('Purina ONE ��� ����� � �������� � �������� �������', 2,4,3124,
'{"���":"���� �����","����":"������� � �����","��� (� �������)":5000,"���� �������� (���)":540,"������������ ":"�����"}',
'C:\Users\�������\Desktop\��������\source\images\Purina One Cats.png')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('Royal Canin Mini Puppy', 2,2,534,
'{"���":"���� �����","����":"�����","��� (� �������)":800,"���� �������� (���)":365,"������������ ":"������"}',
'C:\Users\�������\Desktop\��������\source\images\Royal Canin Puppy.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('����� ���� ��� ����� Purina ONE ��� ������� � ������� �����', 2,4,2444, 
'{"���":"���� �����","����":"��������","��� (� �������)":10000,"���� �������� (���)":540,"������������ ":"������"}', 
'C:\Users\�������\Desktop\��������\source\images\Purina One Dogs.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('����������� ���� ������ �����������', 1,5,3420,
'{"���":"����","��� (� ����������)":20,"������������ ":"�����"}', 'C:\Users\�������\Desktop\��������\source\images\filler1.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('����������� �������������', 1,5,2400,
'{"���":"������������� �������","��� (� ����������)":15,"������������ ":"�����"}',
'C:\Users\�������\Desktop\��������\source\images\filler2.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('����������� ������������� �������', 1,5,400,
'{"���":"�������������","��� (� ����������)":20,"������������ ":"�����"}',
'C:\Users\�������\Desktop\��������\source\images\filler.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('��� �� ����� ����s', 3,3,370,
'{"��������":"��������� �����","������ ����":"9 ��","�����":"25 ��","����":"�����/�������"}',
'C:\Users\�������\Desktop\��������\source\images\ball.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('�����������', 3,3,450,
'{"��������":"��������������� ������","������":"13,5 �� �����","����":"���������"}',
'C:\Users\�������\Desktop\��������\source\images\triangle.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('����������� ������� ��������� ������', 3,3,680,
'{"��������":"�������� ������","�������":"2 ��","�����":"17 ��"}',
'C:\Users\�������\Desktop\��������\source\images\coffeestick.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('���������� ����', 4,1,5900,
'{"������":"55 � 55 � 126 ��","���������":"55 x 55 ��","�������":"10 ��","������� �����":"55 x 55 ��","�����":"35 � 35 � 30 ��","�������": "40 � 14,5 ��"}',
'C:\Users\�������\Desktop\��������\source\images\house1.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('���������� A924-2', 4,1,5400,
'{"������":"60 x 60 x 170 ��","������ �����":"60 � 30 � 30 ��","������� �����":"30 � 30 � 30 ��","������� ������":"35 ��","�������":"10 ��"}', 
'C:\Users\�������\Desktop\��������\source\images\house2.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('���������� �����', 4,1,5400,
'{"������":"60 x 60 x 170 ��","������� ������":"35 ��","�������":"10 ��"}', 
'C:\Users\�������\Desktop\��������\source\images\house3.jpg')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('Rungo ������ �� ������ ��� �����', 5,6,1500,
'{"���":"������","��������":"�����","���":"�������"}', 
'C:\Users\�������\Desktop\��������\source\images\jacket.png')

INSERT INTO dbo.Products (product, id_category, id_firm, price, characteristics, picture) VALUES ('Petmax ������� ��������� � ����� �� �������� ��� �����', 5,6,799,
'{"���":"�������","��������":"������","����":"������"}', 
'C:\Users\�������\Desktop\��������\source\images\boots.jpg')

INSERT INTO dbo.Orders (date_of_order, date_of_issue, delivery_address, id_client) VALUES ('2023-09-03 16:28', NULL, '�. ������� ��. ��������� 5 ��. 175', 1)
INSERT INTO dbo.Orders (date_of_order, date_of_issue, delivery_address, id_client) VALUES ('2023-12-03 14:25', '2023-14-03 09:20', '�. ������� ��. ������� 3 ��. 16', 3)
INSERT INTO dbo.Orders (date_of_order, date_of_issue, delivery_address, id_client) VALUES (NULL, NULL, NULL, 2)

INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (1, 1, 2)
INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (12, 1, 1)
INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (5, 2, 1)
INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (9, 2, 3)
INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (7, 3, 1)
INSERT INTO dbo.Products_in_order (id_product, id_order, count_of_products) VALUES (16, 3, 1)

GO



