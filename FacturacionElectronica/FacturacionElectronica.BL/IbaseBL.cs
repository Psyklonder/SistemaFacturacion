using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public interface IbaseBL
    {
        List<object> ObtenerTodos();

        object ObtenerId(int id);

        object Guardar(object registro);

        object Editar(object registro);

        void Borrar(int id);
    }
}
