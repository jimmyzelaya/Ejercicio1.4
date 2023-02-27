using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicio4.Models
{
    public class sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
    }
}
