using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebClient.Services;

public class AlertService : IAlertService
{
    private readonly ITempDataDictionaryFactory _tempDataFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AlertService(
    ITempDataDictionaryFactory tempDataFactory,
    IHttpContextAccessor httpContextAccessor)
    {
        _tempDataFactory = tempDataFactory;
        _httpContextAccessor = httpContextAccessor;
    }


    private void SetAlert(string type, string title, string message)
    {
        var tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext!);

        tempData["Alert.Type"] = type;
        tempData["Alert.Title"] = title;
        tempData["Alert.Message"] = message;
    }


    public void Success(string message, string title = "Éxito")
                => SetAlert("success", title, message);

    public void Error(string message, string title = "Error")
        => SetAlert("error", title, message);

    public void Warning(string message, string title = "Advertencia")
        => SetAlert("warning", title, message);

    public void Info(string message, string title = "Información")
        => SetAlert("info", title, message);
}
