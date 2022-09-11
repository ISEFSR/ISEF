CREATE TABLE [dbo].[log_type]
(
	[Id] tinyint NOT NULL PRIMARY KEY,
	[Name] nvarchar(250) not null,
	[Info] nvarchar(max) not null
)
