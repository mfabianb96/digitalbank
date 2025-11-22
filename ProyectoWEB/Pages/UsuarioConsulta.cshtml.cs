namespace WebClient.Pages;

public class UsuarioConsultaModel : PageModel
{
    private readonly HttpClient _client;
    private readonly IAlertService _alert;

    public List<Usuario> Usuarios { get; set; } = new();

    public UsuarioConsultaModel(IHttpClientFactory httpClientFactory, IAlertService alert)
    {
        _client = httpClientFactory.CreateClient("api");
        _alert = alert;
    }

    public async Task OnGet()
    {
        Usuarios = await _client.GetFromJsonAsync<List<Usuario>>("Usuario");
    }

    public async Task<IActionResult> OnPostEliminar(int id)
    {
        await _client.DeleteAsync($"Usuario/{id}");

        _alert.Success("Eliminado correctamente");
        return RedirectToPage();
    }
}
