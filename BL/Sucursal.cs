using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaSucursal = (from sucursalDL in context.Sucursals
                                         select new
                                         {
                                             IdSucursal = sucursalDL.IdSucursal,
                                             Nombre = sucursalDL.Nombre,
                                             Calle = sucursalDL.Calle,
                                             NumeroInterior = sucursalDL.NumeroInterior,
                                             NumeroExterior = sucursalDL.NumeroExterior,
                                             CP = sucursalDL.Cp,
                                             Colonia = sucursalDL.Colonia,
                                             Municipio = sucursalDL.Municipio,
                                             Estado = sucursalDL.Estado,
                                             PaginaWeb = sucursalDL.PaginaWeb
                                         });
                    if (listaSucursal != null && listaSucursal.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaSucursal)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();

                            sucursal.IdSucursal = obj.IdSucursal;
                            sucursal.Nombre = obj.Nombre;
                            sucursal.Calle = obj.Calle;
                            sucursal.NumeroInterior = obj.NumeroInterior;
                            sucursal.NumeroExterior = obj.NumeroExterior;
                            sucursal.CP = obj.CP;
                            sucursal.Colonia = obj.Colonia;
                            sucursal.Municipio = obj.Municipio;
                            sucursal.Estado = obj.Estado;
                            sucursal.PaginaWeb = obj.PaginaWeb;

                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos de sucursales"; ;
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
        public static ML.Result GetById(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from sucursalDL in context.Sucursals
                                 where sucursalDL.IdSucursal == IdSucursal
                                 select sucursalDL).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Sucursal sucursal = new ML.Sucursal();

                        sucursal.IdSucursal = query.IdSucursal;
                        sucursal.Nombre = query.Nombre;
                        sucursal.Calle = query.Calle;
                        sucursal.NumeroInterior = query.NumeroInterior;
                        sucursal.NumeroExterior = query.NumeroExterior;
                        sucursal.CP = query.Cp;
                        sucursal.Colonia = query.Colonia;
                        sucursal.Municipio = query.Municipio;
                        sucursal.Estado = query.Estado;
                        sucursal.PaginaWeb = query.PaginaWeb;

                        result.Object = sucursal;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos con ese id de producto";
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
        public static ML.Result Add(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    DL.Sucursal DLSucursal = new DL.Sucursal();
                    DLSucursal.Nombre = sucursal.Nombre;
                    DLSucursal.Calle = sucursal.Calle;
                    DLSucursal.NumeroInterior = sucursal.NumeroInterior;
                    DLSucursal.NumeroExterior = sucursal.NumeroExterior;
                    DLSucursal.Cp = sucursal.CP;
                    DLSucursal.Colonia = sucursal.Colonia;
                    DLSucursal.Municipio = sucursal.Municipio;
                    DLSucursal.Estado = sucursal.Estado;
                    DLSucursal.Estado = sucursal.Estado;
                    DLSucursal.PaginaWeb = sucursal.PaginaWeb;
                    context.Sucursals.Add(DLSucursal);

                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar la sucursal";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from sucursalDL in context.Sucursals
                                 where sucursalDL.IdSucursal == sucursal.IdSucursal
                                 select sucursalDL).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdSucursal = sucursal.IdSucursal;
                        query.Nombre = sucursal.Nombre;
                        query.Calle = sucursal.Calle;
                        query.NumeroInterior = sucursal.NumeroInterior;
                        query.NumeroExterior = sucursal.NumeroExterior;
                        query.Cp = sucursal.CP;
                        query.Colonia = sucursal.Colonia;
                        query.Municipio = sucursal.Municipio;
                        query.Estado = sucursal.Estado;
                        query.Estado = sucursal.Estado;
                        query.PaginaWeb = sucursal.PaginaWeb;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar la sucursal";
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
        public static ML.Result Delete(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from sucursalDL in context.Sucursals
                                 where sucursalDL.IdSucursal == IdSucursal
                                 select sucursalDL).First();
                    if (query != null)
                    {
                        context.Sucursals.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la sucursal";
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
