namespace FloripaSurfClubCore.Requests
{
    public abstract class Request
    {
        public Guid UsuarioSistemaId { get; set; } = Guid.Empty;

    }
}
