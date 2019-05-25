﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABM.Datos.SQL;

namespace ABM.Base
{
    public class BaseController : Controller
    {
        protected ABMEntities BddABM { get; set; }
        protected BaseController()
        {
            BddABM = new ABMEntities();
        }

        protected TBL_USUARIO Usuario { get { return (TBL_USUARIO)Session["Usuario"] ?? new TBL_USUARIO(); } set { Session["Usuario"] = value; } }
        protected TBL_CARRO_COMPRA Carro { get { return (TBL_CARRO_COMPRA)Session["CarroCompra"] ?? new TBL_CARRO_COMPRA(); } set { Session["CarroCompra"] = value; } }

        protected JsonResult JsonExito(string mensaje = "", object data = null)
        {
            return Json(new { exito = true, mensaje = mensaje, data = data });
        }

        protected JsonResult JsonError(string mensaje = "")
        {
            return Json(new { exito = false, mensaje = mensaje });
        }

        protected JsonResult JsonSolo(object data)
        {
            return Json(data);
        }
        public string URLWEBPAY = "";

        protected List<SelectListItem> TipoProductoSelect(int SelectId = 0)
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione...",
                Selected = SelectId == 0,
            });

            var items = BddABM.TBL_TIPO_PRODUCTO.ToList().Select(o => new SelectListItem
            {
                Value = o.tprod_id.ToString(),
                Text = o.tprod_nombre ?? "",
                Selected = o.tprod_id == SelectId,
            }).ToList();

            lista.AddRange(items);
            return lista;
        }

        public JsonResult AgregarAlCarro(int proId)
        {
            if(Usuario.usu_id == 0)
            {
                Usuario = BddABM.TBL_USUARIO.FirstOrDefault(o => o.usu_rut == "1-9");
            }
            if(Carro.CAR_ID == 0)
            {
                Carro.USU_ID = Usuario.usu_id;
                Carro.cas_creacion = DateTime.Now;

                Carro = BddABM.TBL_CARRO_COMPRA.Add(Carro);
                BddABM.Entry(Carro).State = System.Data.Entity.EntityState.Added;

                BddABM.SaveChanges();
            }

            var producto = BddABM.TBL_PRODUCTO.FirstOrDefault(o => o.pro_id == proId);

            if (producto == null)
            {
                return JsonError("El producto no existe.");
            }

            var valor = string.IsNullOrEmpty(producto.pro_precio_oferta) ? producto.pro_precio : int.Parse(producto.pro_precio_oferta);

            var lisProd = new TBL_LISTA_COMPRA
            {
                lcom_valor = valor,
                usu_id = Usuario.usu_id == 0 ? BddABM.TBL_USUARIO.FirstOrDefault(o => o.usu_rut == "1-9").usu_id : Usuario.usu_id,
            };

            return JsonExito();
        }
    }
}