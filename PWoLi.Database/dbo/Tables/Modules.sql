CREATE TABLE [dbo].[Modules] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (50)    NOT NULL,
    [ParentModule] UNIQUEIDENTIFIER NULL,
    [IsDeleted]    BIT              CONSTRAINT [DF_Modules_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]    NVARCHAR (50)    NOT NULL,
    [CreatedOn]    DATETIME         NOT NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    [ModifiedOn]   DATETIME         NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([Id] ASC)
);



