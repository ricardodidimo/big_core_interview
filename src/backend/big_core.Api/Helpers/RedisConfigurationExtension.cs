using big_core.Common;
using StackExchange.Redis;

namespace big_core.Api.Helpers
{
    public static class RedisConfigurationExtension
    {
        public static IServiceCollection RegisterRedisConnection(this IServiceCollection services)
        {
            var redisConnectionString = Environment.GetEnvironmentVariable(CommonConstants.RedisConnectionVariableKey);
            if (redisConnectionString is null) throw new Exception(ErrorMessages.REQUIRED_ENVIRONMENT_VARIABLE_MISSING);
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

            return services;
        }
    }
}
