using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentacion.Models;
using System.Net.Http.Json;

namespace Presentacion.Pages.Usuario
{
    public class CrearModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CrearModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public UsuarioDto Usuario { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public string Mensaje { get; set; }
        public bool EsEdicion => Id.HasValue;

        public async Task OnGetAsync()
        {
            if (EsEdicion)
            {
                var client = _httpClientFactory.CreateClient("API");
                Usuario = await client.GetFromJsonAsync<UsuarioDto>($"api/usuario/{Id.Value}");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("API");

            if (EsEdicion)
            {
                Usuario.Id = Id.Value; // importante para el PUT
                var response = await client.PutAsJsonAsync("api/usuario", Usuario);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Usuario modificado correctamente";
                    return RedirectToPage("Index"); // redirige al index
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    Mensaje = $"Error al modificar usuario: {errorMsg}";
                }
            }
            else
            {
                var response = await client.PostAsJsonAsync("api/usuario", Usuario);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Usuario agregado correctamente";
                    return RedirectToPage("Index"); // redirige al index
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    Mensaje = $"Error al agregar usuario: {errorMsg}";
                }
            }

            return Page(); // si falla, se queda en la misma página
        }

    }
}
