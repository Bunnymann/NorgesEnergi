DROP TABLE IF EXISTS infokat;
DROP TABLE IF EXISTS knyttekat;
DROP TABLE IF EXISTS hjelpeinfo;
DROP TABLE IF EXISTS under_kat;
DROP TABLE IF EXISTS hoved_kat;

CREATE TABLE hoved_kat(
hoved_ID INT,
hovedkategori VARCHAR(45),
CONSTRAINT hovedkat_pk PRIMARY KEY(hoved_ID)
);

CREATE TABLE under_kat(
under_ID INT,
underkategori VARCHAR(45)
CONSTRAINT underkat_pk PRIMARY KEY(under_ID)
);

CREATE TABLE hjelpeinfo(
info_ID INT,
hjelpekst TEXT
CONSTRAINT hjelpeinfo_pk PRIMARY KEY(info_ID)
);

CREATE TABLE knyttekat(
hoved_ID INT,
under_ID INT,
CONSTRAINT knyttekat_pk PRIMARY KEY(hoved_ID, under_ID),
CONSTRAINT knyttekat_hoved_fk FOREIGN KEY(hoved_ID) REFERENCES dbo.hoved_kat,
CONSTRAINT knyttekat_under_fk FOREIGN KEY(under_ID) REFERENCES dbo.under_kat
);

CREATE TABLE infokat(
under_ID INT,
info_ID INT,
CONSTRAINT infokat_pk PRIMARY KEY(under_ID,info_ID),
CONSTRAINT infokat_under_fk FOREIGN KEY(under_ID) REFERENCES dbo.under_kat,
CONSTRAINT infokat_info_fk FOREIGN KEY(info_ID) REFERENCES dbo.hjelpeinfo
);