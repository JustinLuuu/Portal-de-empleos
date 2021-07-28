using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Portal_de_empleos.Models.General
{
    public class CadenaConexion
    {
        public static string DevolverCadena()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfiguration configuration = configBuilder.Build();
            return configuration.GetConnectionString("Miconexion");
        }
    }
}
