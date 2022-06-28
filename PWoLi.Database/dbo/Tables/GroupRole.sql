CREATE TABLE [dbo].[GroupRole] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [GroupName]  NVARCHAR (50)    NOT NULL,
    [ModuleId]   UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]  BIT              CONSTRAINT [DF_GroupRole_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]  NVARCHAR (50)    NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedBy] NVARCHAR (50)    NULL,
    [ModifiedOn] DATETIME         NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);



