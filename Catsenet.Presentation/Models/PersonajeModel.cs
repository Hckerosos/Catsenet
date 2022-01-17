using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catsenet.Presentation.Models
{
    public class PersonajeModel
    {
        readonly int velocidad = 1;
        public int vertical { get; set; } = 6;
        public int horizontal { get; set; } = 8;
        public string estado { get; set; } = "personaje-quieto";
        public bool combo { get; set; } = false;
        public string comboDescripcion  { get; set; } = "COMBO X1";

        public async Task AvanzarHorizontal(bool left)
        {
            if (left)
            {
                estado = "correr-left";
                horizontal += velocidad;
            }
            else
            {
                estado = "personaje-correr";
                horizontal -= velocidad;
            }
        }
        public async Task AvanzarVertical(bool top)
        {
            if (top)
            {
                estado = "correr-left";
                vertical += velocidad;
            }
            else
            {
                estado = "personaje-correr";
                vertical -= velocidad;
            }
        }

        public  async Task Quieto(bool left)
        {
           
                if (left)
                    estado = "quieto-left";
                else
                    estado = "personaje-quieto";
        }

        public async Task Descanso(bool left)
        {
            if (left)
                estado = "descanso-left";
            else
                estado = "personaje-descanso";
        }
    }
}
