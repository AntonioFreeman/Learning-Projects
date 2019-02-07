--
 USE [examensdb]
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_InsertTask]
	-- Add the parameters for the stored procedure here
	@title text,
	@question text,
	@answer_1 text,
	@correct_Ans_1 bit,
	@answer_2 text,
	@correct_Ans_2 bit,
	@answer_3 text,
	@correct_Ans_3 bit,
	@answer_4 text,
	@correct_Ans_4 bit,
	@id_Examen int,
	@Id int out
AS
	INSERT INTO Tasks(Title, Question, Answer_1, Correct_Ans_1, Answer_2, Correct_Ans_2, Answer_3, Correct_Ans_3, Answer_4, Correct_Ans_4, Id_Examen)
	VALUES (@title, @question, @answer_1, @correct_Ans_1, @answer_2, @correct_Ans_2, @answer_3, @correct_Ans_3, @answer_4, @correct_Ans_4, @id_Examen)

	SET @Id=SCOPE_IDENTITY()
