using BCrypt.Net;
using DiceBound.DTOs.Login;
using DiceBound.Entity_s.Identity;
using DiceBound.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var userRepo = _unitOfWork.Repository<User>();

        var existingUsers = await userRepo.GetAllAsync();

        if (existingUsers.Any(u => u.Email == dto.Email))
            throw new Exception("User already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await userRepo.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return new AuthResponseDto
        {
            Token = GenerateJwt(user)
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var userRepo = _unitOfWork.Repository<User>();

        var users = await userRepo.GetAllAsync();

        var user = users.FirstOrDefault(u => u.Email == dto.Email);
        if (user == null) return null;

        var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!valid) return null;

        return new AuthResponseDto
        {
            Token = GenerateJwt(user)
        };
    }

    private string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            // 🔥 ГЛАВНЫЙ ID (ТО ЧТО ТЕБЕ НУЖНО ДЛЯ /my)
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            // 🔥 стандарт JWT subject (очень важно)
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

            // email
            new Claim(ClaimTypes.Email, user.Email),

            // optional
            new Claim("username", user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}