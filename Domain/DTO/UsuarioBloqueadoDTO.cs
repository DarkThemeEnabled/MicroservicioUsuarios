namespace Domain.DTO
{
    public class UsuarioBloqueadoDTO
    {
        public bool IsLocked { get; set; }
        public DateTime LockedUntil { get; set; }
    }
}
