-- 生成表结构TABLE_ID
local tables = string_split(CSGetAllTables(), ',')
code = [[#ifndef H_TABLEINDEX
#define H_TABLEINDEX
]]
code = code .. "enum TABLE_ID" .. br
code = code .. "{" .. br
for i = 1, #tables do
    code = code .. tab(1) .. "TTID_" .. string.upper(tables[i]) .. "_PK,		// ".. i-1 .. br
end
code = code .. tab(1) .. "TTID_TABLES " .. br
code = code .. "};" .. br .. br

-- TABLE_INDEX
code = code .. "// 表索引ID" .. br
code = code .. "enum TABLE_INDEX" .. br
code = code .. "{" .. br
for i = 1, #tables do
    CSSetCurTable(tables[i])
    code = code .. tab(1) .. "IDX_" .. string.upper(tables[i]) .. " = 0,		// " .. CSGetTableComment() .. "-主键索引" .. br
end
code = code .. "};" .. br .. br

for i=1, #tables do
    CSSetCurTable(tables[i])   
    code = code .. [[#include "]] .. string.lower(CSGetTableName()) .. [[.hxx"]] .. br
end
code = code .. br .. br

code = code .. "#ifdef BCCSMT" .. br
code = code .. "// 将要复制到数据库初始化文件中的代码: " .. br
code = code .. "T_TABLEDEF g_AllTablesDef\[TTID_TABLES\]={"
for i=1, #tables do
    CSSetCurTable(tables[i])   
    code = code .. br .. tab(1) .. "{\"" .. tables[i] .. "\",0,sizeof(".. get_struct_var() .. "), 1},"
end
code = code  .. br .. "};" .. br


code = code .. br .. "T_TABLEDEF2 g_AllTablesDef2\[TTID_TABLES\]={"
for i=1,#tables do
    CSSetCurTable(tables[i])   
    code = code .. br .. tab(1) .. [[{"]] .. tables[i] .. [[", &]] .. get_struct_var() .. ","
    code = code .. "&ES_" .. string.upper(CSGetTableName()) .. "_Insert,"
    code = code .. "&ES_" .. string.upper(CSGetTableName()) .. "_Delete,"
    code = code .. "&ES_" .. string.upper(CSGetTableName()) .. "_DeleteAll,"
    code = code .. "&ES_" .. string.upper(CSGetTableName()) .. "_LoadAll},"
end
code = code .. br .. "};" .. br .. br

code = code .. "#define ALLTINDEXES " .. #tables .. br
code = code .. "T_TINDEXDEF g_AllTablesIndexes[ALLTINDEXES] = {" .. br
for i=1,#tables do
    CSSetCurTable(tables[i])
    code = code .. "    {TTID_" .. CSGetTableName() .. ", IDX_" .. CSGetTableName() .. "_PK, 0, "
    local key_cols = string_split(CSGetKeyCols(), ',')
    if #key_cols == 1 then
        if get_esql_var_type(key_cols[1]) == "long" then
            code = code .. "SHTKT_LONG"
        else
            code = code .. "sizeof("  .. get_struct_var_name(key_cols[1]) .. ")"
        end
    else
        code = code .. "sizeof(" .. get_struct_var_name(key_cols[#key_cols]) .. ") + " .. "(int)((char*)&(" .. get_struct_var_name(key_cols[#key_cols]) .. ")-(char*)&(" .. get_struct_var() .. "))"
    end
    code = code .. "}," .. br
end
code = code .. "};" .. br .. br

code = code .. [[#else
extern T_TABLEDEF g_AllTablesDef[TTID_TABLES];
extern T_TABLEDEF2 g_AllTablesDef2[TTID_TABLES];
extern T_TINDEXDEF g_AllTablesIndexes[ALLINDEXES];
#endif

#endif
]]

filepath = CSGetConfig("TbStructFilePath") .. "tableid.h"

