﻿select 
		d.description,
		c.*	
from
		information_schema.columns c
		inner join
		(
			select 
				t.relname as table_name,
				c.attname as column_name,
				c.attnum as column_index,
				d.description
			from 
				pg_attribute c
				inner join pg_class t on c.attrelid=t.oid
				left join pg_description d on d.objoid=t.oid and d.objsubid=c.attnum		
		) d
		on c.column_name = d.column_name and c.table_name = d.table_name
where
		c.table_schema='public'		
order by
		c.table_name,c.column_name asc;

/*
select 
		*
from
		information_schema.columns
where
		table_schema='public'		
order by
		table_name,column_name asc;
*/		
		