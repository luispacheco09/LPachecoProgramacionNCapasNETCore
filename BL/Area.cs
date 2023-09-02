using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {

                    var listaArea = (from areaDL in context.Areas
                                     select new
                                     {
                                         IdArea = areaDL.IdArea,
                                         Nombre = areaDL.Nombre
                                     }).ToList();

                    if (listaArea != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaArea)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;

                            result.Objects.Add(area);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabala no tiene datos";
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
