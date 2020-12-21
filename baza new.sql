CREATE DATABASE [Majster]
go
USE [Majster]
GO
/****** Object:  Table [dbo].[ADVERTS]    Script Date: 23.11.2020 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADVERTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [int] NOT NULL,
	[CATEGORY] [int] NOT NULL,
	[TITLE] [varchar](255) NOT NULL,
	[DESCRIPTION] [text] NOT NULL,
	[DATE] [datetime] NOT NULL,
	[PRICE] [varchar](255) NOT NULL,
	[PHONE_NUMBER] varchar(9) NOT NULL,
	[IMAGE] [varchar](255) NULL,
	[IS_ARCHIVED] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CATEGORIES]    Script Date: 23.11.2020 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TAG] [varchar](3) NOT NULL,
	[NAME] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAV]    Script Date: 23.11.2020 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[USER] [int] NOT NULL,
	[ADV] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MESSAGE]    Script Date: 23.11.2020 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MESSAGE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ADVERT_ID] [int] NOT NULL,
	[MSG_FROM] [int] NOT NULL,
	[MSG_TO] [int] NOT NULL,
	[TEXT] [text] NOT NULL,
	[DATE] [datetime] NOT NULL,
	[IS_READ] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 23.11.2020 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOGIN] [varchar](255) NOT NULL,
	[PASSWORD] [varchar](255) NOT NULL,
	[MAIL] [varchar](255) NOT NULL,
	[FNAME] [varchar](255) NOT NULL,
	[LNAME] [varchar](255) NULL,
	[VERIFIED] [bit] NULL,
	[IS_ADMIN] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ADVERTS] ADD  DEFAULT ((0)) FOR [IS_ARCHIVED]
GO
ALTER TABLE [dbo].[MESSAGE] ADD  DEFAULT ((0)) FOR [IS_READ]
GO
ALTER TABLE [dbo].[USERS] ADD  DEFAULT ((0)) FOR [VERIFIED]
GO
ALTER TABLE [dbo].[USERS] ADD  DEFAULT ((0)) FOR [IS_ADMIN]
GO
ALTER TABLE [dbo].[ADVERTS]  WITH CHECK ADD  CONSTRAINT [FK_ADVERTS_CATEGORIES] FOREIGN KEY([CATEGORY])
REFERENCES [dbo].[CATEGORIES] ([ID])
GO
ALTER TABLE [dbo].[ADVERTS] CHECK CONSTRAINT [FK_ADVERTS_CATEGORIES]
GO
ALTER TABLE [dbo].[ADVERTS]  WITH CHECK ADD  CONSTRAINT [FK_ADVERTS_USERS] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[ADVERTS] CHECK CONSTRAINT [FK_ADVERTS_USERS]
GO
ALTER TABLE [dbo].[FAV]  WITH CHECK ADD  CONSTRAINT [FK_FAV_ADVERTS] FOREIGN KEY([ADV])
REFERENCES [dbo].[ADVERTS] ([ID])
GO
ALTER TABLE [dbo].[FAV] CHECK CONSTRAINT [FK_FAV_ADVERTS]
GO
ALTER TABLE [dbo].[FAV]  WITH CHECK ADD  CONSTRAINT [FK_FAV_USERS] FOREIGN KEY([USER])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[FAV] CHECK CONSTRAINT [FK_FAV_USERS]
GO
ALTER TABLE [dbo].[MESSAGE]  WITH CHECK ADD  CONSTRAINT [FK_MESSAGE_USERS] FOREIGN KEY([MSG_FROM])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[MESSAGE] CHECK CONSTRAINT [FK_MESSAGE_USERS]
GO
ALTER TABLE [dbo].[MESSAGE]  WITH CHECK ADD  CONSTRAINT [FK_MESSAGE_USERS1] FOREIGN KEY([MSG_TO])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[MESSAGE] CHECK CONSTRAINT [FK_MESSAGE_USERS1]
GO
ALTER TABLE [dbo].[MESSAGE] WITH CHECK ADD  CONSTRAINT [FK_FAV_MESSAGE] FOREIGN KEY([ADVERT_ID])
REFERENCES [dbo].[ADVERTS] ([ID])
GO
ALTER TABLE [dbo].[FAV] CHECK CONSTRAINT [FK_FAV_USERS]
GO
