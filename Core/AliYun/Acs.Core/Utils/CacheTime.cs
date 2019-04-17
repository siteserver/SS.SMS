using System;
using SS.SMS.Core.AliYun.Acs.Core.Profile;

namespace SS.SMS.Core.AliYun.Acs.Core.Utils
{
    public static class CacheTime
    {
        private static DateTime lastClearTime = DateTime.Now;
        private static readonly object syncRoot = new object();

        public static bool CheckCacheIsExpire()
        {

            lock (syncRoot)
            {
                TimeSpan ts = DateTime.Now - lastClearTime;
                if (600 < ts.TotalSeconds)
                {
                    DefaultProfile.ClearLocationEndPoints();
                    lastClearTime = DateTime.Now;
                    return true;
                }

                return false;
            }
        }
    }
}
