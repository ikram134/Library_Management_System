create database library;
use library;
create table Books (
id_Book int primary key identity (1,1),
Title varchar (100) not null,
Author varchar(100) not null,
Available_Copies int not null default 1
);
insert into Books ( Title, Author, Available_Copies )
values ('Animal Farm  ', 'George_Orwell', 13);
select * from Books ;

create table Members (
id_Member int primary key identity (1,1),
Full_Name varchar (100) not null,
Email varchar (100) unique,
Join_Date date default getdate()
);
insert into Members values ( 'Leila_Ramsey','ramseyleila@gmail.com','03 October 2025');
select * from Members;

create table Loans (
id_Loan int primary key identity(1,1),
id_Book int not null,
id_Member int not null,
Loan_Date date not null default getdate(),
Due_Date date not null,
Return_Date date null,

foreign key (id_Book)
  references Books(id_Book),
  
foreign key (id_Member)
  references Members(id_Member),

check (Due_Date >= Loan_Date),
check (Return_Date is null  or Return_Date >= Loan_Date)
);
insert into Loans values ( '1', '2', '22 october 2026' , '05 November 2026', '01 november 2026');
select * from Loans;
select * from Books;
select * from Members;

__ JOINING MULTIPLE_TABLES_TO_DISPLAY LOAN DETAILS


select
1.id_Loan,
b.Title,
m.Full_Name,
1.Loan_Date,
1.Due_Date,
1.Return_Date
from Loans l
join Books b
    on l.id_Book = b.id_Book
join Members m 
    on l.id_Member = m.id_Member;

__ OVERDUE LOANS TRACKING

select
id_Loan,
id_Book,
id_Member,
Due_Date,
DATEDIFF(
    day,
    Due_Date,
    Cast(getdate() as date)
    )as days_late
    from Loans
    where Return_Date is null
    and Due_Date < CAST(getdate() as date);

__ LOAN_STATISTICS_AND_REPORTS

   select
   b.Title,
   COUNT( l.id_Loan) as Total_Loans
   from Books b 
   left join Loans l
   on b.id_Book = l.id_Book
   group by b.id_Book, b.Title
   order by Total_Loans desc;

   