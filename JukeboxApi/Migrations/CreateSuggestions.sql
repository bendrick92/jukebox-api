IF OBJECT_ID ('dbo.Suggestions', 'U') IS NOT NULL
	DROP TABLE Suggestions
GO
CREATE TABLE Suggestions (
	Id INT IDENTITY(1,1),
	SubmitterName VARCHAR(500),
	SongName VARCHAR(500),
	ArtistName VARCHAR(500),
	Added DATETIME,
	IsActive BIT
);