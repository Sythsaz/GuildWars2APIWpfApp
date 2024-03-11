CREATE TABLE [dbo].[guildwars2api]
(
	[ID] INT  IDENTITY,
	[Name] NVARCHAR(50) NOT NULL , 
    [UrlEndpoint] NVARCHAR(50) NOT NULL, 
    [UrlFull] NVARCHAR(255) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [LastChecked] DATETIME NOT NULL, 
    CONSTRAINT [PK_guildwars2api] PRIMARY KEY ([ID]) 
)
