-- 生成所有结构体脚本
local tables = string_split(CSGetAllTables(), ',')

code = ""
for i = 1, #tables do
    CSSetCurTable(tables[i])
    code = code .. CSDoAutoCoding("Struct", true) .. br
end
filepath=""

