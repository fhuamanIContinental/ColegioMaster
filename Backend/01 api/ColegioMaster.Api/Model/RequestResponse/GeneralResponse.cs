namespace Model.RequestResponse;

public class GeneralResponse<T>
{
    public bool Success { get; set; }
    public string TitleMessage { get; set; } = string.Empty;
    public string TextMessage { get; set; } = string.Empty;
    public bool ShowAlert { get; set; }
    public T? Content { get; set; }
}

public class GeneralResponse
{
    public bool Success { get; set; }
    public string TitleMessage { get; set; } = string.Empty;
    public string TextMessage { get; set; } = string.Empty;
    public bool ShowAlert { get; set; }
}
