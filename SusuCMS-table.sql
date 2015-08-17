
USE [SusuCMS]
GO
/****** Object:  Role [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 06/14/2012 23:22:00 ******/
CREATE ROLE [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 06/14/2012 23:22:00 ******/
CREATE SCHEMA [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess] AUTHORIZATION [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
GO
/****** Object:  FullTextCatalog [PageIndex]    Script Date: 06/14/2012 23:22:00 ******/
CREATE FULLTEXT CATALOG [PageIndex]WITH ACCENT_SENSITIVITY = ON
AUTHORIZATION [dbo]
GO
/****** Object:  Table [dbo].[Shop_Product]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop_Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Shop_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_Mail]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_Mail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[From] [nvarchar](250) NOT NULL,
	[FromName] [nvarchar](50) NULL,
	[To] [nvarchar](250) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[IsSent] [bit] NOT NULL,
	[ReplyTo] [nvarchar](250) NOT NULL,
	[SiteId] [int] NOT NULL,
 CONSTRAINT [PK_Cms_MailQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_SystemLog]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_SystemLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cms_SystemLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_Site]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cms_Site](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[Theme] [nvarchar](50) NOT NULL,
	[CultureName] [varchar](20) NOT NULL,
	[AnalyticsCode] [nvarchar](2000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Template] [nvarchar](50) NOT NULL,
	[EnableCustomCss] [bit] NOT NULL,
	[UrlsJson] [nvarchar](1000) NOT NULL,
	[MetaFieldsJson] [nvarchar](max) NULL,
	[RedirectToMainUrl] [bit] NOT NULL,
 CONSTRAINT [PK__Website__3214EC0703317E3D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cms_AdminRole]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cms_AdminRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Permissions] [binary](12) NOT NULL,
	[IsRoot] [bit] NOT NULL,
 CONSTRAINT [PK_Cms_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Blog_Tag]    Script Date: 06/14/2012 23:22:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[SiteId] [int] NOT NULL,
	[ArticleCount] [int] NOT NULL,
 CONSTRAINT [PK_ArticleTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS

         BEGIN 
             UPDATE dbo.AspNet_SqlCacheTablesForChangeNotification WITH (ROWLOCK) SET changeId = changeId + 1 
             WHERE tableName = @tableName
         END
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         BEGIN TRAN
         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Remove the table-row from the notification table */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             DELETE FROM dbo.AspNet_SqlCacheTablesForChangeNotification WHERE tableName = @tableName 

         /* Remove the trigger */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
             EXEC('DROP TRIGGER ' + @fullTriggerName) 

         COMMIT TRAN
         END
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         DECLARE @canonTableName NVARCHAR(3000) 
         DECLARE @quotedTableName NVARCHAR(3000) 

         /* Create the trigger name */ 
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Create the cannonicalized table name for trigger creation */ 
         /* Do not touch it if the name contains other delimiters */ 
         IF (CHARINDEX('.', @tableName) <> 0 OR 
             CHARINDEX('[', @tableName) <> 0 OR 
             CHARINDEX(']', @tableName) <> 0) 
             SET @canonTableName = @tableName 
         ELSE 
             SET @canonTableName = '[' + @tableName + ']' 

         /* First make sure the table exists */ 
         IF (SELECT OBJECT_ID(@tableName, 'U')) IS NULL 
         BEGIN 
             RAISERROR ('00000001', 16, 1) 
             RETURN 
         END 

         BEGIN TRAN
         /* Insert the value into the notification table */ 
         IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (NOLOCK) WHERE tableName = @tableName) 
             IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (TABLOCKX) WHERE tableName = @tableName) 
                 INSERT  dbo.AspNet_SqlCacheTablesForChangeNotification 
                 VALUES (@tableName, GETDATE(), 0)

         /* Create the trigger */ 
         SET @quotedTableName = QUOTENAME(@tableName, '''') 
         IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
                 EXEC('CREATE TRIGGER ' + @fullTriggerName + ' ON ' + @canonTableName +'
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N' + @quotedTableName + '
                       END
                       ')
         COMMIT TRAN
         END
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure] 
         AS
         SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCachePollingStoredProcedure]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCachePollingStoredProcedure] AS
         SELECT tableName, changeId FROM dbo.AspNet_SqlCacheTablesForChangeNotification
         RETURN 0
GO
/****** Object:  Table [dbo].[Blog_Category]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[SiteId] [int] NOT NULL,
	[ArticleCount] [int] NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_cms_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog_Contact]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Content] [nvarchar](3000) NOT NULL,
	[SiteId] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Blog_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog_ArticleDraft]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_ArticleDraft](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Time] [datetime] NOT NULL,
	[SiteId] [int] NOT NULL,
 CONSTRAINT [PK_Blog_ArticleDraft] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog_Article]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IsOnline] [bit] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[SiteId] [int] NOT NULL,
	[Hits] [int] NOT NULL,
	[Author] [nvarchar](50) NULL,
	[Slug] [nvarchar](250) NULL,
	[CommentCount] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[EnableComment] [bit] NOT NULL,
	[MetaFieldsJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_Page]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cms_Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[Name] [varchar](30) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[IsOnline] [bit] NOT NULL,
	[Url] [nvarchar](250) NOT NULL,
	[Template] [nvarchar](50) NOT NULL,
	[HtmlTitle] [nvarchar](250) NULL,
	[MetaFieldsJson] [nvarchar](max) NULL,
 CONSTRAINT [PK__Page__3214EC07239E4DCF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cms_Menu]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](250) NULL,
	[DisplayOrder] [int] NOT NULL,
	[ParentId] [int] NULL,
	[IsOnline] [bit] NOT NULL,
	[OpenInNewWindow] [bit] NOT NULL,
 CONSTRAINT [PK_Cms_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_SiteLabel]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_SiteLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](1000) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Value] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Cms_Label] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_SiteData]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_SiteData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Key] [nvarchar](250) NOT NULL,
	[ModuleName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Cms_SiteDictionary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_AdminUser]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_AdminUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Salt] [nvarchar](500) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsRoot] [bit] NOT NULL,
 CONSTRAINT [PK__User__3214EC077F60ED59] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_Widget]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cms_Widget](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[DataJson] [nvarchar](max) NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_Cms_Widgets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cms_WidgetLabel]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_WidgetLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](1000) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[WidgetId] [int] NOT NULL,
 CONSTRAINT [PK_Cms_WidgetLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cms_PageWidget]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cms_PageWidget](
	[PageId] [int] NOT NULL,
	[WidgetId] [int] NOT NULL,
	[ZoneName] [varchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_Cms_PageWidgets_1] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC,
	[WidgetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cms_PageLabel]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cms_PageLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](1000) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[PageId] [int] NOT NULL,
 CONSTRAINT [PK_Cms_PageLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog_Picture]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_Picture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Path] [nvarchar](250) NOT NULL,
	[PageId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog_Comment]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Blog_Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[IP] [varchar](30) NULL,
	[Author] [nvarchar](50) NULL,
	[Email] [varchar](250) NULL,
	[Url] [nvarchar](250) NULL,
	[Status] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[UserAgent] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Blog_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Blog_ArticleTag]    Script Date: 06/14/2012 23:22:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog_ArticleTag](
	[ArticleId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_Blog_ArticleTags] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Cms_Sites_MinifyResource]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_Site] ADD  CONSTRAINT [DF_Cms_Sites_MinifyResource]  DEFAULT ((0)) FOR [AnalyticsCode]
GO
/****** Object:  Default [DF_Cms_Site_IsDeleted]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_Site] ADD  CONSTRAINT [DF_Cms_Site_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Cms_Site_EnableCustomCss]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_Site] ADD  CONSTRAINT [DF_Cms_Site_EnableCustomCss]  DEFAULT ((0)) FOR [EnableCustomCss]
GO
/****** Object:  Default [DF_Cms_Site_RedirectToMainUrl]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_Site] ADD  CONSTRAINT [DF_Cms_Site_RedirectToMainUrl]  DEFAULT ((0)) FOR [RedirectToMainUrl]
GO
/****** Object:  Default [DF_Cms_Roles_Permission]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_AdminRole] ADD  CONSTRAINT [DF_Cms_Roles_Permission]  DEFAULT ((0)) FOR [Permissions]
GO
/****** Object:  Default [DF_Cms_Role_IsRoot]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Cms_AdminRole] ADD  CONSTRAINT [DF_Cms_Role_IsRoot]  DEFAULT ((0)) FOR [IsRoot]
GO
/****** Object:  Default [DF_ArticleTag_Count]    Script Date: 06/14/2012 23:22:03 ******/
ALTER TABLE [dbo].[Blog_Tag] ADD  CONSTRAINT [DF_ArticleTag_Count]  DEFAULT ((0)) FOR [ArticleCount]
GO
/****** Object:  Default [DF_Blog_Categories_IsOnline]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Category] ADD  CONSTRAINT [DF_Blog_Categories_IsOnline]  DEFAULT ((0)) FOR [IsOnline]
GO
/****** Object:  Default [DF_cms_Article_Hits]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Article] ADD  CONSTRAINT [DF_cms_Article_Hits]  DEFAULT ((0)) FOR [Hits]
GO
/****** Object:  Default [DF_Blog_Articles_CommentCount]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Article] ADD  CONSTRAINT [DF_Blog_Articles_CommentCount]  DEFAULT ((0)) FOR [CommentCount]
GO
/****** Object:  Default [DF_Blog_Article_EnableComments]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Article] ADD  CONSTRAINT [DF_Blog_Article_EnableComments]  DEFAULT ((0)) FOR [EnableComment]
GO
/****** Object:  Default [DF_Cms_Navigations_DisplayOrder]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Menu] ADD  CONSTRAINT [DF_Cms_Navigations_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
/****** Object:  Default [DF_Cms_Navigations_IsOnline]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Menu] ADD  CONSTRAINT [DF_Cms_Navigations_IsOnline]  DEFAULT ((0)) FOR [IsOnline]
GO
/****** Object:  Default [DF_Cms_Menu_OpenInNewWindow]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Menu] ADD  CONSTRAINT [DF_Cms_Menu_OpenInNewWindow]  DEFAULT ((0)) FOR [OpenInNewWindow]
GO
/****** Object:  Default [DF_Cms_Users_Status]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_AdminUser] ADD  CONSTRAINT [DF_Cms_Users_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_Cms_AdminUser_IsRoot]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_AdminUser] ADD  CONSTRAINT [DF_Cms_AdminUser_IsRoot]  DEFAULT ((0)) FOR [IsRoot]
GO
/****** Object:  Default [DF_Cms_Widget_IsSystem]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Widget] ADD  CONSTRAINT [DF_Cms_Widget_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
GO
/****** Object:  Default [DF_Cms_PageWidgets_DisplayOrder]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_PageWidget] ADD  CONSTRAINT [DF_Cms_PageWidgets_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
/****** Object:  Default [DF_Blog_Comments_UserId]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Comment] ADD  CONSTRAINT [DF_Blog_Comments_UserId]  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF_Blog_Comments_Status]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Comment] ADD  CONSTRAINT [DF_Blog_Comments_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  ForeignKey [FK_Blog_Categories_Blog_Categories1]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Category]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Categories_Blog_Categories1] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Blog_Category] ([Id])
GO
ALTER TABLE [dbo].[Blog_Category] CHECK CONSTRAINT [FK_Blog_Categories_Blog_Categories1]
GO
/****** Object:  ForeignKey [FK_Blog_Article_Blog_Category]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Article]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Article_Blog_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Blog_Category] ([Id])
GO
ALTER TABLE [dbo].[Blog_Article] CHECK CONSTRAINT [FK_Blog_Article_Blog_Category]
GO
/****** Object:  ForeignKey [FK__Page__WebsiteId__25869641]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Page]  WITH CHECK ADD  CONSTRAINT [FK__Page__WebsiteId__25869641] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Cms_Site] ([Id])
GO
ALTER TABLE [dbo].[Cms_Page] CHECK CONSTRAINT [FK__Page__WebsiteId__25869641]
GO
/****** Object:  ForeignKey [FK_cms_Page_cms_Page1]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Page]  WITH CHECK ADD  CONSTRAINT [FK_cms_Page_cms_Page1] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Cms_Page] ([Id])
GO
ALTER TABLE [dbo].[Cms_Page] CHECK CONSTRAINT [FK_cms_Page_cms_Page1]
GO
/****** Object:  ForeignKey [FK_Cms_Menus_Cms_Menus]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Menu]  WITH CHECK ADD  CONSTRAINT [FK_Cms_Menus_Cms_Menus] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Cms_Menu] ([Id])
GO
ALTER TABLE [dbo].[Cms_Menu] CHECK CONSTRAINT [FK_Cms_Menus_Cms_Menus]
GO
/****** Object:  ForeignKey [FK_Cms_Menus_Cms_Sites]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Menu]  WITH CHECK ADD  CONSTRAINT [FK_Cms_Menus_Cms_Sites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Cms_Site] ([Id])
GO
ALTER TABLE [dbo].[Cms_Menu] CHECK CONSTRAINT [FK_Cms_Menus_Cms_Sites]
GO
/****** Object:  ForeignKey [FK_Cms_Label_Cms_Site]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_SiteLabel]  WITH CHECK ADD  CONSTRAINT [FK_Cms_Label_Cms_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Cms_Site] ([Id])
GO
ALTER TABLE [dbo].[Cms_SiteLabel] CHECK CONSTRAINT [FK_Cms_Label_Cms_Site]
GO
/****** Object:  ForeignKey [FK_Cms_SiteDictionary_Cms_Site]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_SiteData]  WITH CHECK ADD  CONSTRAINT [FK_Cms_SiteDictionary_Cms_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Cms_Site] ([Id])
GO
ALTER TABLE [dbo].[Cms_SiteData] CHECK CONSTRAINT [FK_Cms_SiteDictionary_Cms_Site]
GO
/****** Object:  ForeignKey [FK_Cms_AdminUser_Cms_AdminRole]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_AdminUser]  WITH CHECK ADD  CONSTRAINT [FK_Cms_AdminUser_Cms_AdminRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Cms_AdminRole] ([Id])
GO
ALTER TABLE [dbo].[Cms_AdminUser] CHECK CONSTRAINT [FK_Cms_AdminUser_Cms_AdminRole]
GO
/****** Object:  ForeignKey [FK_Cms_Widgets_Cms_Sites]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_Widget]  WITH CHECK ADD  CONSTRAINT [FK_Cms_Widgets_Cms_Sites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Cms_Site] ([Id])
GO
ALTER TABLE [dbo].[Cms_Widget] CHECK CONSTRAINT [FK_Cms_Widgets_Cms_Sites]
GO
/****** Object:  ForeignKey [FK_Cms_WidgetLabel_Cms_Widget]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_WidgetLabel]  WITH CHECK ADD  CONSTRAINT [FK_Cms_WidgetLabel_Cms_Widget] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[Cms_Widget] ([Id])
GO
ALTER TABLE [dbo].[Cms_WidgetLabel] CHECK CONSTRAINT [FK_Cms_WidgetLabel_Cms_Widget]
GO
/****** Object:  ForeignKey [FK_Cms_PageWidgets_Cms_Pages]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_PageWidget]  WITH CHECK ADD  CONSTRAINT [FK_Cms_PageWidgets_Cms_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Cms_Page] ([Id])
GO
ALTER TABLE [dbo].[Cms_PageWidget] CHECK CONSTRAINT [FK_Cms_PageWidgets_Cms_Pages]
GO
/****** Object:  ForeignKey [FK_Cms_PageWidgets_Cms_Widgets]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_PageWidget]  WITH CHECK ADD  CONSTRAINT [FK_Cms_PageWidgets_Cms_Widgets] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[Cms_Widget] ([Id])
GO
ALTER TABLE [dbo].[Cms_PageWidget] CHECK CONSTRAINT [FK_Cms_PageWidgets_Cms_Widgets]
GO
/****** Object:  ForeignKey [FK_Cms_PageLabel_Cms_Page]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Cms_PageLabel]  WITH CHECK ADD  CONSTRAINT [FK_Cms_PageLabel_Cms_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Cms_Page] ([Id])
GO
ALTER TABLE [dbo].[Cms_PageLabel] CHECK CONSTRAINT [FK_Cms_PageLabel_Cms_Page]
GO
/****** Object:  ForeignKey [FK__PageImage__PageI__2C3393D0]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Picture]  WITH CHECK ADD  CONSTRAINT [FK__PageImage__PageI__2C3393D0] FOREIGN KEY([PageId])
REFERENCES [dbo].[Cms_Page] ([Id])
GO
ALTER TABLE [dbo].[Blog_Picture] CHECK CONSTRAINT [FK__PageImage__PageI__2C3393D0]
GO
/****** Object:  ForeignKey [FK_Blog_Comments_Blog_Articles]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Comment]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Comments_Blog_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Blog_Article] ([Id])
GO
ALTER TABLE [dbo].[Blog_Comment] CHECK CONSTRAINT [FK_Blog_Comments_Blog_Articles]
GO
/****** Object:  ForeignKey [FK_Blog_Comments_Blog_Comments]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_Comment]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Comments_Blog_Comments] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Blog_Comment] ([Id])
GO
ALTER TABLE [dbo].[Blog_Comment] CHECK CONSTRAINT [FK_Blog_Comments_Blog_Comments]
GO
/****** Object:  ForeignKey [FK_Blog_ArticleTags_Blog_Articles]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_ArticleTag]  WITH CHECK ADD  CONSTRAINT [FK_Blog_ArticleTags_Blog_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Blog_Article] ([Id])
GO
ALTER TABLE [dbo].[Blog_ArticleTag] CHECK CONSTRAINT [FK_Blog_ArticleTags_Blog_Articles]
GO
/****** Object:  ForeignKey [FK_Blog_ArticleTags_Blog_Tags]    Script Date: 06/14/2012 23:22:08 ******/
ALTER TABLE [dbo].[Blog_ArticleTag]  WITH CHECK ADD  CONSTRAINT [FK_Blog_ArticleTags_Blog_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Blog_Tag] ([Id])
GO
ALTER TABLE [dbo].[Blog_ArticleTag] CHECK CONSTRAINT [FK_Blog_ArticleTags_Blog_Tags]
GO
