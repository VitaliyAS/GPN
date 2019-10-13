using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    abstract public class BaseEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }
}
