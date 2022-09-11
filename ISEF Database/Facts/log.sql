CREATE TABLE [dbo].[log]
(
	[Id] INT NOT NULL PRIMARY KEY identity(0,1),
	[CreatedBy] nvarchar(250) not null,
	[CreatedDate] datetime2 not null default(GETDATE()),
	[LogTitle] nvarchar(200) not null,
	[LogInfo] nvarchar(max) not null,
	[LogType] tinyint not null default(0) foreign key references dbo.log_type(Id)
)
