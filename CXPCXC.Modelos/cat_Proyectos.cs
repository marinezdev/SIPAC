﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cat_Proyectos
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Activo { get; set; }
    }
}
