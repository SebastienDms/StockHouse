using System.Text.RegularExpressions;

namespace StockHouse.Models
{
  public interface IModelBase
  {
    int Id { get; set; }
  }

  public class MyRegex
  {
    private Regex _mailRegex;
    private Regex _pwdRegex;

    public MyRegex()
    {
      MailRegex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
      PwdRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    }

    public Regex MailRegex
    {
      get => _mailRegex;
      set => _mailRegex = value;
    }

    public Regex PwdRegex
    {
      get => _pwdRegex;
      set => _pwdRegex = value;
    }
  }
}