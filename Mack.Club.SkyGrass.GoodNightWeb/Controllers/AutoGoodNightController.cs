using LxcLibrary.Cache.MySql.lxc_pro_db.auto_good_night_log;
using LxcLibrary.WebBase.Controllers;
using Mack.Club.SkyGrass.GoodNightWeb.BLL;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mack.Club.SkyGrass.GoodNightWeb.Controllers
{
    public class AutoGoodNightController: BaseController
    {
        public ActionResult ReadLocalDB()
        {
            string dbConnectionStr = "";
            string sql = $@"select * from log where priority = 12
                                and name = '[↓]群组消息'
                                and detail like '%#key#%'
                                and detail like '%群: ***%'";

            SQLiteManager manager = new SQLiteManager();

            SQLiteDataReader reader = manager.GetDataReader(dbConnectionStr, sql);

            List<string> details = new List<string>();
            while (reader.Read())
            {
                details.Add(Convert.ToString(reader["detail"]));
            }

            return View();
        }

        public ActionResult ReadRemoteDB()
        {
            return View();
        }

        public JsonResult Api()
        {
            string action = GetStrParams("t");
            JsonResult result = null;

            switch (action)
            {
                case "GetGoodNightMsgList":
                    result = GetGoodNightMsgList();
                    break;
                default:
                    result = MissFunction();
                    break;
            }

            return result;
        }

        public JsonResult GetGoodNightMsgList()
        {
            VerifySign();

            long from_group = GetLongParams("from_group");

            CheckFromGroupToToken();

            string good_night_key = GetStrParams("good_night_key");
            good_night_key = $"#{good_night_key}#";

            string dbConnectionStr = "Database=***;Data Source=***;User ID=***;Password=***;CharSet=utf8mb4;SslMode=None;Connect Timeout=60;";
            AutoGoodNightLogCache cache = new AutoGoodNightLogCache();
            cache.Init(dbConnectionStr);
            List<AutoGoodNightLogModel> list = cache.GetModelList(Convert.ToUInt64(from_group), good_night_key);

            Dictionary<string, object> ret = new Dictionary<string, object>();
            ret["errcode"] = 0;
            ret["data"] = list;
            ret["message"] = "";

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
    }
}