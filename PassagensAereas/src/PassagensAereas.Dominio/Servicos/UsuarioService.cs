using System;
using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class UsuarioService
    {
        public static List<string> Validar(Usuario usuario)
        {
            List<string> inconsistencias = new List<string>();

            if (string.IsNullOrEmpty(usuario.CPF?.Trim()))
                inconsistencias.Add($"O campo {nameof(usuario.CPF)} não pode ser nulo.");
                
            if (usuario.DataNascimento.AddYears(18) > DateTime.Now)
                inconsistencias.Add($"O usuário deve ser maior de 18 anos.");

            if (string.IsNullOrEmpty(usuario.PrimeiroNome?.Trim()))
                inconsistencias.Add($"O campo {nameof(usuario.PrimeiroNome)} não pode ser nulo.");

            if (string.IsNullOrEmpty(usuario.UltimoNome?.Trim()))
                inconsistencias.Add($"O campo {nameof(usuario.UltimoNome)} não pode ser nulo.");

            if (string.IsNullOrEmpty(usuario.Login?.Trim()))
                inconsistencias.Add($"O campo {nameof(usuario.Login)} não pode ser nulo.");

            if (string.IsNullOrEmpty(usuario.Senha?.Trim()))
                inconsistencias.Add($"O campo {nameof(usuario.Senha)} não pode ser nulo.");
            
            return inconsistencias;
        }
        
    }
}