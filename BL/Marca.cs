using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Marca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaMarca = (from marcaDL in context.Marcas
                                             select new
                                             {
                                                 IdMarca = marcaDL.IdMarca,
                                                 Nombre = marcaDL.Nombre
                                             }).ToList();

                    if (listaMarca != null && listaMarca.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaMarca)
                        {
                            ML.Marca marca = new ML.Marca();
                            marca.IdMarca = obj.IdMarca;
                            marca.Nombre = obj.Nombre;

                            result.Objects.Add(marca);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla no tiene datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
