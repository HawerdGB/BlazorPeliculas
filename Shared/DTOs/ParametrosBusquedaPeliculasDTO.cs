using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPeliculas.Shared.DTOs
{
    public class ParametrosBusquedaPeliculasDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistros { get; set; } = 10;
        public PaginacionDTO PaginacionDTO
        {
            get
            {
                return new PaginacionDTO() { Pagina = this.Pagina, CantidadRegistros = this.CantidadRegistros };
            }
        }

        public int GeneroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool EnCartelera { get; set; }
        public bool Estrenos { get; set; }
        public bool MasVotadas { get; set; }
    }
}
