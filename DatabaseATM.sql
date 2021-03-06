
/****** Object:  Table [dbo].[ATM]    Script Date: 04/22/2017 15:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ATM](
	[ATMID] [int] IDENTITY(1,1) NOT NULL,
	[ATMName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[BranchID] [int] NULL,
	[State] [bit] NULL,
	[Status] [int] NULL,
	[IPLan] [nvarchar](50) NULL,
	[IPWan] [nvarchar](100) NULL,
	[Port] [int] NULL,
	[ImageServerID] [int] NULL,
	[CameraID] [nvarchar](255) NULL,
	[Serial] [nvarchar](255) NULL,
	[LicenseKey] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[DateLogs] [datetime] NULL,
	[Vibration] [bit] NULL,
	[RateofRiseHeat] [bit] NULL,
	[Door] [bit] NULL,
	[Smoke] [bit] NULL,
	[PassiveInfrared] [bit] NULL,
	[IRBeamCut] [bit] NULL,
	[Battery] [bit] NULL,
	[LanNetwork] [bit] NULL,
	[WanNetwork] [bit] NULL,
 CONSTRAINT [PK_ATM] PRIMARY KEY CLUSTERED 
(
	[ATMID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ATM] ON
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (3, N'[Camera 1] 123456', N'24 Hai Bà Trưng, Quận Hoàn Kiếm', 1, 1, 0, N'192.168.1.21', N'http://localhost:34300', 80, 1, N'[Camera 1] 123456', N'12345', N'f2e6e5eb3089fbf819b573a8e339411a4fd95cebd79a752874bce0eab834eb03645fb392c1d7c91d3148d4f5101033a9bfd6972ca9e5be791cd2677b37dbd68f8fa62cfb68488abad546361a91d9bf9e57ae1e19d25e5cc9ce76b0381334c401cd7e33873e4397177663439cb1edcc95ede876b9dbda93f7d124bc64d6c1218', NULL, NULL, CAST(0x0000A753010EB909 AS DateTime), N'2', 1, NULL, 1, 0, 0, 0, 0, 1, 1, 1, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (6, N'[Camera 2] Camera 1', N'Số 2 Phan Chu Trinh, Quận Hoàn Kiếm', 1, 1, 1, N'192.168.0.105', N'http://localhost:34300', 8080, 2, N'[Camera 2] Camera 1', N'0', N'f2e6e5eb3089fbf819b573a8e339411a4fd95cebd79a752874bce0eab834eb03645fb392c1d7c91d3148d4f5101033a9bfd6972ca9e5be791cd2677b37dbd68f8fa62cfb68488abad546361a91d9bf9e57ae1e19d25e5cc9ce76b0381334c401cd7e33873e4397177663439cb1edcc95ede876b9dbda93f7d124bc64d6c1218', NULL, NULL, CAST(0x0000A751012D5302 AS DateTime), N'2', 1, NULL, 1, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (7, N'[CMR 3] Camera 1', N'34 Hàng Bài, Quận Hoàn Kiếm', 1, 1, 0, N'192.168.0.1', N'', 8080, 2, N'[CMR 3] Camera 1', N'1', N'f2e6e5eb3089fbf819b573a8e339411a4fd95cebd79a752874bce0eab834eb03645fb392c1d7c91d3148d4f5101033a9bfd6972ca9e5be791cd2677b37dbd68f8fa62cfb68488abad546361a91d9bf9e57ae1e19d25e5cc9ce76b0381334c401cd7e33873e4397177663439cb1edcc95ede876b9dbda93f7d124bc64d6c1218', NULL, NULL, CAST(0x0000A74E012D3489 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (8, N'[CMR 4] Camera 1', N'14 Phố Phủ Doãn, Quận Hoàn Kiếm', 1, 0, 0, N'192.168.0.12', N'', 8080, 2, N'[CMR 4] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EDCB07 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (9, N'[CMR 5] Camera 1', N'37B Đường Thành, Quận Hoàn Kiếm.', 1, 1, 0, N'192.168.0.13', N'', 8080, 2, N'[CMR 5] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EDFC70 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (10, N'[CMR 6] Camera 1', N'23 Hàng Khoai, Quận Hoàn Kiếm', 1, 0, 0, N'192.168.0.15', N'', 8080, 2, N'[CMR 6] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EDF076 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (11, N'[CMR 7] Camera 1', N'38 Hàng Vôi, Quận Hoàn Kiếm', 1, 0, 0, N'192.168.0.18', N'', 8080, 2, N'[CMR 7] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EE0D0D AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (12, N'[CMR 8] Camera 1', N'17 Hàn Thuyên Hà Nôị', 1, 0, 0, N'192.168.0.19', N'', 8080, 2, N'[CMR 8] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EE195A AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (13, N'[CMR CUA T1] Camera 1', N'53 Đinh Tiên Hoàng, Quận Hoàn Kiếm', 1, 0, 0, N'192.168.0.17', N'', 8080, 2, N'[CMR CUA T1] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EE2DE6 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[ATM] ([ATMID], [ATMName], [Address], [BranchID], [State], [Status], [IPLan], [IPWan], [Port], [ImageServerID], [CameraID], [Serial], [LicenseKey], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser], [Active], [DateLogs], [Vibration], [RateofRiseHeat], [Door], [Smoke], [PassiveInfrared], [IRBeamCut], [Battery], [LanNetwork], [WanNetwork]) VALUES (14, N'[CMR Thang May] Camera 1', N'55 Cầu Gỗ', 1, 0, 0, N'192.168.0.115', N'', 8080, 2, N'[CMR Thang May] Camera 1', NULL, NULL, NULL, NULL, CAST(0x0000A74800EE3926 AS DateTime), N'2', 1, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[ATM] OFF

/****** Object:  Table [dbo].[Branch]    Script Date: 04/22/2017 15:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUser] [nvarchar](50) NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON
INSERT [dbo].[Branch] ([BranchID], [BranchName], [Address], [CreatedDate], [CreatedUser], [ModifiedDate], [ModifiedUser]) VALUES (1, N'Chi nhánh Đống Đa', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Branch] OFF

/****** Object:  StoredProcedure [dbo].[City_GetAll]    Script Date: 04/22/2017 15:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ATM_GetAll]
as 
begin
	SELECT [ATMID]
      ,[ATMName]
      ,[Address]
      ,[BranchID]
      ,[State]
      ,[Status]
      ,[IPLan]
      ,[IPWan]
      ,[Port]
      ,[ImageServerID]
      ,[CameraID]
      ,[Serial]
      ,[LicenseKey]
      ,[CreatedDate]
      ,[CreatedUser]
      ,[ModifiedDate]
      ,[ModifiedUser]
      ,[Active]
      ,[DateLogs]
      ,[Vibration]
      ,[RateofRiseHeat]
      ,[Door]
      ,[Smoke]
      ,[PassiveInfrared]
      ,[IRBeamCut]
      ,[Battery]
      ,[LanNetwork]
      ,[WanNetwork]
  FROM [CB08].[dbo].[ATM]
end
GO