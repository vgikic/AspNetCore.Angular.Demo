CREATE TABLE [idm].[Roles] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [NormalizedName]   NVARCHAR (256) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) CONSTRAINT [DF_Roles_ConcurrencyStamp] DEFAULT (newid()) NOT NULL,
    [Discriminator]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

