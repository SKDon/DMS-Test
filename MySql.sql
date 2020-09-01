USE Test
Go

CREATE TABLE Student
(
Id int IDENTITY (1, 1) NOT NULL,
Name nvarchar(10),
Email nvarchar(50),
Contact nvarchar(20),
Class_Id int,
CONSTRAINT PK_Student PRIMARY KEY NONCLUSTERED (Id),
);

Go

CREATE TABLE Class
(
Id int IDENTITY (1, 1) NOT NULL,
Name nvarchar(10) NULL,
CONSTRAINT PK_Class PRIMARY KEY NONCLUSTERED (Id),
);

Go

ALTER TABLE Student    
ADD CONSTRAINT FK_Student_Class FOREIGN KEY (Class_Id)     
    REFERENCES Class (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE    
;    
GO    

