using BlazorPeliculas.Client.Repositorios;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;





namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    { 
 
          private int currentCount = 0;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;

        private async Task IncrementCount()
    {
       
            var authState = await authenticationStateTask;
            var userAuthenticated = authState.User.Identity != null && authState.User.Identity.IsAuthenticated;

            if (userAuthenticated)
            {
                currentCount++;

            }else
            {
                currentCount-= 1;
            }
            
    }
    
    }
}