-- 生成结构体脚本
local all_cols = string_split(CSGetAllCols(), ',')
local col_type = ""
local tmp_str = ""
code = ""
code = code .. "CREATE TABLE " .. CSGetTableFullName() .. br
code = code .. "(" .. br
for i = 1, #all_cols do
    code = code .. "    " .. formatstr(all_cols[i],32) .. formatstr(string.upper(get_db_col_type(all_cols[i])), 12)
    code = code .. "DEFAULT " .. formatstr(get_db_col_default(all_cols[i]), 4) 
    code = code .. "NOT NULL COMMENT '" .. CSGetColComment(all_cols[i]) .. "'," .. br
end
code = code .. "    PRIMARY KEY (" .. CSGetKeyCols() .. ")" .. br
code = code .. ")" .. "COMMENT = '" .. CSGetTableComment() .. "';" .. br .. br

filepath = ""
