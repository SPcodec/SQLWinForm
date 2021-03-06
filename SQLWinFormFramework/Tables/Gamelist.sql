USE [Demo]
GO
/****** Object:  Table [dbo].[GameList]    Script Date: 15/11/2021 18:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameList](
	[Name] [nvarchar](50) NOT NULL,
	[Release_Year] [int] NOT NULL,
	[Completed] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_GameList] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[GameList] ([Name], [Release_Year], [Completed]) VALUES (N'Monster Hunter: World', 2018, N'Yes')
INSERT [dbo].[GameList] ([Name], [Release_Year], [Completed]) VALUES (N'Overcooked! 2', 2018, N'No')
INSERT [dbo].[GameList] ([Name], [Release_Year], [Completed]) VALUES (N'Payday 2', 2013, N'No')
INSERT [dbo].[GameList] ([Name], [Release_Year], [Completed]) VALUES (N'S4 League', 2007, N'N/A')
GO
ALTER TABLE [dbo].[GameList]  WITH CHECK ADD  CONSTRAINT [chk_Frequency] CHECK  (([Completed]='N/A' OR [Completed]='No' OR [Completed]='Yes'))
GO
ALTER TABLE [dbo].[GameList] CHECK CONSTRAINT [chk_Frequency]
GO
