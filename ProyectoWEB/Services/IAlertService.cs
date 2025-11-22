namespace WebClient.Services;

public interface IAlertService
{
    void Success(string message, string title = "Éxito");
    void Error(string message, string title = "Error");
    void Warning(string message, string title = "Advertencia");
    void Info(string message, string title = "Información");
}
