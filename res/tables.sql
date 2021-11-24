create database MicroElectronsDB;
use MicroElectronsDB;

create table Post(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table EmployeeStatus(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table Employee(
    id int primary key auto_increment,
    lastName nvarchar(50) not null,
    firstName nvarchar(50) not null,
    patronymic nvarchar(50) null,
    birthday date not null,
    postId int not null,
    foreign key (postId) references Post (id),
    statusId int not null,
    foreign key (statusId) references EmployeeStatus (id)
);

create table User(
    id int primary key auto_increment,
    login varchar(50) not null unique,
    password varchar(64) not null,
    employeeId int not null,
    foreign key (employeeId) references Employee (id)
);

create table Labor(
    id int primary key auto_increment,
    salary double not null,
    dateConfirm date not null,
    employeeId int not null,
    foreign key (employeeId) references Employee (id)
);

create table Dismissal(
    id int primary key auto_increment,
    dateConfirm date not null,
    employeeId int not null,
    foreign key (employeeId) references Employee (id)
);

create table Holiday(
    id int primary key auto_increment,
    dateStart date not null,
    dateEnd date not null,
    employeeId int not null,
    foreign key (employeeId) references Employee (id)
);

create table Visitor(
    id int primary key auto_increment,
    lastName nvarchar(50) not null,
    firstName nvarchar(50) not null,
    patronymic nvarchar(50) null,
    passport varchar(10) not null
);

create table VisitorJournal(
    id int primary key auto_increment,
    dateTimeEntry datetime not null,
    dateTimeExit datetime null,
    employeeEntryId int not null,
    foreign key (employeeEntryId) references Employee (id),
    employeeExitId int null,
    foreign key (employeeExitId) references Employee (id),
    visitorId int not null,
    foreign key (visitorId) references Visitor (id)
);

create table ProductCategory(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table Product(
    id int primary key auto_increment,
    name nvarchar(100) not null,
    categoryId int not null,
    foreign key (categoryId) references ProductCategory (id)
);

create table StorageMethod(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table Warehouse(
    id int primary key auto_increment,
    quantity int default 0,
    productId int not null,
    foreign key (productId) references Product (id),
    storageMethodId int not null,
    foreign key (storageMethodId) references StorageMethod (id)
);

create table TaskStatus(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table Task(
    id int primary key auto_increment,
    dateStart date not null,
    dateEnd date null,
    dateDeadline date not null,
    quantity int not null,
    warehouseId int not null,
    foreign key (warehouseId) references Warehouse (id),
    employeeId int not null,
    foreign key (employeeId) references Employee (id),
    statusId int not null,
    foreign key (statusId) references TaskStatus (id)
);

create table Counterparty(
    id int primary key auto_increment,
    name nvarchar(100) not null,
    tin varchar(10) not null,
    address nvarchar(200) not null,
    bic varchar(9)
);

create table Supply(
    id int primary key auto_increment,
    isSell bool default 0,
    dateSupply date not null,
    counterpartyId int not null,
    foreign key (counterpartyId) references Counterparty (id)
);

create table SupplyCompos(
    id int primary key auto_increment,
    quantity int not null,
    summa double not null,
    productId int not null,
    foreign key (productId) references Product (id),
    supplyId int not null,
    foreign key (supplyId) references Supply (id)
);

create table OperationType(
    id int primary key auto_increment,
    name nvarchar(100) not null
);

create table ComeJournal(
    id int primary key auto_increment,
    subjectName nvarchar(100) not null,
    quantity int not null,
    dateTimeConfirm datetime not null,
    isCome bool default 1,
    operationId int not null,
    foreign key (operationId) references OperationType (id)
);