using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using FacturacionElectronica.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace FacturacionElectronica.Web.Controllers
{
    public class FacturacionController : Controller
    {
        IbaseBL _base;
        Usuario _usuarioLogeado;
        // GET: Facturacion
        public ActionResult Index()
        {
            _base = new FacturaBL();
            _usuarioLogeado = (Usuario)Session["User"];
            List<Factura> _result;
            if (_usuarioLogeado.Rol.FirstOrDefault().nombre == "SUPERADMIN")
            {
                var _facturas = _base.ObtenerTodos();
                _result = (_facturas).Cast<Factura>().ToList();
                return View(_result);
            }
            else
            {
                var _facturas = FacturaBL.ObtenerPorVendedor(_usuarioLogeado.id);
                _result = (_facturas).Cast<Factura>().ToList();
                return View(_result);
            }

        }

        // GET: Facturacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Facturacion/Create
        public ActionResult Create()
        {
            ClienteBL clienteBD = new ClienteBL();

            var _clientes = clienteBD.ObtenerTodos();
            List<Cliente> result = (_clientes).Cast<Cliente>().ToList();


            return View(result);
        }

        public ActionResult CrearFactura(int? id, int? idFactura, int? idProducto)
        {
            int.TryParse(idFactura.ToString(), out int _idFactura);
            int.TryParse(id.ToString(), out int _idCliente);
            Factura _registro;
            if (id > 0)
            {
                var _usuarioLogeado = (Usuario)Session["User"];
                _registro = new Factura();
                _registro.idVendedor = _usuarioLogeado.id;
                _registro.idCliente = _idCliente;
                _registro.facturado = false;
                _registro.valorTotal = 0;
                _base = new FacturaBL();
                _registro = (Factura)_base.Guardar(_registro);
                return RedirectToAction("CrearFactura", "Facturacion", new { @id = 0, @idFactura = _registro.id });

            }
            else if (_idFactura > 0)
            {
                //EXISTE FACTURA
                _base = new FacturaBL();
                _registro = (Factura)_base.ObtenerId(_idFactura);
                _base = new ProductoBL();
                var _productos = (_base.ObtenerTodos()).Cast<Producto>().ToList();
                _registro.Productos = _productos;

                //SE VERIFICA SI SELECCIONARON PRODUCTOS NUEVOS
                int.TryParse(idProducto.ToString(), out int _idProducto);

                if (_idProducto > 0)
                {
                    //SE CREA EL DETALLE EN LA FACTURA
                    _base = new DetalleFacturaBL();

                    var _detalle = new DetalleFactura();
                    _detalle.cantidad = 1;
                    _detalle.idFactura = _idFactura;
                    _detalle.idProducto = _idProducto;
                    _detalle.precio = _registro.Productos.Where(x => x.id == _idProducto).FirstOrDefault().precio;
                    _base.Guardar(_detalle);
                    double _valorTotal = 0;
                    //SE ACTUALIZA LA FACTURA EL VALOR A PAGAR (MEJORABLE CON LAS SUMAS DE LOS DETALLES)
                    Parallel.ForEach(_registro.DetalleFactura.Cast<DetalleFactura>(),
                    currentElement =>
                    {
                        _valorTotal += currentElement.precio;
                    });
                    _registro.valorTotal = _valorTotal;
                    _base = new FacturaBL();
                    _registro = (Factura)_base.Editar(_registro);
                    _registro.Productos = _productos;
                }


                return View(_registro);
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public ActionResult CrearFactura(FormCollection formCollection)
        {
            int _idFactura = int.Parse(formCollection.Get("idFactura"));

            FacturaBL.Facturar(_idFactura);

            return RedirectToAction("Index");

        }


        public ActionResult Facturar(int? id)
        {
            int.TryParse(id.ToString(), out int _id);
            if (id > 0)
            {
                //OBTENIENDO EL OBJETO FACTURA
                _base = new FacturaBL();
                var _factura = (Factura)_base.ObtenerId(_id);
                FacturaXML _xml = new FacturaXML();
                _xml.Cliente = _factura.Cliente.nombres;
                _xml.Vendedor = _factura.Usuario.userName;
                DateTime _fechaCompra = DateTime.Parse(_factura.fechaCompra.ToString());
                _xml.FechaCompra = _fechaCompra.ToString("dd/MM/yyyy hh:mm:ss");
                _xml.CodigoFactura = Encriptacion.Encrypt(_factura.id.ToString().PadRight(20));
                _xml.Detalle = new FacturaXML.DetalleFactura[_factura.DetalleFactura.Count];

                int _i = 0;
                foreach (var item in _factura.DetalleFactura)
                {
                    _xml.Detalle[_i]=new FacturaXML.DetalleFactura { NombreProducto = item.Producto.nombre, Precio = item.Producto.precio.ToString() };
                    _i++;
                }



                //string DES = Encriptacion.Decrypt(ENC);
                string _xmlFile = CreateXML(_xml);
                CargarArchivoXML(_xmlFile);
                return RedirectToAction("Index", "Facturacion");
            }
            else
            {
                return RedirectToAction("Index", "Facturacion");
            }
        }

        public string CreateXML(FacturaXML YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        protected void CargarArchivoXML(string TextoXml)
        {
            StringBuilder sb = new StringBuilder();
            string output = "Output";
            sb.Append(output);
            sb.Append("\r\n");

            string text = TextoXml;

            Response.Clear();
            Response.ClearHeaders();
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "application/xml";
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"output.xml\"");

            Response.Write(text);
            Response.End();
        }

        // GET: Facturacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facturacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Facturacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Facturacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
