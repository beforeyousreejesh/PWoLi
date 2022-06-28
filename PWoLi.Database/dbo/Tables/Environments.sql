CREATE TABLE [dbo].[Environments] (
    [EnvironmentId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]            VARCHAR (50)     NOT NULL,
    [IsSecretEnabled] BIT              CONSTRAINT [DF_Environments_IsSecretEnabled] DEFAULT ((0)) NOT NULL,
    [CreatedBy]       NVARCHAR (50)    NOT NULL,
    [CreatedOn]       DATETIME         NOT NULL,
    [ModifiedBy]      NVARCHAR (50)    NULL,
    [ModifiedOn]      DATETIME         NULL,
    CONSTRAINT [PK_Environments] PRIMARY KEY CLUSTERED ([EnvironmentId] ASC)
);



