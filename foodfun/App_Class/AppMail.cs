using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;


/// <summary>
/// 系統發出電子信件類別
/// </summary>
public class AppMail : BaseClass
{
    public string UserRegister(string userNo)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var data = db.Users.Where(m => m.account_name == userNo).FirstOrDefault();
            if (data == null) return string.Format("查無使用者代號:{0}!!", userNo);
            if (string.IsNullOrEmpty(data.email)) return "使用者電子信箱空白，無法寄出!!";
            using (GmailService gmail = new GmailService())
            {
                var str_url = string.Format("/User/Verify/{0}", data.varify_code);
                var str_link = HttpContext.Current.Request.Url.AbsoluteUri.
                    Replace(HttpContext.Current.Request.Url.PathAndQuery, str_url);
                string str_subject = string.Format("{0} - 帳號 - {1} 成功建立通知", AppService.AppName, userNo);
                string str_body = "<br/><br/>";
                str_body += "很高興告訴你，你的" + AppService.AppName + "帳戶已經成功建立。<br/>";
                str_body += "請點擊信件中的啟用連結，完成驗證帳號程序。<br/>";
                str_body += "<a href = ' " + str_link + "'>" + str_link + "</a>";
                str_body += "</br><br/>";
                str_body += "本信件由電腦系統自動寄出，請勿回信!!<br/><br/>";
                str_body += string.Format("{0} 系統開發團隊敬上", AppService.AppName);

                gmail.ReceiveEmail = data.email;
                gmail.Subject = str_subject;
                gmail.Body = str_body;
                gmail.Send();
                return gmail.MessageText;

            }
        }

    }

    public string UserForget(string userno)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            var data = db.Users.Where(m => m.account_name == userno).FirstOrDefault();
            var mname = data.mname;
            var password = data.password;

            if (data == null) return string.Format("查無使用者代號:{0}!!", userno);
            if (string.IsNullOrEmpty(data.email)) return "使用者電子信箱空白，無法寄出!!";
            using (GmailService gmail = new GmailService())
            {
                var str_url = string.Format("/User/Login");
                var str_link = HttpContext.Current.Request.Url.AbsoluteUri.
                    Replace(HttpContext.Current.Request.Url.PathAndQuery, str_url);
                string str_body = "<br/><br/>";
                string str_subject = string.Format("忘記密碼通知");
                str_body += "親愛的會員" + mname + "您好: ";
                str_body += "<br/>您的帳號是 : " + userno;
                str_body += "<br/>您的登入密碼是 : " + password;
                str_body += "<br/>請點擊信件中的變更密碼連結，登入後請變更密碼。<br/>";
                str_body += "<a href = ' " + str_link + "'>" + str_link + "</a>";
                str_body += "</br><br/>";
                str_body += "本信件由電腦系統自動寄出，請勿回信!!<br/><br/>";
                str_body += string.Format("{0} 系統開發團隊敬上", AppService.AppName);

                gmail.ReceiveEmail = data.email;
                gmail.Subject = str_subject;
                gmail.Body = str_body;
                gmail.Send();
                return gmail.MessageText;

            }
        }

    }
}
