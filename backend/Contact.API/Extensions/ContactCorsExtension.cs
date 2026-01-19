namespace Contact.API.Extensions
{
    public static class ContactCorsExtension
    {
        public const string AllowAllPolicyName = "AllowAll";

        public static IServiceCollection AddContactCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(AllowAllPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
