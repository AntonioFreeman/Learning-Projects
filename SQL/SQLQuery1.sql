USE [examensdb]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertExamen]    Script Date: 05.02.2019 15:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_InsertExamen]
	-- Add the parameters for the stored procedure here
	@subject text,
	@password nvarchar(50),
	@procent_3 int,
	@procent_4 int,
	@procent_5 int,
	@amountTasks int,
	@timeExamen int,
	@id int out
AS
	INSERT INTO Examens(Subject, Password, Procent_3, Procent_4, Procent_5, AmountTasks, TimeExamen)
	VALUES (@subject, @password, @procent_3, @procent_4, @procent_5, @amountTasks, @timeExamen)

	SET @id=SCOPE_IDENTITY()
