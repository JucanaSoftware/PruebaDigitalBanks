namespace Presentacion.Models
{
    public class UsuarioDto
    {
        public int Id { get; set; }              // Solo para GET y modificar
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
    }
}
