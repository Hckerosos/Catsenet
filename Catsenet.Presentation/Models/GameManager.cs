using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catsenet.Presentation.Models
{
    public class GameManager
    {
        int recorrido = 0;
        bool sentido = true;
        public event EventHandler MainLoopCompleted;
        public bool IsRunning = false;
        public BubbleModel BubbleModelManager;
        public PersonajeModel Personaje;

        public GameManager()
        {
            Personaje = new PersonajeModel();
            BubbleModelManager = new BubbleModel();
        }

        public async Task Avanzar()
        {
            var dado = new Random().Next(1, 7);
            if (recorrido < 0)
                recorrido =  dado;
            await CambiaEstadoDado(dado);
            IsRunning = true;
            var distanciaVertical = 13;
            var distanciaHorizontal =  16;
            while (IsRunning)
            {
                for (int i = 0; i < dado; i++)
                {
                    recorrido++;
                    if (recorrido == 5 | recorrido == 15 | recorrido == 25 | recorrido == 35)
                    {
                        for (int ii = 0; ii < distanciaVertical; ii++)
                        {
                            MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                            await Task.Delay(35);
                            await Personaje.AvanzarVertical(true);
                        }
                        recorrido = 0;
                        if (sentido)
                            sentido = false;
                        else
                            sentido = true;
                    }
                    else
                    {
                        for (int ii = 0; ii < distanciaHorizontal; ii++)
                        {
                            await Personaje.AvanzarHorizontal(sentido);
                            MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                            await Task.Delay(35);
                        }
                    }
                }
                IsRunning = false;
            }
            await Personaje.Quieto(sentido);
        }

        public async Task EstadoCombo(bool _combo, string _descripcion)
        {
            Personaje.combo = _combo;
            Personaje.comboDescripcion = _descripcion;
        }

        public async Task VisibilidadBubble(bool _visibilidad, string _estado)
        {
            BubbleModelManager.visibilidad = _visibilidad;
            BubbleModelManager.estadoDado = _estado;
        }
        public async Task CambiaEstadoDado(int number)
        {
            switch (number)
            {
                case 1:
                    BubbleModelManager.estadoDado = "dado-uno";
                    break;
                case 2:
                    BubbleModelManager.estadoDado = "dado-dos";
                    break;
                case 3:
                    BubbleModelManager.estadoDado = "dado-tres";
                    break;
                case 4:
                    BubbleModelManager.estadoDado = "dado-cuatro";
                    break;
                case 5:
                    BubbleModelManager.estadoDado = "dado-cinco";
                    break;
                case 6:
                    BubbleModelManager.estadoDado = "dado-seis";
                    break;
            }
        }
        public async Task Muere()
        {
            Personaje.horizontal = 8;
            Personaje.vertical = 6;
            sentido = true;
            recorrido = 0;
        }
        public async Task Gana()
        {
            Personaje.horizontal = 72;
            Personaje.vertical = 84;
            Personaje.estado = "personaje-quieto";
        }

    }
}
