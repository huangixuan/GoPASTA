using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using foodfun.Models;

/// <summary>
/// 應用程式服務類別
/// </summary>
public static class AppService
{
    /// <summary>
    /// 應用程式名稱
    /// </summary>
    public static string AppName
    {
        get
        {
            object obj_value = WebConfigurationManager.AppSettings["AppName"];
            return (obj_value == null) ? "未定義名稱" : obj_value.ToString(); 
        }
    }


    /// <summary>
    /// 除錯模式
    /// </summary>
    public static bool DebugMode
    {
        get
        {
            object obj_value = WebConfigurationManager.AppSettings["DebugMode"];
            string str_value = (obj_value == null) ? "0" : obj_value.ToString();
            return (str_value == "1");
        }
    }


    /// <summary>
    /// 後台套版
    /// </summary>
    /// <summary>
    /// 後台套版
    /// </summary>
    public static List<ProgramList> GetModuleList(EnumList.LoginRole role)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            List<ProgramList> programLists;

            if (role == EnumList.LoginRole.Staff)
            {
                programLists = db.ProgramList.Where(m => m.parentid == 0 &&
               m.filter_role == "S").OrderBy(m => m.pro_no).ToList();
            }
            else
            {
                programLists = db.ProgramList
                .Where(m => m.parentid == 0)
                .OrderBy(m => m.pro_no)
                .ToList();


            }
            return programLists;

        }

    }
    public static List<ProgramList> GetProgramLists(int parentID, EnumList.LoginRole role)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            List<ProgramList> programLists;

            if (role == EnumList.LoginRole.Staff)
            {
                programLists = db.ProgramList
                .Where(m => m.parentid == parentID && m.filter_role == "S")
                .OrderBy(m => m.pro_no)
                .ToList();

            }
            else
            {
                programLists = db.ProgramList
                .Where(m => m.parentid == parentID)
                .OrderBy(m => m.pro_no)
                .ToList();

            }
            return programLists;
        }

    }

}
