SELECT TOP (1000) [Id]
      ,[Tag]
      ,[Title]
      ,[Url]
      ,[Summary]
      ,[CreateDate]
      ,[Category]
      ,[Author]
  FROM [balta].[dbo].[vwCourses] ORDER BY [CreateDate] DESC