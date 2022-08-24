USE [Library]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/23/2022 11:10:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(0,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (0, N'Select Category')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1, N'Novel')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2, N'Story')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (6, N'Science')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (8, N'Fiction')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (9, N'Poetry')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (10, N'Fantasy')
SET IDENTITY_INSERT [dbo].[Category] OFF
