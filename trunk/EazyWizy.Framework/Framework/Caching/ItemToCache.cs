using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EazyWizy.Framework.Caching
{
    public class ItemToCache
    {
        public object Item { get; set; }
        public short TimeoutInSeconds { get; set; }

        public ItemToCache(object item) : this(item, short.MaxValue)
        {
        }

        public ItemToCache(object item, short timeoutInSeconds)
        {
            this.Item = item;
            this.TimeoutInSeconds = timeoutInSeconds;
        }
    }
}
