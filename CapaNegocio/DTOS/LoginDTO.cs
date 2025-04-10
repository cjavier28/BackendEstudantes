namespace ServicioGestionEstudiantes.Negocio.DTOS
{
    public class LoginDTO
    {
        public string? IdEstudiante { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }
}
