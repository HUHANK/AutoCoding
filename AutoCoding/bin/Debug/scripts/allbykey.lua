-- 生成通过主键访问语句
local key_cols = string_split(CSGetKeyCols(), ',')
local nonkey_cols = string_split(CSGetNonKeyCols(), ',')
local all_cols = string_split(CSGetAllCols(), ',')
code = ""
local tb_name_fmted = string.upper(CSGetTableName())
local table_id = "TTID_" .. string.upper(CSGetTableName())
local cursor_id = "C_" .. string.upper(CSGetTableName())
local level = ""

code = code .. [[#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "cpack.h"
#include "mypub.h"
#include "bupub.h"
#include "smtbase.h"

EXEC SQL INCLUDE sqlca;

EXEC SQL BEGIN DECLARE SECTION;
EXEC SQL INCLUDE ']] .. string.lower(CSGetTableName()) .. [[.hxx';
EXEC SQL END DECLARE SECTION;

#include "tableid.h"

struct ]] .. string.upper(get_struct_name()) .. " " .. get_struct_var() .. [[;]] .. br 

-- 全部记录查询：
code = code .. "// 查询所有的" .. get_table_comment() .. "记录" .. br
code = code .. "int ES_" .. tb_name_fmted .. "_SelectAll()" .. br
code = code .. [[{
	int n=0;
	EXEC SQL DECLARE ]] .. cursor_id .. [[ CURSOR  FOR
 ]] .. get_esql_select(all_cols) .. [[
	FROM ]] .. CSGetTableFullName() .. [[;
		
	EXEC SQL OPEN ]] .. cursor_id .. [[;
	
	while (SQLCODE==0) 
	{
		memset(&]] .. get_struct_var() .. [[, 0, sizeof(]] .. get_struct_var() .. [[));
		EXEC SQL FETCH ]] .. cursor_id .. br .. get_esql_into(all_cols) .. [[;
		if (SQLCODE)
			break;
]]
		
code = code .. smf_trim(all_cols)
code = code .. [[
		MTT_InsertRecord(]] .. table_id .. ", &" .. get_struct_var() .. ", sizeof(" .. get_struct_var() .. [[));
		++n;
	}
	
	EXEC SQL CLOSE ]] .. cursor_id .. [[;
	return(n);
}]] .. br

-- 根据主键查询语句
code = code .. "// 根据主键查询" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_Select()
{
    EXEC SQL
]] .. get_esql_select(nonkey_cols) .. [[
]] .. get_esql_into(nonkey_cols) .. [[ 
     FROM ]] .. CSGetTableFullName() .. [[ 
]] .. get_esql_where(key_cols)
level = [[21000 + ]] .. table_id
code = code .. get_esql_exec_deal(key_cols, "select", level)
code = code .. trim_cols(nonkey_cols)
code = code .. tab(1) .. "return 0;" .. br
code = code .. "}" .. br

-- 插入语句
code = code .. "// 插入" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. "_Insert()" .. br
code = code .. "{" .. br
code = code .. get_esql_insert(all_cols)
level = [[22000 + ]] .. table_id
code = code .. get_esql_exec_deal(all_cols, "insert", level)
code = code .. tab(1) .. "return 0;" .. br
code = code .. "}" .. br

-- 更新语句
code = code .. "// 根据主键更新" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. "_Update()" .. br
code = code .. "{" .. br
code = code .. tab(1) .. "EXEC SQL" .. br
code = code .. tab(2) .. "UPDATE " .. CSGetTableFullName() .. br
code = code .. get_esql_update_set(nonkey_cols)
code = code .. get_esql_where(key_cols)
level = [[23000 + ]] .. table_id
code = code .. get_esql_exec_deal(key_cols, "update", level)
code = code .. tab(1) .. "return 0;" .. br
code = code .. "}" .. br

-- 删除语句
code = code .. "// 根据主键删除" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. "_Delete()" .. br
code = code .. "{" .. br
code = code .. tab(1) .. "EXEC SQL" .. br
code = code .. tab(2) .. "DELETE FROM " .. CSGetTableFullName() .. br
code = code .. get_esql_where(key_cols)
level = [[24000 + ]] .. table_id
code = code .. get_esql_exec_deal(key_cols, "delete", level)
code = code .. tab(1) .. "return 0;" .. br
code = code .. "}" .. br

-- 删除全部 ：
code = code .. "// 删除全部" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_DeleteAll()
{
    EXEC SQL DELETE FROM ]] .. CSGetTableFullName() .. [[;
}
]]    

filepath =CSGetConfig("TbStructFilePath") .. string.lower(CSGetTableName()) .. ".cxx"



