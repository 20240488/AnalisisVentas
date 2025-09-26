using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalisisVentas.Interfaces
{
    public interface IExternalSource<T>
    {
        List<T> Extract();
    }
}