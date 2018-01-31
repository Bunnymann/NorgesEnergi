CREATE TABLE info(
info_ID INT IDENTITY(1,1) NOT NULL,
stage1 VARCHAR(50),
stage2 VARCHAR(50),
stage3 VARCHAR(50),
stage4 VARCHAR(50),
helptext_sum TEXT,
helptext_full TEXT,
helptext_header VARCHAR(50),
CONSTRAINT info_PK PRIMARY KEY(info_ID)
);

CREATE TABLE category(
category_ID INT IDENTITY(1,1) NOT NULL,
category_name VARCHAR(50),
parent_ID INT,
CONSTRAINT category_PK PRIMARY KEY(category_ID),
CONSTRAINT category_FK FOREIGN KEY(parent_ID) REFERENCES category(category_ID) ON DELETE CASCADE
);

CREATE TABLE metatag(
metatag_ID INT IDENTITY(1,1) NOT NULL,
metatag_tag VARCHAR(50) NOT NULL,
CONSTRAINT metatag_PK PRIMARY KEY(metatag_ID)
);

CREATE TABLE metainfo (
category_ID INT NOT NULL,
metatag_ID INT NOT NULL,
CONSTRAINT metainfo_PK PRIMARY KEY(category_ID, metatag_ID),
CONSTRAINT category_ID FOREIGN KEY(category_ID) REFERENCES category(category_ID) ON DELETE CASCADE,
CONSTRAINT metatag_ID FOREIGN KEY(metatag_ID) REFERENCES metatag(metatag_ID) ON DELETE CASCADE
);  