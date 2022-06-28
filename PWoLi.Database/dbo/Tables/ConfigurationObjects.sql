CREATE TABLE [dbo].[ConfigurationObjects] (
    [CObjectId]      UNIQUEIDENTIFIER NOT NULL,
    [ModuleId]       UNIQUEIDENTIFIER NOT NULL,
    [Name]           VARCHAR (50)     NOT NULL,
    [ObjectType]     UNIQUEIDENTIFIER NOT NULL,
    [ParentObjectId] UNIQUEIDENTIFIER NULL,
    [IsSecret]       BIT              CONSTRAINT [DF_ConfigurationObjects_IsSecret] DEFAULT ((0)) NOT NULL,
    [IsDeleted]      BIT              CONSTRAINT [DF_ConfigurationObjects_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]      VARCHAR (50)     NULL,
    [CreatedOn]      DATETIME         NULL,
    [UpdatedBy]      VARCHAR (50)     NULL,
    [UpdatedOn]      DATETIME         NULL,
    CONSTRAINT [PK_ConfigurationObjects] PRIMARY KEY CLUSTERED ([CObjectId] ASC),
    CONSTRAINT [FK_ConfigurationObjects_ConfigurationObjects1] FOREIGN KEY ([ParentObjectId]) REFERENCES [dbo].[ConfigurationObjects] ([CObjectId]),
    CONSTRAINT [FK_ConfigurationObjects_Modules] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Modules] ([Id]),
    CONSTRAINT [FK_ConfigurationObjects_ObjectType] FOREIGN KEY ([ObjectType]) REFERENCES [dbo].[ObjectType] ([ObjectTypeId])
);



