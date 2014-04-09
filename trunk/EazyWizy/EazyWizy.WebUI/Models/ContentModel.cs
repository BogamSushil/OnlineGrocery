using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EazyWizy.Domain;
using EazyWizy.Domain.Abstract;
using EazyWizy.Domain.Entities;

namespace EazyWizy.WebUI.Models
{
    public class ContentModel
    {
        public IEnumerable<TopMenu> TopMenu { get; set; }

    }
}