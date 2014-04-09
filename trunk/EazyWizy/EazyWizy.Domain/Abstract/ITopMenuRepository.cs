using System.Collections.Generic;
using System.Linq;
using EazyWizy.Domain.Entities;

namespace EazyWizy.Domain.Abstract
{
    public interface ITopMenuRepository
    {
        IEnumerable<TopMenu> TopMenu { get; }
    }
}
