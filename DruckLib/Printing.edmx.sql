
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/11/2022 00:41:37
-- Generated from EDMX file: C:\Users\Dani1\source\repos\3D-Druck-Verwaltungssystem\DruckLib\Printing.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Printing];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonDrucker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DruckerSet] DROP CONSTRAINT [FK_PersonDrucker];
GO
IF OBJECT_ID(N'[dbo].[FK_Erstellt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DruckauftragSet] DROP CONSTRAINT [FK_Erstellt];
GO
IF OBJECT_ID(N'[dbo].[FK_Bearbeitet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DruckauftragSet] DROP CONSTRAINT [FK_Bearbeitet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[DruckerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DruckerSet];
GO
IF OBJECT_ID(N'[dbo].[DruckauftragSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DruckauftragSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Nachname] nvarchar(max)  NOT NULL,
    [EMail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DruckerSet'
CREATE TABLE [dbo].[DruckerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Besitzer] int  NOT NULL,
    [Bauraum] nvarchar(max)  NOT NULL,
    [VerfuegbareMaterialen] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DruckauftragSet'
CREATE TABLE [dbo].[DruckauftragSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BauteilURL] nvarchar(max)  NOT NULL,
    [ersteller] int  NOT NULL,
    [bearbeiter] int  NULL,
    [gestartet] datetime  NULL,
    [Material] nvarchar(max)  NOT NULL
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

-- Creating foreign key on [Besitzer] in table 'DruckerSet'
ALTER TABLE [dbo].[DruckerSet]
ADD CONSTRAINT [FK_PersonDrucker]
    FOREIGN KEY ([Besitzer])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonDrucker'
CREATE INDEX [IX_FK_PersonDrucker]
ON [dbo].[DruckerSet]
    ([Besitzer]);
GO

-- Creating foreign key on [ersteller] in table 'DruckauftragSet'
ALTER TABLE [dbo].[DruckauftragSet]
ADD CONSTRAINT [FK_Erstellt]
    FOREIGN KEY ([ersteller])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Erstellt'
CREATE INDEX [IX_FK_Erstellt]
ON [dbo].[DruckauftragSet]
    ([ersteller]);
GO

-- Creating foreign key on [bearbeiter] in table 'DruckauftragSet'
ALTER TABLE [dbo].[DruckauftragSet]
ADD CONSTRAINT [FK_Bearbeitet]
    FOREIGN KEY ([bearbeiter])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bearbeitet'
CREATE INDEX [IX_FK_Bearbeitet]
ON [dbo].[DruckauftragSet]
    ([bearbeiter]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------