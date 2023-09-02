﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL
{
    public class SucursalProducto
    {
        public static ML.Result GetbySucursal(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaSucursal = (from sucursalPDL in context.SucursalProductos
                                         join sucursal in context.Sucursals on sucursalPDL.IdSucursal equals sucursal.IdSucursal
                                         join producto in context.Productos on sucursalPDL.IdProducto equals producto.IdProducto
                                         where sucursalPDL.IdSucursal == IdSucursal
                                         select new
                                         {
                                             IdSucursalProducto = sucursalPDL.IdSucursalProducto,
                                             IdSucursal = sucursalPDL.IdSucursal,
                                             IdProducto = sucursalPDL.IdProducto,
                                             Stock = sucursalPDL.Stock,
                                             ProductoNombre = producto.Nombre,
                                             SucursalNombre = sucursal.Nombre
                                         });

                    if (listaSucursal != null && listaSucursal.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaSucursal)
                        {
                            ML.SucursalProducto sucursalp = new ML.SucursalProducto();
                            sucursalp.IdSucursalProducto = obj.IdSucursalProducto;
                            sucursalp.Stock = obj.Stock;

                            sucursalp.Sucursal = new ML.Sucursal();
                            sucursalp.Sucursal.IdSucursal = obj.IdSucursal.Value;
                            sucursalp.Sucursal.Nombre = obj.SucursalNombre;
                            sucursalp.Producto = new ML.Producto();
                            sucursalp.Producto.IdProducto = obj.IdProducto.Value;
                            sucursalp.Producto.Nombre = obj.ProductoNombre;
                           

                            result.Objects.Add(sucursalp);
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}