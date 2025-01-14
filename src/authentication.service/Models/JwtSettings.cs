﻿namespace authentication.service.Models;

public class JwtSettings
{
    public JwtSettings() { }
    public string SecretKey { get; set; }
    public int? ExpiryMinutes { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

}