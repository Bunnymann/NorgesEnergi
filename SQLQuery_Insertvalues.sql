INSERT INTO user_table(user_ID, user_name)
VALUES 
(1, 'Eirik'), (2, 'Benjamin'), (3, 'Marius'), (4, 'Superbruker');

INSERT INTO page_table(page_ID, page_text)
VALUES
(1, 'login'),
(2, 'selger'),
(3, 'invoice'),
(201, 'selgeradm'),
(301, 'invoiceadm'),
(401, 'editadm');

INSERT INTO help_table(help_ID, help_text)
VALUES (1, 'Press the "login" button'),
(2, 'Press the "add sale" button'),
(3, 'Press the "new invoce" button)'),
(4, 'Press the "accept changes" button'); 

INSERT INTO userpage_table(user_ID, page_ID)
VALUES (1,1),(1,2),(1,3),
(2,1),(2,2),(2,3),
(3,1),(3,2),(3,3),
(4, 1), (4,201),(4,301),(4,401);

INSERT INTO helpedit_table(user_ID, help_ID)
VALUES
(4,1),(4,2),(4,3),(4,4);

INSERT INTO helppage_table(page_ID, help_ID)
VALUES (1,1), (2,2), (3,3), (401,4);