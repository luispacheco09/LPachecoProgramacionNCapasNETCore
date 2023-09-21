using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result GetAll(string UserId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaHistorial = (from venta in context.Venta
                                          where venta.IdUser == UserId
                                          select new
                                          {
                                              IdVenta = venta.IdVenta,
                                              IdUser = venta.IdUser,
                                              Total = venta.Total,
                                              IdMetodoPago = venta.IdMetodoPago,
                                              Fecha = venta.Fecha
                                          }).ToList();
                    if (listaHistorial != null && listaHistorial.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaHistorial)
                        {
                            ML.Venta venta = new ML.Venta();
                            venta.IdVenta = obj.IdVenta;
                            venta.Total = obj.Total;
                            venta.IdMetodoPago = obj.IdMetodoPago;
                            //venta.Fecha = obj.Fecha;
                            venta.Fecha = (obj.Fecha != null) ? obj.Fecha.Value.ToString("dd-MM-yyyy") : "0";

                            result.Objects.Add(venta);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos del historial";
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public static ML.Result GetProductoHistorial(int IdVenta, string userId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaHistorial = (from ventaProductDL in context.VentaProductos
                                          join sucursalP in context.SucursalProductos on ventaProductDL.IdSucursalProducto equals sucursalP.IdSucursalProducto
                                          join sucursal in context.Sucursals on  sucursalP.IdSucursal equals sucursal.IdSucursal
                                          join producto in context.Productos on  sucursalP.IdProducto equals producto.IdProducto
                                          join venta in context.Venta on ventaProductDL.IdVenta equals venta.IdVenta
                                          where ventaProductDL.IdVenta == IdVenta && venta.IdUser == userId
                                          select new
                                          {
                                              IdVentaP = ventaProductDL.IdVentaProducto,
                                              IdVenta = ventaProductDL.IdVenta,
                                              Fecha = venta.Fecha,
                                              IdSucursalProducto = ventaProductDL.IdSucursalProducto,
                                              Cantidad = ventaProductDL.Cantidad,
                                              SucursalNombre = sucursal.Nombre,
                                              ProductoNombre = producto.Nombre,
                                              ProductoImg = producto.Imagen,
                                              ProductoPrecio = producto.PrecioUnitario,
                                              ProductoDescripcion = producto.Descripcion,
                                              Subtotal = ventaProductDL.Cantidad * producto.PrecioUnitario
                                          }).ToList();
                    if (listaHistorial != null && listaHistorial.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaHistorial)
                        {
                            ML.VentaProducto ventaProducto = new ML.VentaProducto();
                            ventaProducto.IdVentaProducto = obj.IdVentaP;
                            ventaProducto.Venta = new ML.Venta();
                            ventaProducto.Venta.IdVenta = obj.IdVenta;
                            ventaProducto.SucursalProducto = new ML.SucursalProducto();
                            ventaProducto.SucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;
                            ventaProducto.Cantidad = obj.Cantidad;
                            ventaProducto.SucursalProducto.Sucursal = new ML.Sucursal();
                            ventaProducto.SucursalProducto.Sucursal.Nombre = obj.SucursalNombre;
                            ventaProducto.SucursalProducto.Producto = new ML.Producto();
                            ventaProducto.SucursalProducto.Producto.Nombre = obj.ProductoNombre;
                            ventaProducto.SucursalProducto.Producto.PrecioUnitario = obj.ProductoPrecio;
                            ventaProducto.SucursalProducto.Producto.Imagen = obj.ProductoImg;
                            ventaProducto.SucursalProducto.Producto.Descripcion = obj.ProductoDescripcion;

                            ventaProducto.SubTotal = obj.Subtotal;


                            result.Objects.Add(ventaProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos del historial";
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
