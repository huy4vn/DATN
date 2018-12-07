USE [DATN]
GO
DECLARE @cnt INT = 0;
DECLARE @cnt_total INT = 5;
WHILE @cnt < @cnt_total
BEGIN
    DECLARE @randomNumber FLOAT=CAST(RAND() as decimal(1,1));
   INSERT INTO [dbo].[WeightVector]
           ([star]
           ,[rating])
     VALUES
           (@randomNumber
           ,CAST(1-@randomNumber as decimal(1,1)));
		   Set @cnt=@cnt+1;
END;