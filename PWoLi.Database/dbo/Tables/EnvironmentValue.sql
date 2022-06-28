CREATE TABLE [dbo].[EnvironmentValue] (
    [ValueId]             UNIQUEIDENTIFIER NOT NULL,
    [ModuleEnvironmentId] UNIQUEIDENTIFIER NOT NULL,
    [ObjectId]            UNIQUEIDENTIFIER NOT NULL,
    [Value]               NVARCHAR (MAX)   NULL,
    [IsDeleted]           BIT              CONSTRAINT [DF_EnvironmentValue_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]           VARCHAR (50)     NOT NULL,
    [CreatedOn]           DATETIME         NOT NULL,
    [UpdatedBy]           VARCHAR (50)     NULL,
    [UpdatedOn]           DATETIME         NULL,
    CONSTRAINT [PK_EnvironmentValue] PRIMARY KEY CLUSTERED ([ValueId] ASC),
    CONSTRAINT [FK_EnvironmentValue_ConfigurationObjects] FOREIGN KEY ([ObjectId]) REFERENCES [dbo].[ConfigurationObjects] ([CObjectId]),
    CONSTRAINT [FK_EnvironmentValue_ModuleEnvironments] FOREIGN KEY ([ModuleEnvironmentId]) REFERENCES [dbo].[ModuleEnvironments] ([ModuleEnvironmentId])
);







