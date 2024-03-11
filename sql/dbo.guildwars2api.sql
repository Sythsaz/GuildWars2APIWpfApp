CREATE TABLE [dbo].[guildwars2api] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Path]        NVARCHAR (50) NOT NULL,
    [Keys]        NVARCHAR (5) NOT NULL,
    [Name]        NVARCHAR (15)  NOT NULL,
    CONSTRAINT [PK_guildwars2api] PRIMARY KEY CLUSTERED ([ID] ASC)
);