CREATE TABLE [dbo].[ObjectType] (
    [ObjectTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Name]         VARCHAR (50)     NOT NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    [CreatedOn]    DATETIME         NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    [ModifiedOn]   DATETIME         NULL,
    CONSTRAINT [PK_ObjectType] PRIMARY KEY CLUSTERED ([ObjectTypeId] ASC)
);

