using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FacturacionElectronica.Web.Models
{
    [XmlRoot(ElementName = "Factura")]
    public class FacturaXML
    {

        public FacturaXML()
        {
        }

        //[XmlElement("CodigoFactura")]
        public string CodigoFactura { get; set; }

        //[XmlElement("FechaCompra")]
        public string FechaCompra { get; set; }

        ///[XmlElement("Vendedor")]
        public string Vendedor { get; set; }

        //[XmlElement("Cliente")]
        public string Cliente { get; set; }

        //[XmlElement("ValorTotal")]
        public string ValorTotal { get; set; }



        //[XmlArrayItem("NombreProducto")]
        //[XmlArrayItem("Precio")]
        // [XmlArray]
        public List<DetalleProducto> Detalle { get; set; }


        public class DetalleProducto
        {
            [XmlElement]
            public string NombreProducto { get; set; }

            [XmlElement]
            public string Precio { get; set; }
        }

    }
}