USE [Library]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 8/23/2022 11:10:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[BookCode] [nvarchar](10) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BookTitle] [nvarchar](100) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[RegisteredDate] [date] NOT NULL,
 CONSTRAINT [PK_BookID] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (18, N'1222', 6, N'Quantum Mechanics', N'Nouredine Zettili', CAST(36149.00 AS Decimal(18, 2)), CAST(N'2021-03-19' AS Date))
INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (19, N'1224', 8, N'Pride and Prejudice', N'Jane Austen', CAST(3423.00 AS Decimal(18, 2)), CAST(N'2021-01-21' AS Date))
INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (20, N'5553', 10, N'The Name of the Wind', N'Patrick Rothfuss', CAST(466.00 AS Decimal(18, 2)), CAST(N'2021-01-14' AS Date))
INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (21, N'2444', 6, N'Great Scientists', N'John Farndon', CAST(12000.00 AS Decimal(18, 2)), CAST(N'2021-03-19' AS Date))
INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (22, N'2233', 8, N'Nineteen Eighty Four', N'Gorge Orwell', CAST(3423.00 AS Decimal(18, 2)), CAST(N'2021-01-24' AS Date))
INSERT [dbo].[Book] ([BookID], [BookCode], [CategoryID], [BookTitle], [Author], [Price], [RegisteredDate]) VALUES (23, N'1123', 10, N'A Game of Thrones', N'George R. R. Martin', CAST(466.00 AS Decimal(18, 2)), CAST(N'2021-01-14' AS Date))
SET IDENTITY_INSERT [dbo].[Book] OFF
ALTER TABLE [dbo].[Book]  WITH NOCHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
