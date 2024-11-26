SET session_replication_role = 'replica';

DELETE FROM "Products";

SET session_replication_role = 'origin';

--select 'DELETE FROM ' || quote_ident(table_name) || ';'
--from information_schema."tables"
--where table_schema = 'public' and table_name != '__EFMigrationsHistory' order by table_name
