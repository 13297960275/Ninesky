CREATE TABLE [dbo].[Users] (
    [UserID]           INT            IDENTITY (1, 1) NOT NULL,
    [UserName]         NVARCHAR (20)  NOT NULL,
    [DisplayName]      NVARCHAR (20)  NOT NULL,
    [Password]         NVARCHAR (MAX) NOT NULL,
    [Email]            NVARCHAR (MAX) NOT NULL,
    [Status]           INT            NOT NULL,
    [RegistrationTime] DATETIME2       NULL,
    [LoginTime]        DATETIME2       NULL,
    [LoginIP]          DATETIME2       NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

