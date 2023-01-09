CREATE OR ALTER VIEW vwCareers
AS
    SELECT
        [Career].[Id],
        [Career].[Title],
        [Career].[Url],
        COUNT([Id]) AS Courses
    FROM
        [balta].[dbo].[Career]
        INNER JOIN [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
    GROUP BY
    [Career].[Id],
    [Career].[Title],
    [Career].[Url]