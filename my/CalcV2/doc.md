# HTTP калькулятор.

Invoke-RestMethod -Uri "http://localhost:5224/result"

Invoke-WebRequest -Method Post -Uri "http://localhost:5224/getArg" `
-ContentType "application/json" -Body '{"X":5, "Y":3}'