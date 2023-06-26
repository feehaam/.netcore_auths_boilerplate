## appsettings.json
```
{
  "AppSettings": {
    "Token": "THISISMYSUPERSECRETTOKENTHATNONECANEVERIMAGINE"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```


## Program.cs imports
```
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
```

## Program.cs adding authentication service
```
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
    );
```

## Must include in Program.cs
```
app.UseAuthentication();
app.UseAuthorization();
```

# Token generator
```
// Includes
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// Claims
[HttpGet("/login/admin")]
public ActionResult<object> Authenticate2()
{
    List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "This is just a name representing extra information"),
        new Claim(ClaimTypes.Role, "admin")
    };
    string token = CreateToken(claims);
    return Ok(token);
}

// JWT generator
private string CreateToken(List<Claim> clm)
{
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    var token = new JwtSecurityToken(
        claims: clm,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: cred
    );

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return jwt;
}
```
