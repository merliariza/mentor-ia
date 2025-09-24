using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiPortfolio.Helpers;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<UserMember> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<UserMember> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<DataUserDto> RegisterAsync(RegisterDto registerDto)
    {
        var resultDto = new DataUserDto();
        
        var userExists = _unitOfWork.UserMember.Find(u => u.Email!.ToLower() == registerDto.Email.ToLower()).FirstOrDefault();
        if (userExists != null)
        {
            resultDto.IsAuthenticated = false;
            resultDto.Message = $"El usuario con email {registerDto.Email} ya existe.";
            return resultDto;
        }

        var user = new UserMember
        {
            FullName = registerDto.FullName,
            Email = registerDto.Email,
            PasswordHash = _passwordHasher.HashPassword(null!, registerDto.Password)
        };

        _unitOfWork.UserMember.Add(user);
        await _unitOfWork.SaveAsync();

        resultDto.IsAuthenticated = true;
        resultDto.Message = $"Usuario {registerDto.Email} registrado exitosamente.";
        resultDto.Email = user.Email;
        resultDto.Name = user.FullName;
        resultDto.UserName = user.Email;

        return resultDto;
    }

    public async Task<DataUserDto> LoginAsync(LoginDto model)
    {
        return await GetTokenAsync(model);
    }

    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        var user = _unitOfWork.UserMember.Find(u => u.Email == model.Email).FirstOrDefault();
        var resultDto = new DataUserDto();

        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, model.Password) != PasswordVerificationResult.Success)
        {
            resultDto.IsAuthenticated = false;
            resultDto.Message = "Credenciales incorrectas.";
            return resultDto;
        }

        var jwtToken = CreateJwtToken(user);

        resultDto.IsAuthenticated = true;
        resultDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        resultDto.Email = user.Email;
        resultDto.Name = user.FullName;
        resultDto.UserName = user.Email;


        var activeRefreshToken = user.RefreshTokens?.FirstOrDefault(t => t.IsActive);
        if (activeRefreshToken != null)
        {
            resultDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            resultDto.RefreshToken = activeRefreshToken.Token;
            resultDto.RefreshTokenExpiration = activeRefreshToken.Expires;
        }
        else
        {
            var newRefreshToken = CreateRefreshToken();
            resultDto.RefreshToken = newRefreshToken.Token;
            resultDto.RefreshTokenExpiration = newRefreshToken.Expires;
            user.RefreshTokens!.Add(newRefreshToken);

            _unitOfWork.UserMember.Update(user);
            await _unitOfWork.SaveAsync();
        }

        return resultDto;
    }

    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var user = _unitOfWork.UserMember.Find(u => u.RefreshTokens.Any(r => r.Token == refreshToken)).FirstOrDefault();
        var result = new DataUserDto();

        if (user == null)
        {
            result.IsAuthenticated = false;
            result.Message = "Token inválido.";
            return result;
        }

        var tokenDb = user.RefreshTokens!.FirstOrDefault(t => t.Token == refreshToken);
        if (tokenDb == null || !tokenDb.IsActive)
        {
            result.IsAuthenticated = false;
            result.Message = "El token no está activo.";
            return result;
        }

        tokenDb.Revoked = DateTime.UtcNow;
        var newToken = CreateRefreshToken();
        user.RefreshTokens!.Add(newToken);
        _unitOfWork.UserMember.Update(user);
        await _unitOfWork.SaveAsync();

        var jwtToken = CreateJwtToken(user);

        result.IsAuthenticated = true;
        result.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        result.Email = user.Email;
        result.UserName = user.Email;
        result.Name = user.FullName;
        result.RefreshToken = newToken.Token;
        result.RefreshTokenExpiration = newToken.Expires;

        result.Message = "Token renovado exitosamente.";

        return result;
    }

    private JwtSecurityToken CreateJwtToken(UserMember user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("uid", user.Id.ToString()),

            new Claim("roles", "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: creds
        );
    }

    private RefreshToken CreateRefreshToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(bytes),
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(10)
        };
    }
}