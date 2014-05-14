using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EazyWizy.Framework.Caching
{
    public class CacheItemRemovedEventArgs : EventArgs
    {
        public string ItemName { get; private set; }
        public CacheItemRemovedReason Reason { get; private set; }

        internal CacheItemRemovedEventArgs(string itemName, CacheItemRemovedReason reason)
        {
            this.ItemName = itemName;
            this.Reason = reason;
        }
    }
}
