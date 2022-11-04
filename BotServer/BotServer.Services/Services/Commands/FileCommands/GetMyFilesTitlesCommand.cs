using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.FileCommands
{
    //[Service]
    //public class GetMyFilesTitlesCommand : ICommandHandler
    //{
    //    //private readonly IFileServiceCHttpClient _httpClient;
    //   // private readonly IHttpContextAccessor _httpContextAccessor;


    //    public GetMyFilesTitlesCommand(IFileServiceCHttpClient httpClient/*,IHttpContextAccessor httpContext*/)
    //    {
    //       // _httpContextAccessor = httpContext;
    //        _httpClient = httpClient;
    //    }

    //    public bool CanProcess(ICommand command)
    //    {
    //        if (command.CommandString.ToLower().Contains("show my files"))
    //            return true;
    //        return false;
    //    }

    //    public Task<string> ProcessCommand(ICommand command)
    //    {
    //        //var head = _httpContextAccessor.HttpContext.Request.Headers.ToList();
    //        return _httpClient.GetAllFilesByUserId();
    //    }
    //}
}
