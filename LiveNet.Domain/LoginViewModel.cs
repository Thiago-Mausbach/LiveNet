namespace LiveNet.Domain;

public class LoginViewModel
{
    public required string Email { get; set; }
    public required string Senha { get; set; }
}

public class AuthResult
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}