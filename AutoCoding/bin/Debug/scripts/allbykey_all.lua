-- �������нṹ��ű�
local tables = string_split(CSGetAllTables(), ',')

code=""
for i = 1, #tables do
    CSSetCurTable(tables[i])
    code = code .. CSDoAutoCoding("AllByKey", true) .. br
end
filepath=""