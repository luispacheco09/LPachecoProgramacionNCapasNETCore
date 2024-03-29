﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaProducto = (from productoDL in context.Productos
                                         join proveedor in context.Proveedors on productoDL.IdProveedor equals proveedor.IdProveedor
                                         join marca in context.Marcas on productoDL.IdMarca equals marca.IdMarca
                                         join departamento in context.Departamentos on productoDL.IdDepartamento equals departamento.IdDepartamento
                                         join area in context.Areas on departamento.IdArea equals area.IdArea
                                         select new
                                         {
                                             IdProducto = productoDL.IdProducto,
                                             Nombre = productoDL.Nombre,
                                             PrecioUnitario = productoDL.PrecioUnitario,
                                             CodigoBarras = productoDL.CodigoBarras,
                                             Imagen = productoDL.Imagen,
                                             Modelo = productoDL.Modelo,
                                             IdMarca = productoDL.IdMarca,
                                             MarcaNombre = marca.Nombre,
                                             IdProveedor = productoDL.IdProveedor,
                                             ProveedorNombre = proveedor.Nombre,
                                             IdArea = departamento.IdArea,
                                             IdDepartamento = productoDL.IdDepartamento,
                                             DepartamentoNombre = departamento.Nombre,
                                             Descripcion = productoDL.Descripcion
                                         });
                    if (listaProducto != null && listaProducto.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaProducto)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario;
                            producto.CodigoBarras = obj.CodigoBarras;
                            producto.Imagen = obj.Imagen;
                            producto.Modelo = obj.Modelo;
                            producto.Marca = new ML.Marca();
                            producto.Marca.IdMarca = obj.IdMarca.Value;
                            producto.Marca.Nombre = obj.MarcaNombre;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Nombre = obj.ProveedorNombre;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.Nombre = obj.DepartamentoNombre;
                            //producto.Proveedor.Nombre = obj.ProveedorNombre;

                            //producto.Departamento.Area = new ML.Area();
                            //producto.Departamento.Area.IdArea = obj.IdArea;

                            producto.Descripcion = obj.Descripcion;

                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos de Productos"; ;
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from productoDL in context.Productos
                                 join proveedor in context.Proveedors on productoDL.IdProveedor equals proveedor.IdProveedor
                                 join departamento in context.Departamentos on productoDL.IdDepartamento equals departamento.IdDepartamento
                                 join area in context.Areas on departamento.IdArea equals area.IdArea
                                 where productoDL.IdProducto == IdUsuario
                                 select new
                                 {
                                     IdProducto = productoDL.IdProducto,
                                     Nombre = productoDL.Nombre,
                                     Descripcion = productoDL.Descripcion,
                                     FechaIngreso = productoDL.FechaIngreso,
                                     PrecioUnitario = productoDL.PrecioUnitario,
                                     CodigoBarras = productoDL.CodigoBarras,
                                     Imagen = productoDL.Imagen,
                                     Modelo = productoDL.Modelo,
                                     IdMarca = productoDL.IdMarca,
                                     IdProveedor = productoDL.IdProveedor,
                                     ProveedorNombre = proveedor.Nombre,
                                     IdDepartamento = productoDL.IdDepartamento,
                                     IdArea = departamento.IdArea,
                                     IdUsuarioModificacion = productoDL.IdUsuarioModificacion,

                                 }).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Descripcion = query.Descripcion;
                        producto.FechaIngreso = (query.FechaIngreso != null) ? query.FechaIngreso.Value.ToString("dd-MM-yyyy") : "0"; ;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.CodigoBarras = query.CodigoBarras;
                        producto.Imagen = query.Imagen;
                        producto.Modelo = query.Modelo;
                        producto.Marca = new ML.Marca();
                        producto.Marca.IdMarca = query.IdMarca.Value;
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                        producto.Proveedor.Nombre = query.ProveedorNombre;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = query.IdArea.Value;
                        producto.Usuario = new ML.Usuario();
                        producto.Usuario.IdUsuario = query.IdUsuarioModificacion.Value;


                        result.Object = producto;
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
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {

                    DL.Producto DLProducto = new DL.Producto();

                    DLProducto.Nombre = producto.Nombre;
                    DLProducto.Descripcion = producto.Descripcion;
                    DLProducto.PrecioUnitario = producto.PrecioUnitario;
                    DLProducto.IdDepartamento = producto.Departamento.IdDepartamento;
                    DLProducto.FechaIngreso = DateTime.Now;
                    DLProducto.CodigoBarras = producto.CodigoBarras;
                    DLProducto.Imagen = producto.Imagen;
                    DLProducto.Modelo = producto.Modelo;
                    DLProducto.IdMarca = producto.Marca.IdMarca;
                    DLProducto.IdProveedor = producto.Proveedor.IdProveedor;
                    DLProducto.IdUsuarioModificacion = 1;

                    context.Productos.Add(DLProducto);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        ML.Result resultSucursal = BL.Sucursal.GetAll();

                        if (resultSucursal.Correct)
                        {
                            foreach (var item in resultSucursal.Objects.ToList())
                            {
                                if(item is ML.Sucursal sucursal)
                                {
                                    DL.SucursalProducto sucursalProducto = new DL.SucursalProducto();
                                    sucursalProducto.IdProducto = DLProducto.IdProducto;
                                    sucursalProducto.IdSucursal = sucursal.IdSucursal;
                                    sucursalProducto.Stock = 0;
                                    context.SucursalProductos.Add(sucursalProducto);
                                }
                            }
                            int RowsAffectedSucursalP = context.SaveChanges();

                            if (RowsAffectedSucursalP > 0)
                            {
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se pudo registrar el producto en la sucursal";
                            }
                        }
                        else
                        {
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar el producto";
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
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var query = (from productoDL in context.Productos
                                 where productoDL.IdProducto == producto.IdProducto
                                 select productoDL).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdProducto = producto.IdProducto;
                        query.Nombre = producto.Nombre;
                        query.Descripcion = producto.Descripcion;
                        query.PrecioUnitario = producto.PrecioUnitario;
                        query.IdDepartamento = producto.Departamento.IdDepartamento;
                        query.CodigoBarras = producto.CodigoBarras;
                        query.Imagen = producto.Imagen;
                        query.Modelo = producto.Modelo;
                        query.IdMarca = producto.Marca.IdMarca;
                        query.IdProveedor = producto.Proveedor.IdProveedor;
                        query.IdUsuarioModificacion = 1;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el producto";
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
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    //var query = (from productoDL in context.Productos
                    //             where productoDL.IdProducto == IdProducto
                    //             select productoDL).First();
                     var query = context.Database.ExecuteSql($"ProductoDeleteCascade @IdProducto={IdProducto}");
                    

                    if (query >0)
                    {
                        //context.Productos.Remove(query);
                        //context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el producto";
                    }
                }

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException DbUpdateEx)
            {
                if (DbUpdateEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)//Error especifico para violacion de llave foranea
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede eliminar el producto ya que esta asociado a una o más sucursales.";
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudieron guardar los cambios el producto";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //public static ML.Result DeleteSP(int IdProduco)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
        //        {
        //            string query = "UsuarioDelete";

        //            using (SqlCommand cmd = new SqlCommand(query, context))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();

        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "No se pudo realizar la actulización";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;

        //    }

        //    return result;
        //}

    }
}
