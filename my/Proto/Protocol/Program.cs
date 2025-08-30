var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWelcomePage("/");

// Не обязательный, он уже добавлен – просмотр исключений в окружении разработки.
// Генерирует HTML-страницу, которую возвращает пользователю с кодом состояния 500.
app.UseDeveloperExceptionPage();

/* Есть и другие компоненты, которые вставляются автоматически:
   HostFilteringMiddleware – компонент связан с безопасностью; 
ForwardedHeadersMiddleware – управляет обработкой пересылаемых заголовков;
         RoutingMiddleware – если добавляются конечные точки, метод UseRouting() 
                             выполняется до добавления в приложение какого-либо собственного компонента;
  AuthenticationMiddleware – компонент аутентифицирует пользователя для запроса;
   AuthorizationMiddleware – определяет, разрешено ли пользователю выполнить конечную точку;
        EndpointMiddleware – соединяется с RoutingMiddleware для выполнения конечной точки. 
                             Добавляется в конец конвейера после любого другого компонента.
*/

// Настраивает другой конвейер, когда он не работает в окружении разработки
if (!app.Environment.IsDevelopment()) {
    // не будет передавать конфиденциальные данные при запуске в промышленном окружении
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapGet("/", () => "Hello World!");

// Эта конечная точка ошибки будет выполнена при обработке исключения
app.MapGet("/error", () => "Sorry, an error occurred.");

app.Run();