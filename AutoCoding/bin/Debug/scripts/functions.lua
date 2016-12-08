-- 生成结构体脚本
-- 参数:待分割的字符串,分割字符-- 返回:子串表.(含有空串)
function string_split(str, split_char)
    local sub_str_tab = {};
    while (true) do
        local pos = string.find(str, split_char);
        if (not pos) then
            sub_str_tab[#sub_str_tab + 1] = str;
            break;
        end
        local sub_str = string.sub(str, 1, pos - 1);
        sub_str_tab[#sub_str_tab + 1] = sub_str;
        str = string.sub(str, pos + 1, #str);
    end
    return sub_str_tab;
end

-- 多个tab(四个空格)
function tab(cnt)
    local str = ""
    for i = 1, cnt do
        str = str .. "    "
    end
    return str
end

-- 根据数据库列名获取ESQL变量类型
function get_esql_var_type(col_name)
    local db_var_type = string.lower(CSGetColType(col_name))
    if db_var_type=="char" or db_var_type=="varchar" or db_var_type=="time" or db_var_type=="date" or db_var_type=="timestamp" then
        return "char"
    elseif db_var_type=="double" or db_var_type=="decimal" or db_var_type=="dec" then
        return "double"
    elseif db_var_type=="integer" or db_var_type=="int" then
        return "long"
    elseif db_var_type=="smallint" then
        return "short"
    else
        return ""
    end
end

-- 获取数据库列长度
function get_esql_char_var_len(col_name)
    return CSGetColSize1(col_name) + 1
end

-- 获取ESQL变量类型前缀
function get_esql_var_type_flag(col_type)
    if col_type=="double" then
        return "d"
    elseif col_type=="int" then
        return "i"
    elseif col_type=="long" then
    	return "l"
    elseif col_type=="short" then
        return "sh"
    elseif col_type=="char" then
        return "s"
    else
        return ""
    end
end

-- 获取格式化时用的标志
function get_var_type_fmt_flag(col_name)
    local col_type = get_esql_var_type(col_name)
    if col_type=="double" then
        return "%f"
    elseif col_type=="int" then
        return "%d"
    elseif col_type=="long" then
    	return "%ld"
    elseif col_type=="sqlint32" then
        return "%d"
    elseif col_type=="short" then
        return "%d"
    elseif col_type=="char" then
        return "'%s'"
    else
        return ""
    end
end

-- 根据数据库列名称获取结构体中变量名称
function get_struct_col_name(col_name)
    local col_type = get_esql_var_type(col_name)
    return get_esql_var_type_flag(col_type) .. string.lower(col_name)
end

-- 格式化字符串，右补空格
function formatstr(str, len)
    local fmtlen = len
    if fmtlen < #str then fmtlen = #str + 1 end
    local fmt = "%-" .. tostring(fmtlen) .. "s"
    return string.format(fmt, str)
end

-- 换行
br="\r\n"

-- 获取结构体名称(全大写)
function get_struct_name()
    return "ST_" .. string.upper(CSGetTableName())
end

-- 获取结构体全局变量
function get_struct_var()
    return "g_" .. string.lower(CSGetTableName())
end

function get_esql_select(cols)
    local code = ""
    local nIndex = 0
    -- SELECT部分
    code = code .. tab(2) .. "SELECT" .. br
    code = code ..  tab(3)
    for i = 1, #cols do
        if i == #cols then
            code = code ..  cols[i];
            nIndex = nIndex + 1;
        else 
            code = code ..  cols[i] .. ", ";
            nIndex = nIndex + 1
            if nIndex == 6 then
                code = code ..  br;
                code = code ..  tab(3);
                nIndex = 0;
            end
        end
    end
    code = code ..  br;
    
    return code
end

function get_struct_var_name(col_name)
    return get_struct_var() .. "." .. get_struct_col_name(col_name)
end

function get_esql_into(cols)
    local code = ""
    local nIndex = 0
    -- INTO部分
    code = code .. tab(2) .. "INTO" .. br
    code = code ..  tab(3)
    for i = 1, #cols do
        if i == #cols then
            code = code ..  ":" .. get_struct_var_name(cols[i])
            nIndex = nIndex + 1;
        else 
            code = code .. ":" ..  get_struct_var_name(cols[i]) .. ", ";
            nIndex = nIndex + 1
            if nIndex == 6 then
                code = code ..  br;
                code = code ..  tab(3);
                nIndex = 0;
            end
        end
    end
    return code
end

function get_esql_where(cols)
    local code = ""
    code = code .. tab(2) .. "WHERE" .. br
    for i = 1, #cols do
        code = code .. tab(3)
        if i > 1 then
            code = code .. "AND "
        end
        code = code .. cols[i] .. " = :" .. get_struct_var_name(cols[i])
        if i == #cols then
            code = code .. ";"
        end
        code = code .. br
    end
    return code
end

function get_esql_exec_deal(cols, oper_type, level)
    local code = ""
    local strFmt = ""
    local strVar = ""
    local iMaxCountPerRow = 6
    local iIndex = 0
    code = code .. tab(1) .. "if(SQLCODE)" .. br
    code = code .. tab(1) .. "{" .. br
    code = code .. tab(2) .. "SQLDebugLog(" .. level .. ", \"Fail to " .. oper_type .. " table " .. CSGetTableFullName() .. ":"
    for i = 1, #cols do
        if iIndex == iMaxCountPerRow then 
            iIndex = 1
            strFmt = strFmt .. "\"" .. br .. tab(3) .. "\""
            strVar = strVar .. br .. tab(3)
        end
        if i == #cols then
            strFmt = strFmt .. get_struct_var_name(cols[i]) .. "=" .. get_var_type_fmt_flag(cols[i])
            strVar = strVar .. get_struct_var_name(cols[i])
        else
            strFmt = strFmt .. get_struct_var_name(cols[i]) .. "=" .. get_var_type_fmt_flag(cols[i]) .. ","
            strVar = strVar .. get_struct_var_name(cols[i]) .. ","
        end
    end
    strFmt = strFmt .. ",SQLCODE=%d,File=%s,Line=%d"
    strVar = strVar .. ",SQLCODE,__FILE__,__LINE__"
    code = code .. strFmt .. "\"," .. br
    code = code .. tab(3) .. strVar .. ");" .. br
    code = code .. tab(2) .. "return SQLCODE;\r\n"
    code = code .. tab(1) .. "}" .. br
    return code
end

function trim_cols(cols)
    local code = ""
    for i = 1, #cols do
        if get_esql_var_type(cols[i]) == "char" then
            code = code .. tab(1) .. "mytrim(" .. get_struct_var_name(cols[i]) .. ");" .. br
        end
    end
    return code
end

function smf_trim(cols)
    local code = ""
    for i = 1, #cols do
        if get_esql_var_type(cols[i]) == "char" then
            code = code .. tab(1) .. "SMF_TRIM(" .. get_struct_var_name(cols[i]) .. ");" .. br
        end
    end
    return code
end
	

function get_esql_insert(cols)
    local code = ""
    local iIndex = 0
    code = code .. tab(1) .. "EXEC SQL" .. br
    code = code .. tab(2) .. "INSERT INTO "  .. CSGetTableFullName() .. br
    code = code .. tab(3) .. "("
    for i = 1, #cols do
        code = code .. cols[i]
        if i < #cols then
            code = code .. ","
            iIndex = iIndex + 1
            if iIndex == 6 then
                code = code .. br
                code = code .. tab(3)
                iIndex = 0
            end
        end
    end
    code = code .. ")" .. br
    code = code .. tab(2) .. "VALUES" .. br
    code = code .. tab(3) .. "("
    iIndex = 0
    for i = 1, #cols do
        code = code .. ":" .. get_struct_var_name(cols[i])
        if i < #cols then
            code = code .. ","
            iIndex = iIndex + 1
            if iIndex == 6 then
                code = code .. br
                code = code .. tab(3)
                iIndex = 0
            end
        end
    end
    code = code .. ");" .. br
    return code
end

function get_esql_update_set(cols)
    local code = ""
    code = code .. tab(2) .. "SET" .. br
    for i = 1, #cols do
        code = code .. tab(3) .. cols[i] .. " = :" .. get_struct_var_name(cols[i])
        if i < #cols then
            code = code .. ","
        end
        code = code .. br
    end
    return code
end

function get_table_comment()
    local comment = CSGetTableComment()
    if comment == nil then comment = "" end
    return comment
end

-- 根据数据库列类型
function get_db_col_type(col_name)
    local db_var_type = string.lower(CSGetColType(col_name))
    if db_var_type=="char" or db_var_type=="varchar" then
        if(tonumber(CSGetColSize1(col_name)) > 255) then
            return "varchar(" .. CSGetColSize1(col_name) .. ")"
        else
            return "char(" .. CSGetColSize1(col_name) .. ")"
        end
    elseif db_var_type=="decimal" then
        return "decimal(" .. CSGetColSize1(col_name) .. "," .. CSGetColSize2(col_name) .. ")"
    elseif db_var_type=="integer" or db_var_type=="int" then
        return db_var_type
    elseif db_var_type=="smallint" then
        return db_var_type
    else
        return db_var_type
    end
end

function get_db_col_default(col_name)
    local db_var_type = string.lower(CSGetColType(col_name))
    if db_var_type=="char" or db_var_type=="varchar" then
        return "''"
    else
        return "0"
    end
end

