-- ����ͨ�������������
local key_cols = string_split(CSGetKeyCols(), ',')
local nonkey_cols = string_split(CSGetNonKeyCols(), ',')
local all_cols = string_split(CSGetAllCols(), ',')
local tb_name_fmted = string.upper(CSGetTableName())
local gfp = "gfp_" .. string.lower(CSGetTableName())
local table_id = "TTID_" .. string.upper(CSGetTableName())
local g_var = "g_" .. string.lower(CSGetTableName())
local level = ""

code = ""
code = [[#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#ifdef _MSC_VER
#include <io.h>
#else
#include <unistd.h>
#define chsize ftruncate
#endif
#include "cpack.h"
#include "mypub.h"
#include "bupub.h"
#include "smtbase.h"

#include "]] .. string.lower(CSGetTableName()) .. [[.hxx"

#include "tableid.h"

struct ]] .. string.upper(get_struct_name()) .. " " .. g_var .. [[;]] .. br 

code = code .. [[FILE *]] .. gfp .. [[=NULL;

bool ES_Open_]] .. tb_name_fmted .. [[()
{
   if (]] .. gfp.. [[==NULL)
   {
      ]] .. gfp .. [[ = sh_fopen("]] .. CSGetTableName() .. [[.tbl","r+t",SH_DENYNO);
      if (]] .. gfp .. [[==NULL)
      {
         ]] .. gfp .. [[ = sh_fopen("]] .. CSGetTableName() .. [[.tbl","w+t",SH_DENYNO);
      }
   }
   return(]] .. gfp .. [[!=NULL);
}

#define VALIDRECORDMARK    'I'
#define DELETEDRECORDMARK  'D'
#define MAXTABLEBUFLEN 40960
#define MAXTABLEFLEN 1024
int ES_]] .. tb_name_fmted .. [[_Read()
{
   char buf[MAXTABLEBUFLEN];
   char sbuf[MAXTABLEFLEN];
   memset(&]] .. g_var .. [[,0,sizeof(]] .. g_var .. [[));
   if (fgets(buf,sizeof(buf),]] .. gfp .. [[)==NULL)
      return(-1);
   
   if (GetSubString(buf,'|',0,sbuf,sizeof(sbuf),NULL)<=0)
      return(-101);
   if (sbuf[0]!=VALIDRECORDMARK)
      return(0);
   ]]
   for i=1,#all_cols do
      local col_type = get_esql_var_type(all_cols[i])
      code = code .. [[   if (GetSubString(buf,'|',]].. i .. [[,sbuf,sizeof(sbuf),NULL)<=0)
      return(]]
      code = code .. (-1-i) .. [[);
   ]] 
      if col_type=="double" then
          code = code .. g_var .. "." .. get_struct_col_name(all_cols[i]) .. "=atof(sbuf);"
      elseif col_type=="int" then
          code = code .. g_var .. "." .. get_struct_col_name(all_cols[i]) .. "=atoi(sbuf);"
      elseif col_type=="long" then
    	  code = code .. g_var .. "." .. get_struct_col_name(all_cols[i]) .. "=atol(sbuf);"
      else
          code = code .. [[STR_SMF(sbuf,]] .. g_var .. [[.]] .. get_struct_col_name(all_cols[i]) .. [[);]]
      end
      code = code .. br
   end
   
code = code .. [[   return(1);
}

]]


code = code .. "// ��ѯ���е�" .. get_table_comment() .. "��¼" .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_LoadAll()
{
   int n=0;
   int rtn=0;
   if (ES_Open_]] .. tb_name_fmted .. [[())
   {
      fseek(]] .. gfp .. [[,0,SEEK_SET);
      while (rtn!=-1)
      {
         rtn = ES_]] .. tb_name_fmted .. [[_Read();
         if (rtn==1)
         {
            MTT_InsertRecord(TTID_]] .. tb_name_fmted .. [[, &]] .. g_var .. ", sizeof(" .. g_var .. [[));
            ++n;
         }
      }
   }
   return(n);
}


int ES_]] .. tb_name_fmted .. [[_Out()
{
   fprintf(]] .. gfp .. [[,"%c|",VALIDRECORDMARK);]] .. br   
   for i=1,#all_cols do
      local col_type = get_esql_var_type(all_cols[i])
      code = code .. [[   fprintf(]] .. gfp .. ","
      if col_type=="double" then
          code = code .. [["%lf|"]] 
      elseif col_type=="int" then
          code = code .. [["%d|"]]
      elseif col_type=="long" then
    	  code = code .. [["%ld|"]]
      else
          code = code .. [["%s|"]]   
      end
      code = code .. "," .. g_var .. [[.]] .. get_struct_col_name(all_cols[i]) .. [[);]] .. br
   end

code=code .. [[   fprintf(]] .. gfp ..[[,"\n");
   return(0);
}

]]
-- �������
code = code .. "// ����" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_Insert()
{
   if (ES_Open_]] .. tb_name_fmted .. [[())
   {
      fseek(]] .. gfp .. [[,0,SEEK_END);
      ES_]] .. tb_name_fmted .. [[_Out();
      fflush(]] .. gfp .. [[);
      return 0;
   }
   else
      return(-1);
}

]]

local v_tmp = "v_" .. string.lower(CSGetTableName())
-- ɾ�����
code = code .. "// ��������ɾ��" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_Delete()
{
   ]] .. string.upper(get_struct_name()) .. " " .. v_tmp .. [[;
   int rtn;
   if (!ES_Open_]] .. tb_name_fmted .. [[())
      return(-1);
   memcpy(&]] ..v_tmp .. [[,&]] .. g_var .. [[,sizeof(]] .. v_tmp .. [[));
   fseek(]] .. gfp .. [[,0,SEEK_SET);
   while (1)
   {
      long fpos = ftell(]] .. gfp .. [[);
      rtn = ES_]] .. tb_name_fmted .. [[_Read();
      if (rtn==-1)
         break;
      if (rtn==1)
      {
         // ����������Ч��¼����
         if (]]
         for i=1,#key_cols do
              local col_type = get_esql_var_type(key_cols[i])
              if i>1 then
                  code = code .. br .. "           && "
              end
              if col_type=="char" then
                  code = code .. "strcmp(" .. v_tmp .. "." .. get_struct_col_name(key_cols[i]) .. ",".. g_var .. "." .. get_struct_col_name(key_cols[i]) .. ")==0"
              else
                  code = code .. v_tmp .. "." .. get_struct_col_name(key_cols[i]) .. "==" .. g_var .. "." .. get_struct_col_name(key_cols[i])
              end
         end
         code = code .. [[)
         {
            // ���ҵ��˸ü�¼:
            fseek(]] .. gfp .. [[,fpos,SEEK_SET);
            fprintf(]] .. gfp .. [[,"%c|",DELETEDRECORDMARK);
            fflush(]] .. gfp .. [[);
            return(0);
         }
      }
   }
   // û���ҵ���Ҫ��ɾ���ļ�¼:
   return(-200);
}

]]

-- ɾ��ȫ�� ��
code = code .. "// ɾ��ȫ��" .. get_table_comment() .. br
code = code .. "int ES_" .. tb_name_fmted .. [[_DeleteAll()
{
   if (!ES_Open_]] .. tb_name_fmted .. [[())
      return(-1000);
   chsize(fileno(]] .. gfp .. [[),0);
   fclose(]] .. gfp .. [[);
   ]] .. gfp .. [[ = NULL; // �������´�
   return(0);
}
]]    

filepath =CSGetConfig("TbStructFilePath") .. string.lower(CSGetTableName()) .. ".cxx"



