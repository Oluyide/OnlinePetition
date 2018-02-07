using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLocalGovt.Infrastructure
{
    public class SessionFacade
    {
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}