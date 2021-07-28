using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Portal_de_empleos.Controllers.Recursos.Enumerados
{
    public enum Ordenar
    {    
        [Display(Name = "Ordenar por Título")]
        TITULO = 1,
        [Display(Name = "Ordenar por Sueldo")]
        SUELDO = 2,        
        [Display(Name = "Ordenar por Modalidad")]
        MODALIDAD = 3,
        [Display(Name = "Ordenar por Categoría")]
        CATEGORIA = 4,
        [Display(Name = "Ordenar por Provincia")]
        PROVINCIA = 5
    }
}
