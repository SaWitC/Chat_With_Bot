using System.Transactions;
using BotServer.Data.Data;
using Microsoft.AspNetCore.Identity;

namespace BotServer.Midlewares
{
    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await next(context);
            }
        }
    }
}
