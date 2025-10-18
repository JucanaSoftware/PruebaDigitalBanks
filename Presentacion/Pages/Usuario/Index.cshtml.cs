using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentacion.Models;
using System.Net.Http.Json;

namespace Presentacion.Pages.Usuario
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<UsuarioDto> Usuarios { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("API");
            Usuarios = await client.GetFromJsonAsync<List<UsuarioDto>>("api/usuario");
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.DeleteAsync($"api/usuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Usuario eliminado correctamente";
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar usuario";
            }

            return RedirectToPage(); // recarga el Index mostrando el mensaje
        }

    }
}
