using System; // contiene tipos fundamentales y clases base que definen tipos de datos comunes, eventos y manipulación de archivos y E/S.
using System.Collections.Generic;// contiene clases y interfaces genéricas que complementan las colecciones no genéricas del espacio de nombres System.Collections.
using System.ComponentModel.DataAnnotations.Schema; // contiene clases que se utilizan para trabajar con anotaciones de atributos de esquema de base de datos.
using System.ComponentModel.DataAnnotations; // contiene clases y atributos que se utilizan para definir metadatos de modelos y realizar validaciones de datos.
using System.Linq; // proporciona clases y métodos que admiten consultas LINQ.
using System.Text; // contiene clases que representan codificaciones de caracteres Unicode y clases que ayudan en la manipulación de texto.
using System.Threading.Tasks; // contiene tipos que admiten la tarea basada en el modelo de programación asíncrona.

namespace FoodOffer.Model.Models
{
    public class Address
    {
        public int Ref_Id { get; set; }
        public char Ref_Type { get; set; }
        public short Item { get; set; }
        public string Description { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string? Obs { get; set; }

    }
}
