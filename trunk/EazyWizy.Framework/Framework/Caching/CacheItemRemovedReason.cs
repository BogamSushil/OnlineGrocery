using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EazyWizy.Framework.Caching
{
    public enum CacheItemRemovedReason
    {
        Expired,
        Removed,
        Evicted,
        Other
    }
}
