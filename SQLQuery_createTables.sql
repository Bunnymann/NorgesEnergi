CREATE TABLE user_table(
user_ID INT NOT NULL,
user_name VARCHAR(50),
CONSTRAINT user_PK PRIMARY KEY(user_ID)
);

CREATE TABLE page_table(
page_ID INT NOT NULL,
page_text TEXT,
user_ID INT NOT NULL,
CONSTRAINT page_PK PRIMARY KEY(page_ID),
CONSTRAINT page_user_FK FOREIGN KEY(user_ID) REFERENCES dbo.user_table(user_ID)
);

CREATE TABLE help_table(
help_ID INT NOT NULL,
help_text TEXT NOT NULL,
CONSTRAINT help_FK PRIMARY KEY(help_ID)
);

CREATE TABLE helppage_table(
page_ID INT NOT NULL,
help_ID INT NOT NULL,
CONSTRAINT helppage_page_FK FOREIGN KEY(page_ID) REFERENCES dbo.page_table(page_ID),
CONSTRAINT helppage_help_FK FOREIGN KEY(help_ID) REFERENCES dbo.help_table(help_ID)
);

CREATE TABLE helpedit_table(
user_ID INT NOT NULL,
help_ID INT NOT NULL,
CONSTRAINT helpedit_user_FK FOREIGN KEY(user_ID) REFERENCES dbo.user_table(user_ID),
CONSTRAINT helpedit_help_FK FOREIGN KEY(help_ID) REFERENCES dbo.help_table(help_ID)
); 