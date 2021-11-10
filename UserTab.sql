USE [Gameslist]
GO
/****** Object:  Table [dbo].[UserTab]    Script Date: 09/11/2021 21:46:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTab](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Age] [float] NULL,
 CONSTRAINT [PK_UserTab] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UserTab] ([ID], [Name], [Age]) VALUES (1, N'1', 1)
INSERT [dbo].[UserTab] ([ID], [Name], [Age]) VALUES (2, N'2', 2)
INSERT [dbo].[UserTab] ([ID], [Name], [Age]) VALUES (3, N'3', 3)
GO
