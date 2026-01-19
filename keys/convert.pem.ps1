$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2("ImageMagick.pem")

[System.IO.File]::WriteAllBytes("ImageMagick.cer", $cert.Export([System.Security.Cryptography.X509Certificates.X509ContentType]::Cert))
