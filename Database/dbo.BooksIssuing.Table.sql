USE [Library]
GO
/****** Object:  Table [dbo].[BooksIssuing]    Script Date: 8/23/2022 11:10:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BooksIssuing](
	[IssueID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[IssuedDate] [date] NOT NULL,
	[ReturnedDate] [date] NOT NULL,
	[IssuingStatus] [bit] NOT NULL,
	[DelayCharge] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
 CONSTRAINT [PK_BooksIssuing] PRIMARY KEY CLUSTERED 
(
	[IssueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[BooksIssuing] ON 

INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (98, 3, 18, CAST(N'2021-01-29' AS Date), CAST(N'2021-02-12' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (99, 3, 18, CAST(N'2021-03-19' AS Date), CAST(N'2021-04-15' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1100, 3, 19, CAST(N'2021-01-20' AS Date), CAST(N'2021-02-26' AS Date), 1, CAST(145.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1102, 7, 18, CAST(N'2021-01-02' AS Date), CAST(N'2021-01-29' AS Date), 1, CAST(30.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1103, 1, 20, CAST(N'2021-01-18' AS Date), CAST(N'2021-02-01' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1104, 7, 19, CAST(N'2021-01-31' AS Date), CAST(N'2021-02-26' AS Date), 1, CAST(25.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1105, 7, 20, CAST(N'2021-01-26' AS Date), CAST(N'2021-02-16' AS Date), 0, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (1106, 6, 19, CAST(N'2021-02-04' AS Date), CAST(N'2021-02-25' AS Date), 0, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2104, 3, 18, CAST(N'2021-01-29' AS Date), CAST(N'2021-02-12' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2105, 3, 18, CAST(N'2021-03-19' AS Date), CAST(N'2021-04-15' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2106, 3, 19, CAST(N'2021-03-03' AS Date), CAST(N'2021-03-24' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2107, 3, 19, CAST(N'2021-01-20' AS Date), CAST(N'2021-02-26' AS Date), 1, CAST(145.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2109, 7, 18, CAST(N'2021-01-02' AS Date), CAST(N'2021-01-29' AS Date), 1, CAST(30.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2110, 1, 20, CAST(N'2021-01-18' AS Date), CAST(N'2021-02-01' AS Date), 1, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2111, 7, 19, CAST(N'2021-01-31' AS Date), CAST(N'2021-02-26' AS Date), 1, CAST(25.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2112, 7, 20, CAST(N'2021-01-26' AS Date), CAST(N'2021-03-26' AS Date), 1, CAST(585.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2113, 6, 19, CAST(N'2021-02-04' AS Date), CAST(N'2021-02-25' AS Date), 0, CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2114, 1014, 21, CAST(N'2021-01-14' AS Date), CAST(N'2021-03-26' AS Date), 1, CAST(965.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[BooksIssuing] ([IssueID], [MemberID], [BookID], [IssuedDate], [ReturnedDate], [IssuingStatus], [DelayCharge], [PaymentStatus]) VALUES (2115, 1014, 23, CAST(N'2022-08-23' AS Date), CAST(N'2022-10-15' AS Date), 1, CAST(605.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[BooksIssuing] OFF
ALTER TABLE [dbo].[BooksIssuing]  WITH NOCHECK ADD  CONSTRAINT [FK_BooksIssuing_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([BookID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BooksIssuing] CHECK CONSTRAINT [FK_BooksIssuing_Book]
GO
ALTER TABLE [dbo].[BooksIssuing]  WITH NOCHECK ADD  CONSTRAINT [FK_BooksIssuing_Member] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BooksIssuing] CHECK CONSTRAINT [FK_BooksIssuing_Member]
GO
