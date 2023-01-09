-- SELECT * FROM [Student]

DECLARE @StudentId UNIQUEIDENTIFIER = '7d615ff6-9855-4530-b553-ba9a6e80e6ee'

SELECT
    [Student].[Name] AS [Student],
    [Course].[Title] AS [Course],
    [StudentCourse].[Progress],
    [StudentCourse].[LastUpdateDate]

FROM
    [StudentCourse]
    INNER JOIN [Student] ON [StudentCourse].[StudentId] = [Student].[Id]
    INNER JOIN [Course] ON [StudentCourse].[CourseId] = [Course].[Id]
WHERE
    [StudentCourse].[StudentId] = @StudentId
    AND [StudentCourse].[Progress] < 100
    AND [StudentCourse].[Progress] > 0
ORDER BY
    [StudentCourse].[LastUpdateDate] DESC