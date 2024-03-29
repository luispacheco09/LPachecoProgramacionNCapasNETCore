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
                                             Imagen = producto.Imagen,
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
                            sucursalp.Producto.Imagen = obj.Imagen;
                           

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

        //public static ML.Result GetProductbySucDepto(int IdSucursal, int IdDepartamento)
        public static ML.Result GetProductbySucDepto( int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaSucursal = (from sucursalPDL in context.SucursalProductos
                                         join sucursal in context.Sucursals on sucursalPDL.IdSucursal equals sucursal.IdSucursal
                                         join producto in context.Productos on sucursalPDL.IdProducto equals producto.IdProducto
                                         where sucursalPDL.IdSucursal == 3 && producto.IdDepartamento == IdDepartamento 
                                         select new
                                         {
                                             IdSucursalProducto = sucursalPDL.IdSucursalProducto,
                                             IdSucursal = sucursalPDL.IdSucursal,
                                             IdProducto = sucursalPDL.IdProducto,
                                             Stock = sucursalPDL.Stock,
                                             ProductoNombre = producto.Nombre,
                                             Descripcion = producto.Descripcion,
                                             idDepartamento = producto.IdDepartamento,
                                             Imagen = producto.Imagen,
                                             Modelo = producto.Modelo,
                                             PrecioUnitario = producto.PrecioUnitario,
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
                            sucursalp.Producto.Descripcion = obj.Descripcion;
                            sucursalp.Producto.Imagen = obj.Imagen;
                            sucursalp.Producto.Modelo = obj.Modelo;
                            sucursalp.Producto.PrecioUnitario = obj.PrecioUnitario;

                            result.Objects.Add(sucursalp);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos de los poductos"; ;
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
        public static ML.Result GetProductbySucDeptoSinStock(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaSucursal = (from sucursalPDL in context.SucursalProductos
                                         join sucursal in context.Sucursals on sucursalPDL.IdSucursal equals sucursal.IdSucursal
                                         join producto in context.Productos on sucursalPDL.IdProducto equals producto.IdProducto
                                         where sucursalPDL.IdSucursal == 3 && producto.IdDepartamento == IdDepartamento && sucursalPDL.Stock > 0
                                         select new
                                         {
                                             IdSucursalProducto = sucursalPDL.IdSucursalProducto,
                                             IdSucursal = sucursalPDL.IdSucursal,
                                             IdProducto = sucursalPDL.IdProducto,
                                             Stock = sucursalPDL.Stock,
                                             ProductoNombre = producto.Nombre,
                                             Descripcion = producto.Descripcion,
                                             idDepartamento = producto.IdDepartamento,
                                             Imagen = producto.Imagen,
                                             Modelo = producto.Modelo,
                                             PrecioUnitario = producto.PrecioUnitario,
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
                            sucursalp.Producto.Descripcion = obj.Descripcion;
                            sucursalp.Producto.Imagen = obj.Imagen;
                            sucursalp.Producto.Modelo = obj.Modelo;
                            sucursalp.Producto.PrecioUnitario = obj.PrecioUnitario;

                            result.Objects.Add(sucursalp);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos de los poductos"; ;
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

        public static ML.Result GetProductbySucProduct( int? IdSucursalProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaSucursal = (from sucursalPDL in context.SucursalProductos
                                         join sucursal in context.Sucursals on sucursalPDL.IdSucursal equals sucursal.IdSucursal
                                         join producto in context.Productos on sucursalPDL.IdProducto equals producto.IdProducto
                                         where sucursalPDL.IdSucursalProducto == IdSucursalProducto 
                                         select new
                                         {
                                             IdSucursalProducto = sucursalPDL.IdSucursalProducto,
                                             IdSucursal = sucursalPDL.IdSucursal,
                                             IdProducto = sucursalPDL.IdProducto,
                                             Stock = sucursalPDL.Stock,
                                             ProductoNombre = producto.Nombre,
                                             Descripcion = producto.Descripcion,
                                             idDepartamento = producto.IdDepartamento,
                                             Imagen = producto.Imagen,
                                             Modelo = producto.Modelo,
                                             PrecioUnitario = producto.PrecioUnitario,
                                             SucursalNombre = sucursal.Nombre
                                         }).FirstOrDefault();

                    if (listaSucursal != null)
                    {
                        result.Objects = new List<object>();
                       
                        ML.SucursalProducto sucursalp = new ML.SucursalProducto();
                        sucursalp.IdSucursalProducto = listaSucursal.IdSucursalProducto;
                        sucursalp.Stock = listaSucursal.Stock;

                        sucursalp.Sucursal = new ML.Sucursal();
                        sucursalp.Sucursal.IdSucursal = listaSucursal.IdSucursal.Value;
                        sucursalp.Sucursal.Nombre = listaSucursal.SucursalNombre;
                        sucursalp.Producto = new ML.Producto();
                        sucursalp.Producto.IdProducto = listaSucursal.IdProducto.Value;
                        sucursalp.Producto.Nombre = listaSucursal.ProductoNombre;
                        sucursalp.Producto.Descripcion = listaSucursal.Descripcion;
                        sucursalp.Producto.Imagen = listaSucursal.Imagen;
                        sucursalp.Producto.Modelo = listaSucursal.Modelo;
                        sucursalp.Producto.PrecioUnitario = listaSucursal.PrecioUnitario;

                        //ML.VentaProducto sucursalp = new ML.VentaProducto();
                        //sucursalp.SucursalProducto = new ML.SucursalProducto();
                        //sucursalp.SucursalProducto.IdSucursalProducto = listaSucursal.IdSucursalProducto;
                        //sucursalp.SucursalProducto.Stock = listaSucursal.Stock;

                        //sucursalp.SucursalProducto.Sucursal = new ML.Sucursal();
                        //sucursalp.SucursalProducto.Sucursal.IdSucursal = listaSucursal.IdSucursal.Value;
                        //sucursalp.SucursalProducto.Sucursal.Nombre = listaSucursal.SucursalNombre;
                        //sucursalp.SucursalProducto.Producto = new ML.Producto();
                        //sucursalp.SucursalProducto.Producto.IdProducto = listaSucursal.IdProducto.Value;
                        //sucursalp.SucursalProducto.Producto.Nombre = listaSucursal.ProductoNombre;
                        //sucursalp.SucursalProducto.Producto.Descripcion = listaSucursal.Descripcion;
                        //sucursalp.SucursalProducto.Producto.Imagen = listaSucursal.Imagen;
                        //sucursalp.SucursalProducto.Producto.Modelo = listaSucursal.Modelo;
                        //sucursalp.SucursalProducto.Producto.PrecioUnitario = listaSucursal.PrecioUnitario;

                        result.Object = sucursalp;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos de los poductos"; ;
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

        public static ML.Result UpdateStock(int? IdStock, int? Stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from spDL in context.SucursalProductos
                                 where spDL.IdSucursalProducto == IdStock
                                 select spDL).FirstOrDefault();
                    if(query != null)
                    {
                        query.Stock = Stock;
                        context.SaveChanges();
                        result.Object = query.Stock;
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede actualiza el stock";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdSucursalP)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from sproductoDL in context.SucursalProductos
                                 where sproductoDL.IdSucursalProducto == IdSucursalP
                                 select sproductoDL).First();
                    if (query != null)
                    {
                        context.SucursalProductos.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el producto de la sucursal";
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
