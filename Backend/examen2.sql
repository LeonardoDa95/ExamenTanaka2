DROP DATABASE IF EXISTS examen2;
CREATE DATABASE examen2;
USE examen2;

CREATE TABLE ranking (
    ranking_id INT NOT NULL AUTO_INCREMENT,
    score INT NOT NULL,
    name VARCHAR(3) NOT NULL,
    final BOOLEAN NOT NULL,
    levels VARCHAR(200) NOT NULL,
    PRIMARY KEY (ranking_id)
);

/* Inserts */

INSERT INTO ranking(ranking_id, score, name, final, levels)
VALUES 
(NULL ,3000 ,"snk" ,TRUE ,"level 3"),
(NULL ,1600 ,"aaa" ,FALSE ,"level 2"),
(NULL ,500 ,"zzz" ,FALSE ,"level 1"),
(NULL ,2700 ,"ezz" ,TRUE ,"level 3");

/* Consulta 1*/

SELECT * 
FROM ranking;

/* Consulta 2*/

SELECT name, score
FROM ranking
ORDER BY score ASC LIMIT 10;

/* Consulta 3*/

SELECT final, name 
FROM ranking 
WHERE final = 1;

/* Consulta 4*/

SELECT levels, AVG(score) AS promedio
FROM ranking
GROUP BY levels
ORDER BY levels ASC;
