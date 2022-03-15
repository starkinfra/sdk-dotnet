using StarkInfra;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public static class TestUtils
    {
        internal static void Log(object log)
        {
            //Console.WriteLine(log);
        }
        
        static public string GetEndToEndId()
        {
            string endToEndId = "";
            List<PixRequest> page;
            string cursor = null;
            while (endToEndId == "")
            {
                (page, cursor) = PixRequest.Page(limit: 5, cursor: cursor);
                foreach (PixRequest entity in page)
                {
                    if ((entity.Amount > 5) && (entity.Flow == "in") && (entity.Status == "success"))
                    {
                        endToEndId = entity.EndToEndId;
                    }
                }

                if (cursor == null) { break; }
            }
            return endToEndId;
        }
    }
}
