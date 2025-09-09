# HTTP калькулятор.

## HTTP запросы
Invoke-RestMethod -Uri "http://localhost:5224/result"

Invoke-WebRequest -Method Post -Uri "http://localhost:5224/getArg" `
-ContentType "application/json" -Body '{"X":5, "Y":3}'

curl -X POST http://localhost:5224/setArg -H "Content-Type: application/json" -d '{"X":5, "Y":3}'