using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPeliculas.Client.Auth
{
    public class ProveedorAutenticacionPrueba : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
          
            var anonimo = new ClaimsIdentity();

            var userHawerd = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Hawerd"),
                new Claim(ClaimTypes.Role,"admin")
            },
                authenticationType:"admin");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(userHawerd)));
        }
    }
}
 