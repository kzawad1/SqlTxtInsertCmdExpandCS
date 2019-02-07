PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE COMPANY(
   ID INT PRIMARY KEY     NOT NULL,
   NAME           TEXT    NOT NULL,
   AGE            INT     NOT NULL,
   ADDRESS        CHAR(50),
   SALARY         REAL
);
INSERT INTO COMPANY VALUES(
/*   0: ID INT PRIMARY KEY     NOT NULL, */ 1,
/*   1: NAME           TEXT    NOT NULL, */ 'Paul',
/*   2: AGE            INT     NOT NULL, */ 32,
/*   3: ADDRESS        CHAR(50), */ 'California',
/*   4: SALARY         REAL */ 20000.0);
INSERT INTO COMPANY VALUES(
/*   0: ID INT PRIMARY KEY     NOT NULL, */ 2,
/*   1: NAME           TEXT    NOT NULL, */ 'Allen',
/*   2: AGE            INT     NOT NULL, */ 25,
/*   3: ADDRESS        CHAR(50), */ 'Texas',
/*   4: SALARY         REAL */ 15000.0);
INSERT INTO COMPANY VALUES(
/*   0: ID INT PRIMARY KEY     NOT NULL, */ 3,
/*   1: NAME           TEXT    NOT NULL, */ 'Teddy',
/*   2: AGE            INT     NOT NULL, */ 23,
/*   3: ADDRESS        CHAR(50), */ 'Norway',
/*   4: SALARY         REAL */ 20000.0);
CREATE TABLE DEPARTMENT(
   ID INT PRIMARY KEY      NOT NULL,
   DEPT           CHAR(50) NOT NULL,
   EMP_ID         INT      NOT NULL
);
COMMIT;
;