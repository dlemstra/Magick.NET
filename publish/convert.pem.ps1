$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2("../keys/ImageMagick.pem")

[System.IO.File]::WriteAllBytes("../keys/ImageMagick.cer", $cert.Export([System.Security.Cryptography.X509Certificates.X509ContentType]::Cert))
