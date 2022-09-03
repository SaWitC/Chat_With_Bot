using BotServer.Features.ValidationPipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BotServer.Features
{
    public class DependensyInjection
    {
        public static void AddFeatures(IServiceCollection Services)
        {
           // Services.AddValidatorsFromAssembly(typeof(startup).Assembly);
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            Services.AddMediatR(Assembly.GetExecutingAssembly());

            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
