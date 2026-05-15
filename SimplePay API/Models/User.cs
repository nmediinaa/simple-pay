using System.ComponentModel.DataAnnotations;

namespace SimplePay_API.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome do titular da conta nao deve ser nulo")]
    public string? Name { get; set; }

    public DateTime SingUpDate { get; set; }

    public Account? Account { get; set; }
}
