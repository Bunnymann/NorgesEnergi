drop table if exists info;
drop table if exists stage1;
drop table if exists stage2;
drop table if exists stage3;
drop table if exists stage4;
drop table if exists helptext;
drop table if exists metatag;
drop table if exists helptexttag;
drop table if exists users;

CREATE TABLE [dbo].[helptext] (
    [helptext_ID]     INT          IDENTITY (1, 1) NOT NULL,
    [helptext_header] VARCHAR (45) NOT NULL,
    [helptext_short]  TEXT         NOT NULL,
    [helptext_long]   TEXT         NOT NULL,
    CONSTRAINT [helptext_PK] PRIMARY KEY CLUSTERED ([helptext_ID] ASC)
);

CREATE TABLE [dbo].[stage1] (
    [stage1_ID]   INT          IDENTITY (1, 1) NOT NULL,
    [stage1_name] VARCHAR (45) NOT NULL,
    CONSTRAINT [stage1_PK] PRIMARY KEY CLUSTERED ([stage1_ID] ASC)
);

CREATE TABLE [dbo].[stage2] (
    [stage2_ID]   INT          IDENTITY (1, 1) NOT NULL,
    [stage2_name] VARCHAR (45) NOT NULL,
    CONSTRAINT [stage2_PK] PRIMARY KEY CLUSTERED ([stage2_ID] ASC)
);

CREATE TABLE [dbo].[stage3] (
    [stage3_ID]   INT          IDENTITY (1, 1) NOT NULL,
    [stage3_name] VARCHAR (45) NOT NULL,
    CONSTRAINT [stage3_PK] PRIMARY KEY CLUSTERED ([stage3_ID] ASC)
);

CREATE TABLE [dbo].[stage4] (
    [stage4_ID]   INT          IDENTITY (1, 1) NOT NULL,
    [stage4_name] VARCHAR (45) NOT NULL,
    [helptext_ID] INT          NULL,
    CONSTRAINT [stage4_PK] PRIMARY KEY CLUSTERED ([stage4_ID] ASC),
    CONSTRAINT [stage4_helpID_FK] FOREIGN KEY ([helptext_ID]) REFERENCES [dbo].[helptext] ([helptext_ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[info] (
    [info_ID]   INT IDENTITY (1, 1) NOT NULL,
    [stage1_ID] INT NOT NULL,
    [stage2_ID] INT NOT NULL,
    [stage3_ID] INT NOT NULL,
    [stage4_ID] INT NOT NULL,
    CONSTRAINT [info_PK] PRIMARY KEY CLUSTERED ([info_ID] ASC),
    CONSTRAINT [info_stage1_FK] FOREIGN KEY ([stage1_ID]) REFERENCES [dbo].[stage1] ([stage1_ID]),
    CONSTRAINT [info_stage2_FK] FOREIGN KEY ([stage2_ID]) REFERENCES [dbo].[stage2] ([stage2_ID]),
    CONSTRAINT [info_stage3_FK] FOREIGN KEY ([stage3_ID]) REFERENCES [dbo].[stage3] ([stage3_ID]),
    CONSTRAINT [info_stage4_FK] FOREIGN KEY ([stage4_ID]) REFERENCES [dbo].[stage4] ([stage4_ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[metatag] (
    [metatag_ID] INT          IDENTITY (1, 1) NOT NULL,
    [tag]        VARCHAR (45) NOT NULL,
    CONSTRAINT [metatag_PK] PRIMARY KEY CLUSTERED ([metatag_ID] ASC)
);

CREATE TABLE [dbo].[helptexttag] (
    [helptexttag_ID] INT IDENTITY (1, 1) NOT NULL,
    [helptext_ID]    INT NOT NULL,
    [metatag_ID]     INT NULL,
    CONSTRAINT [helptexttag_PK] PRIMARY KEY CLUSTERED ([helptexttag_ID] ASC),
    CONSTRAINT [helptexttag_helptext_FK] FOREIGN KEY ([helptext_ID]) REFERENCES [dbo].[helptext] ([helptext_ID]),
    CONSTRAINT [helptexttag_metatag_FK] FOREIGN KEY ([metatag_ID]) REFERENCES [dbo].[metatag] ([metatag_ID])
);

CREATE TABLE [dbo].[users] (
    [loginname]     VARCHAR (90) NOT NULL,
    [loginpassword] VARCHAR (90) NOT NULL,
    [stage1_ID]     INT          NULL,
    CONSTRAINT [user_PK] PRIMARY KEY CLUSTERED ([loginname] ASC),
    UNIQUE NONCLUSTERED ([loginname] ASC),
    CONSTRAINT [user_stage_FK] FOREIGN KEY ([stage1_ID]) REFERENCES [dbo].[stage1] ([stage1_ID])
);
