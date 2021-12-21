
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/21/2021 18:38:43
-- Generated from EDMX file: C:\Users\Dani1\source\repos\Druck\DruckLibDB\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Druck];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [EMail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DruckerSet'
CREATE TABLE [dbo].[DruckerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Bezeichung] nvarchar(max)  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'DruckauftragSet'
CREATE TABLE [dbo].[DruckauftragSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [gestartet] datetime  NULL,
    [BauteilURL] nvarchar(max)  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DruckerSet'
ALTER TABLE [dbo].[DruckerSet]
ADD CONSTRAINT [PK_DruckerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DruckauftragSet'
ALTER TABLE [dbo].[DruckauftragSet]
ADD CONSTRAINT [PK_DruckauftragSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonId] in table 'DruckerSet'
ALTER TABLE [dbo].[DruckerSet]
ADD CONSTRAINT [FK_PersonDrucker]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonDrucker'
CREATE INDEX [IX_FK_PersonDrucker]
ON [dbo].[DruckerSet]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'DruckauftragSet'
ALTER TABLE [dbo].[DruckauftragSet]
ADD CONSTRAINT [FK_Erstellt]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Erstellt'
CREATE INDEX [IX_FK_Erstellt]
ON [dbo].[DruckauftragSet]
    ([PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------