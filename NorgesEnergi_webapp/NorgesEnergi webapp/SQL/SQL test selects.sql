-- Viser hovedkategori og tilhørende underkategorier
SELECT dbo.hoved_kat.hovedkategori, dbo.under_kat.underkategori FROM dbo.hoved_kat
INNER JOIN dbo.knyttekat ON dbo.knyttekat.hoved_ID = dbo.hoved_kat.hoved_ID
INNER JOIN dbo.under_kat ON dbo.knyttekat.under_ID = dbo.under_kat.under_ID;

-- Viser underkategorier til en valgt hovedkategori
SELECT dbo.under_kat.underkategori FROM dbo.under_kat
INNER JOIN dbo.knyttekat ON dbo.knyttekat.under_ID = dbo.under_kat.under_ID
INNER JOIN dbo.hoved_kat ON dbo.knyttekat.hoved_ID = dbo.hoved_kat.hoved_ID
WHERE dbo.hoved_kat.hovedkategori = 'privat';

-- Viser hjelpetekst for valgt underkategori og tilhørende hovedkategori
SELECT dbo.hjelpeinfo.hjelpekst, dbo.under_kat.underkategori FROM dbo.hjelpeinfo
INNER JOIN dbo.infokat ON infokat.info_ID = hjelpeinfo.info_ID
INNER JOIN dbo.under_kat ON under_kat.under_ID = infokat.under_ID
INNER JOIN dbo.knyttekat ON dbo.knyttekat.under_ID = dbo.under_kat.under_ID
INNER JOIN dbo.hoved_kat ON dbo.knyttekat.hoved_ID = dbo.hoved_kat.hoved_ID
WHERE underkategori = 'adresse' AND hovedkategori = 'privat';