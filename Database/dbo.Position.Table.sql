USE [Library]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 8/23/2022 11:10:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionID] [int] IDENTITY(0,1) NOT NULL,
	[Position] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([PositionID], [Position]) VALUES (0, N'Select Position')
INSERT [dbo].[Position] ([PositionID], [Position]) VALUES (1, N'Student')
INSERT [dbo].[Position] ([PositionID], [Position]) VALUES (2, N'Staff')
SET IDENTITY_INSERT [dbo].[Position] OFF
