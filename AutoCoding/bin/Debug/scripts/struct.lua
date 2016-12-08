-- ���ɽṹ��ű�
local key_cols = string_split(CSGetKeyCols(), ',')
local nonkey_cols = string_split(CSGetNonKeyCols(), ',')
local col_type = ""
local tmp_str = ""
code = ""
code = code .. [[#ifndef _H_]] .. CSGetTableFullName() .. br
code = code .. [[#define _H_]] .. CSGetTableFullName() .. br
code = code .. [[/**]] .. br
code = code .. [[ * @brief ]] .. get_table_comment() .. br
code = code .. [[ * @details ]] .. br
code = code .. [[ * - �� �� �� �� : ]] .. os.date("%Y/%m/%d") .. br
code = code .. [[ * - �� �� �� �� : ]] .. CSGetConfig("Author") .. "(by AutoCoding)" .. br
code = code .. [[ * - ��Ӧ���ݿ��: ]] .. CSGetTableFullName() .. br
code = code .. [[ * - ���ݿ��ע��: ]] .. get_table_comment() .. br
code = code .. [[ * - ˵       ��: ]] .. br
code = code .. [[ */ ]] .. br
code = code .. [[extern struct ]] .. string.upper(get_struct_name()) .. br
code = code .. [[{]] .. br
for i = 1, #key_cols do
    col_type = get_esql_var_type(key_cols[i])
    tmp_str = tab(1) .. col_type .. "  " .. get_esql_var_type_flag(col_type) .. string.lower(key_cols[i])
    tmp_str = formatstr(tmp_str, 27)
    if col_type == "char" then
        tmp_str = tmp_str .. "[" .. tostring(get_esql_char_var_len(key_cols[i])) .. "]"
    end
    tmp_str = formatstr(tmp_str, 31)
    tmp_str = tmp_str .. ";"
    tmp_str = formatstr(tmp_str, 36)
    tmp_str = tmp_str .. "/* <PK>" .. CSGetColComment(key_cols[i]) .. "*/" .. br
    code = code .. tmp_str
end
for i = 1, #nonkey_cols do
    col_type = get_esql_var_type(nonkey_cols[i])
    tmp_str = tab(1) .. col_type .. "  " .. get_esql_var_type_flag(col_type) .. string.lower(nonkey_cols[i])
    tmp_str = formatstr(tmp_str, 27)
    if col_type == "char" then
        tmp_str = tmp_str .. "[" .. tostring(get_esql_char_var_len(nonkey_cols[i])) .. "]"
    end
    tmp_str = formatstr(tmp_str, 31)
    tmp_str = tmp_str .. ";"
    tmp_str = formatstr(tmp_str, 36)
    tmp_str = tmp_str .. "/* " .. CSGetColComment(nonkey_cols[i]) .. "*/" .. br
    code = code .. tmp_str
end
code = code .. [[} ]]  .. get_struct_var() .. ";" .. br

-- ������غ���ԭ��
code = code .. [[#ifdef  __cplusplus
extern "C" {
#endif
]]
code = code .. "int ES_" .. string.upper(CSGetTableName()) .. "_Insert();" .. br
code = code .. "int ES_" .. string.upper(CSGetTableName()) .. "_LoadAll();" .. br
code = code .. "int ES_" .. string.upper(CSGetTableName()) .. "_Delete();" .. br
code = code .. "int ES_" .. string.upper(CSGetTableName()) .. "_DeleteAll();" .. br
code = code .. [[#ifdef  __cplusplus
}
#endif
]]
-- 
code = code .. br
code = code .. [[#endif  /*]] .. CSGetTableFullName() .. [[*/]] .. br 



filepath = CSGetConfig("TbStructFilePath") .. string.lower(CSGetTableName()) .. ".hxx"
