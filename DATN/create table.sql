CREATE TABLE DataPoint(
    id int primary key not null identity(1,1),
	rating int,
	star int
);
CREATE TABLE WeightVector(
	 id int primary key not null identity(1,1),
	rating int,
	star int
);