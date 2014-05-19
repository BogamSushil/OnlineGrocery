using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Category
    {
        public long CategoryId { get; set; }

        public long? ParentCategoryId { get; set; }

        public string ImageName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}
