using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaDepartamento = (from departamentoDL in context.Departamentos
                                             select new
                                             {
                                                 IdDepartamento = departamentoDL.IdDepartamento,
                                                 Nombre  = departamentoDL.Nombre
                                             }).ToList();

                    if (listaDepartamento != null && listaDepartamento.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaDepartamento)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            result.Objects.Add(departamento);
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
        public static ML.Result GetByIdArea(int? IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaDepartamentos = (from deparatementoDL in context.Departamentos
                                              join area in context.Areas on deparatementoDL.IdArea equals area.IdArea
                                              where deparatementoDL.IdArea == IdArea
                                              select new
                                              {
                                                  IdDepartamento = deparatementoDL.IdDepartamento,
                                                  Nombre = deparatementoDL.Nombre,
                                                  IdArea = deparatementoDL.IdArea,
                                              }).ToList();



                    if (listaDepartamentos != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in listaDepartamentos)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;
                            //departamento.Area.IdArea = obj.IdArea;

                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla no contiene datos";
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
