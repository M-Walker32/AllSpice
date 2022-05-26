CREATE TABLE IF NOT EXISTS accounts(
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
    id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    picture TEXT NOT NULL,
    title TEXT NOT NULL,
    subtitle TEXT NOT NULL,
    category TEXT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS ingredients(
    id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
    name TEXT NOT NULL,
    quantity TEXT NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS steps(
    id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
    position TEXT NOT NULL,
    body TEXT NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS favorites(
    id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
    accountId VARCHAR(255) NOT NULL,
    FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

DROP TABLE recipes;

DROP TABLE favorites;

DROP TABLE steps;

DROP TABLE ingredients;