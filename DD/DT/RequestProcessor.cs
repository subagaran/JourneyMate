using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWatcherSQLUpdater
{
    public static class RequestProcessor
    {
        public static string Handle(Request req)
        {
            switch (req.action)
            {
                case "get_items":
                    return JsonConvert.SerializeObject(Database.GetItems());

                case "update_qty":
                    var output = Database.UpdateQty(req.itemId, req.qty);
                    return JsonConvert.SerializeObject(output);

                default:
                    return JsonConvert.SerializeObject(new { error = "Invalid action" });
            }
        }
    }
}
