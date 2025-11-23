namespace ProyectoWEB.Pages;

public class UsuarioModel : PageModel
{
    private readonly HttpClient _client;
    private readonly IAlertService _alert;

    public UsuarioModel(IHttpClientFactory httpClientFactory, IAlertService alert)
    {
        _client = httpClientFactory.CreateClient("api");
        _alert = alert;
    }


    [BindProperty]
    public Usuario Usuario { get; set; } = new Usuario();

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        if (Usuario.Id == 0)
        {
            await _client.PostAsJsonAsync("Usuario", Usuario);
            _alert.Success("Usuario creado correctamente");
            return Page();
        }
        else
        {
            await _client.PutAsJsonAsync($"Usuario", Usuario);
            _alert.Success("Usuario modificado correctamente");
            return RedirectToPage("/UsuarioConsulta");
        }

        
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        ModelState.Clear();

        if (id.HasValue)
        {
            var usuario = await _client.GetFromJsonAsync<Usuario>($"Usuario?id={id}");

            if (usuario == null)
            {
                _alert.Error("Usuario no encontrado");
                return RedirectToPage("/UsuarioConsulta");
            }

            Usuario = usuario;
        }

        return Page();
    }
}
