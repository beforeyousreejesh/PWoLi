CREATE TABLE [dbo].[ModuleEnvironments] (
    [ModuleEnvironmentId] UNIQUEIDENTIFIER NOT NULL,
    [ModuleId]            UNIQUEIDENTIFIER NOT NULL,
    [EnvironmentId]       UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]           BIT              CONSTRAINT [DF_ModuleEnvironments_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]           NVARCHAR (50)    NOT NULL,
    [CreatedOn]           DATETIME         NOT NULL,
    [ModifiedBy]          NVARCHAR (50)    NULL,
    [ModifiedOn]          DATETIME         NULL,
    CONSTRAINT [PK_ModuleEnvironments] PRIMARY KEY CLUSTERED ([ModuleEnvironmentId] ASC)
);



