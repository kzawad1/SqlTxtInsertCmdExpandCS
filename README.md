# Overview
The main purpose of this project is to take a text file with SQL statements and replace the one line "INSERT" statement with a multiple line insert statement with comments.  This is made so that it is easier to compare using a file comparison tool such as MELD or WinMerge or others.

# Example Output
To show by example of what the utility does, below are two examples of a SQL text file: (1) input file, & (2) output file.

1) Generate  a *.sql file

To get a text representation of a SQL database one can execute the command below:
```
sqlite3 test.db .dump > test.db.sql
```
2) After that pass the **test.db.sql** through **SqlTxtInsertCmdExpandCS**.

```
>SqlTxtInsertCmdExpandCS.exe test.db.sql
SQL Text File (*.sql) 'INSERT' Statement Expander
 PURPOSE: Make DIFF's easier.

Input File:  test.db.sql
Output File: test.db.YYYY-MM-DD_HHMMSS.sql
Working....
Finished with Parsing, expanded 3 INSERT statements.
>
```

3) Compare the output **test.db.sql** & **test.db.YYYY-MM-DD_HHMMSS.sql**

Input:
```
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
```

Output:
```
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
```

# License

To do.
