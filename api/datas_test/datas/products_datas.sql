DELETE FROM public."Products";
\copy public."Products" FROM 'datas/products.csv' DELIMITER ';' CSV HEADER
SELECT pg_catalog.setval('public."Products_Id_seq"', (SELECT MAX("Id") FROM "Products"), true);