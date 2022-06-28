CREATE TABLE [dbo].[Role] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (50)    NOT NULL,
    [ParentRole] UNIQUEIDENTIFIER NULL,
    [CreatedBy]  NVARCHAR (50)    NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedBy] NVARCHAR (50)    NULL,
    [ModifiedOn] DATETIME         NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);



