module JwtHelper

open System
open Microsoft.IdentityModel.Tokens
open System.Text
open ParametrosJwt
open System.IdentityModel.Tokens.Jwt
open System.Security.Claims


    
    let generateJwtToken (username: string) =
        let securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.secretKey))
        let credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        
        let claims = 
            [| 
                Claim(JwtRegisteredClaimNames.Sub, username)
                Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            |]
        
        let token = JwtSecurityToken(
            issuer = JwtConfig.issuer,
            audience = JwtConfig.audience,
            claims = claims,
            expires = DateTime.UtcNow.AddMinutes(float JwtConfig.expirationMinutes),
            signingCredentials = credentials
        )    
        JwtSecurityTokenHandler().WriteToken(token)