
namespace Datos.Models
{
    public class Usuario
    {
        public int Id { get; set; }                  // Id del usuario
        public string Nombre { get; set; }           // Nombre del usuario
        public DateTime FechaNacimiento { get; set; }// Fecha de nacimiento
        public string Sexo { get; set; }             // Sexo ('M' o 'F')
    }
}
