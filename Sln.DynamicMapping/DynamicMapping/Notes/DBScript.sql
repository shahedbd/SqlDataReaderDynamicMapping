

USE [DevTemp]
GO

/****** Object:  Table [dbo].[PersonalInfo]    Script Date: 10/28/2017 12:16:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PersonalInfo](
	[PersonalInfoID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](100) NULL,
	[LastName] [nchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[City] [nchar](50) NULL,
	[Country] [nchar](100) NULL,
	[MobileNo] [nchar](50) NULL,
	[NID] [nchar](100) NULL,
	[Email] [nchar](100) NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_PersonalInfo] PRIMARY KEY CLUSTERED 
(
	[PersonalInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO















/****** Object:  StoredProcedure [dbo].[sp_personalinfo]    Script Date: 10/28/2017 12:16:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[sp_personalinfo] (
@PersonalInfoID bigint = NULL,
@FirstName nchar(100) = NULL,
@LastName nchar(100) = NULL,
@DateOfBirth datetime = NULL,
@City nchar(50) = NULL,
@Country nchar(100) = NULL,
@MobileNo nchar(50) = NULL,
@NID nchar(100) = NULL,
@Email nchar(100) = NULL,
@Status tinyint = NULL,

@Msg nvarchar(max) = NULL OUT,
@pOptions int)
AS

  --Save personalinfo
  IF (@pOptions = 1)
  BEGIN
  
  INSERT INTO dbo.personalinfo (FirstName,LastName,DateOfBirth,City,Country,MobileNo,NID,Email,Status)
		VALUES (@FirstName, @LastName, @DateOfBirth, @City, @Country, @MobileNo, @NID, @Email, 1)   
		IF @@ROWCOUNT = 0
		BEGIN
		  SET @Msg = 'Warning: No rows were Inserted';
		END
		ELSE
		BEGIN
		  SET @Msg = 'Data Saved Successfully';
		END
  END
  --End of Save personalinfo


  --Update personalinfo 
  IF (@pOptions = 2)
  BEGIN
    UPDATE personalinfo
    SET FirstName = @FirstName,
        LastName = @LastName,
        DateOfBirth = @DateOfBirth,
        City = @City,
        Country = @Country,
        MobileNo = @MobileNo,
        NID = @NID,
        Email = @Email,
        Status = @Status WHERE PersonalInfoID = @PersonalInfoID;

    IF @@ROWCOUNT = 0
    BEGIN
      SET @Msg = 'Warning: No rows were Updated';
    END
    ELSE
    BEGIN
      SET @Msg = 'Data Updated Successfully';
    END
  END
  --End of Update personalinfo 



  --Delete personalinfo
  IF (@pOptions = 3)
  BEGIN
    DELETE FROM personalinfo WHERE PersonalInfoID = @PersonalInfoID;
    SET @Msg = 'Data Deleted Successfully';
  END
  --End of Delete personalinfo 


  --Select All personalinfo 
  IF (@pOptions = 4)
  BEGIN
    SELECT * FROM personalinfo;
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
  --End of Select All personalinfo 


  --Select personalinfo By ID 
  IF (@pOptions = 5)
  BEGIN
    SELECT * FROM personalinfo WHERE PersonalInfoID = @PersonalInfoID;
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
--End of Select personalinfo By ID 





--Bulck data insert:
----truncate 
truncate table PersonalInfo 
---SQL loop insert 
DECLARE @ID int =0; 
DECLARE @StartDate AS DATETIME = '1980-01-01' 
WHILE @ID < 200000 
BEGIN 
insert into PersonalInfo values('First Name ' + CAST(@ID AS nvarchar),'Last Name ' + CAST(@ID AS VARCHAR),dateadd(day,1, @StartDate), 
'City ' + CAST(@ID AS VARCHAR),'Country ' + CAST(@ID AS VARCHAR),ABS(CAST(NEWID() AS binary(12)) % 1000) + 111111, 
ABS(CAST(NEWID() AS binary(12)) % 1000) + 999999999,'email' + CAST(@ID AS nvarchar) +'@gmail.com',1) 
SET @ID = @ID + 1; 
set @StartDate=dateadd(day,1, @StartDate) 
END 
