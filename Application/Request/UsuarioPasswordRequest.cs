﻿namespace Application.Request
{
    public class UsuarioPasswordRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public string Password { get; set; }
    }
}
