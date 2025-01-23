namespace SampleSentry.API.Middleware
{
    public static class ApplicationBuilderExtension
    {
        public static void UseApplication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);

                    var statusCode = context.Response.StatusCode;
                    (string? message, SentryLevel? level) = statusCode switch
                    {
                        401 => ("Unauthorized Access Attempt", SentryLevel.Warning),
                        403 => ("Forbidden Access", SentryLevel.Warning),
                        404 => ("Resource Not Found", SentryLevel.Warning),
                        400 => ("Bad Request", SentryLevel.Warning),
                        405 => ("Method Not Allowed", SentryLevel.Warning),
                        500 => ("Internal Server Error", SentryLevel.Error),
                        _ => ((string?, SentryLevel?))(null, null)
                    };

                    if (message != null && level != null)
                    {
                        SentrySdk.CaptureMessage(message, scope =>
                        {
                            scope.Level = level.Value;
                            scope.SetExtra("Path", context.Request.Path);
                            scope.SetExtra("Method", context.Request.Method);
                            scope.SetTag("status_code", statusCode.ToString());
                        });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}