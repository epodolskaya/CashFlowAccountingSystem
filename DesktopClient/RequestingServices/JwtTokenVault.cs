using DesktopClient.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesktopClient.RequestingServices;

public static class JwtTokenVault
{
    private static readonly JwtSecurityTokenHandler TokenHandler;

    static JwtTokenVault()
    {
        TokenHandler = new JwtSecurityTokenHandler();
    }

    public static string? JwtTokenString { get; private set; }

    public static long UserId { get; private set; }

    public static long EmployeeId { get; private set; }

    public static string Email { get; private set; }

    public static Roles Role { get; private set; }

    public static long DepartmentId { get; private set; }

    public static void SetToken(string? tokenString)
    {
        if (tokenString is null)
        {
            JwtTokenString = tokenString;

            return;
        }

        string pureToken = tokenString.Split(' ').Last();

        JwtSecurityToken? token = TokenHandler.ReadJwtToken(pureToken);
        UserId = long.Parse(token.Claims.Single(x => x.Type == CustomClaimName.AccountId).Value);
        EmployeeId = long.Parse(token.Claims.Single(x => x.Type == CustomClaimName.EmployeeId).Value);
        Email = token.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
        DepartmentId = long.Parse(token.Claims.Single(x => x.Type == CustomClaimName.DepartmentId).Value);
        string roleName = token.Claims.Single(x => x.Type == ClaimTypes.Role).Value;

        switch (roleName)
        {
            case RoleNames.DepartmentHead:
                {
                    Role = Roles.DepartmentHead;

                    break;
                }
            case RoleNames.DepartmentEmployee:
                {
                    Role = Roles.FinancialAnalyst;

                    break;
                }
            default:
                {
                    throw new Exception("Неподдерживаемая роль!");
                }
        }

        JwtTokenString = pureToken;
    }
}

public enum Roles
{
    Unauthorized,
    FinancialAnalyst,
    DepartmentHead
}