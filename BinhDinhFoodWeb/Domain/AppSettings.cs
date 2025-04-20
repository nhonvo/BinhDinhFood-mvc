namespace BinhDinhFood.Domain;

public class AppSettings
{
    public string AllowedHosts { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public MailSettings MailSettings { get; set; }
    public VnPaySettings VnPaySettings { get; set; }
    public MOMOSettings MOMOSettings { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public class MailSettings
{
    public string FromMail { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
}

public class MOMOSettings
{
    public string serectkey { get; set; }
    public string accessKey { get; set; }
    public string partnerCode { get; set; }
    public string endpoint { get; set; }
    public string returnMOMO { get; set; }
}

public class VnPaySettings
{
    public string vnp_Url { get; set; }
    public string querydr { get; set; }
    public string vnp_TmnCode { get; set; }
    public string vnp_HashSecret { get; set; }
    public string vnp_Returnurl { get; set; }
}
