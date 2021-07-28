using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal_de_empleos.Controllers.Recursos;

namespace Portal_de_empleos.Models.General.Interfaces
{
    interface IUpdate
    {
        (string,bool) Actualizar();
    }
}
