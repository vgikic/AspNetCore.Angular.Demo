CREATE TABLE [idm].[Users] (
    [Id]                INT                IDENTITY (1, 1) NOT NULL,
    [UserName]          NVARCHAR (256)     NOT NULL,
    [Email]             NVARCHAR (256)     NOT NULL,
    [NormalizedEmail]   NVARCHAR (256)     NOT NULL,
    [EmailConfirmed]    BIT                NOT NULL,
    [PasswordHash]      NVARCHAR (MAX)     NULL,
    [LockoutEnd]        DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]    BIT                CONSTRAINT [DF_Users_LockoutEnabled] DEFAULT ((1)) NOT NULL,
    [AccessFailedCount] INT                NOT NULL,
    [ConcurrencyStamp]  NVARCHAR (MAX)     CONSTRAINT [DF_Users_ConcurrencyStamp] DEFAULT (newid()) NOT NULL,
    [SecurityStamp]     NVARCHAR (MAX)     CONSTRAINT [DF_Users_SecurityStamp] DEFAULT (newid()) NOT NULL,
    [LastLogin]         DATETIME2 (7)      NULL,
    [Created]           DATETIME2 (7)      CONSTRAINT [DF_Users_Created] DEFAULT (getutcdate()) NOT NULL,
    [IsActive]          BIT                NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

