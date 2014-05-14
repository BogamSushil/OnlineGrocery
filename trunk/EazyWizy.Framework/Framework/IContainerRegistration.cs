using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace EazyWizy.Framework
{
    public interface IContainerRegistration
    {
        void Register(IUnityContainer container, string name);
    }
}
