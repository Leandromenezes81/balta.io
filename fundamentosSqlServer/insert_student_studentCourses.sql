SELECT *
FROM [Course]

SELECT *
FROM [Student]

SELECT *
FROM [StudentCourse]

-- Gerando um novo Id
-- SELECT NEWID() ou...
DECLARE @studentId UNIQUEIDENTIFIER = (SELECT NEWID())

INSERT INTO
    [Student]
    (
    [Id],
    [Name],
    [Email],
    [Document],
    [Phone],
    [Birthdate],
    [CreateDate]
    )
VALUES
    (
        @studentId,
        'André Baltieri',
        'hello@balta.io',
        '12345678912',
        '123456789',
        NULL,
        GETDATE()
    )

INSERT INTO
    [StudentCourse]
    (
    [CourseId],
    [StudentId],
    [Progress],
    [Favorite],
    [StartDate],
    [LastUpdateDate]
    )
VALUES
    (
        '5d8cf396-e717-9a02-2443-021b00000000',
        '7d615ff6-9855-4530-b553-ba9a6e80e6ee',
        50,
        0,
        '2022-12-21 01:01:29',
        GETDATE()
    )