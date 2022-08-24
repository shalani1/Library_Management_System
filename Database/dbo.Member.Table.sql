USE [Library]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 8/23/2022 11:10:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NOT NULL,
	[RegistrationNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[NIC] [nvarchar](13) NOT NULL,
	[PhoneNo] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[RegisteredDate] [date] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1, 1, N'SOF/19/B1/10', N'Kalpani', N'Hansika', N'455555555555', N'0712231234', N'kalpani@gmail.com', N'Kalthota', CAST(N'2020-12-01' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (3, 2, N'LEC19310', N'Deshan', N'Sanoj', N'938520453555', N'0771234567', N'sanoj@yahoo.com', N'Galle', CAST(N'2021-02-19' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (5, 1, N'NET/18/B1/03', N'Tharindu', N'Haasaranaga', N'223412342563', N'0452352567', N'tharinda@gmail.com', N'Tanglle', CAST(N'2021-03-07' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (6, 2, N'LEC19420', N'Mihin', N'Waruna', N'897684357654', N'0784312567', N'mihin@@gmail.com', N'Ampara', CAST(N'2021-03-24' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (7, 2, N'LEC19268', N'Deshan', N'Maduka', N'987325764198', N'0713326987', N'deshan@yahoo.com', N'Colombo', CAST(N'2021-03-19' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (8, 2, N'LEC16370', N'Tharmi', N'Basker', N'127643895483', N'0773318497', N'basker123@gmail.com', N'Mahawa', CAST(N'2021-01-08' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (9, 1, N'ELT/19/B1/15', N'Denesha', N'Wasana', N'237619745623', N'0707741753', N'denesha@gmail.com', N'Tangalle', CAST(N'2021-01-16' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (10, 1, N'SOF/19/B2/20', N'Manoj', N'Withana', N'239845013578', N'0457189345', N'manoj@yahoo.com', N'Baddegama', CAST(N'2021-01-06' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1008, 1, N'MMW/19/B1/16', N'Kasuni', N'Dileka', N'893467124528', N'0771237924', N'kasuni@@gmail.com', N'Mirissa', CAST(N'2020-12-01' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1009, 2, N'LEC33012', N'Tharun', N'Prabath', N'998076453167', N'0714195265', N'tharun@yahoo.com', N'Hkkaduwa', CAST(N'2021-02-19' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1010, 1, N'MEC/17/B1/12', N'Winushi', N'Sewwandi', N'563487685673', N'0707812683', N'winushi@gmail.com', N'Wellawaya', CAST(N'2021-03-07' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1011, 2, N'LEC77850', N'Minoli', N'Sathisha', N'786531542985', N'0783104275', N'sewwandi@@gmail.com', N'Thissa', CAST(N'2021-03-24' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1012, 2, N'LEC99251', N'Jayan', N'Mihikula', N'562718340265', N'0763481056', N'jayan@@gmail.com', N'Katharagama', CAST(N'2021-03-19' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1013, 2, N'LEC19682', N'Sanu', N'Dewapriya', N'872398174602', N'0782122443', N'dewapriya@yahoo.com', N'Galle', CAST(N'2021-01-08' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1014, 1, N'MEC/19/B1/02', N'Tharika', N'Hansini', N'987612042854', N'0753423434', N'hansini@@gmail.com', N'Ratmalana', CAST(N'2021-01-16' AS Date))
INSERT [dbo].[Member] ([MemberID], [PositionID], [RegistrationNo], [FirstName], [LastName], [NIC], [PhoneNo], [Email], [Address], [RegisteredDate]) VALUES (1015, 1, N'MMW/17/B1/17', N'Manula', N'Nethsara', N'671245930184', N'0752324674', N'manula@gmail.com', N'Galkissa', CAST(N'2021-01-06' AS Date))
SET IDENTITY_INSERT [dbo].[Member] OFF
ALTER TABLE [dbo].[Member]  WITH NOCHECK ADD  CONSTRAINT [FK_Member_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([PositionID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Position]
GO
