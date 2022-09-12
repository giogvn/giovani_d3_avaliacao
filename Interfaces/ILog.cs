using giovani_d3_avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace giovani_d3_avaliacao.Interfaces
{
    internal interface ILog
    {
        void RegisterAccess(User user);
    }
}