PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE COMPANY(
   ID INT PRIMARY KEY     NOT NULL,
   NAME           TEXT    NOT NULL,
   AGE            INT     NOT NULL,
   ADDRESS        CHAR(50),
   SALARY         REAL
);
INSERT INTO COMPANY VALUES(1,'Paul',32,'California',20000.0);
INSERT INTO COMPANY VALUES(2,'Allen',25,'Texas',15000.0);
INSERT INTO COMPANY VALUES(3,'Teddy',23,'Norway',20000.0);
CREATE TABLE DEPARTMENT(
   ID INT PRIMARY KEY      NOT NULL,
   DEPT           CHAR(50) NOT NULL,
   EMP_ID         INT      NOT NULL
);
COMMIT;