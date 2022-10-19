using Ardalis.Specification;
using BotServer.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Specifications
{
    public class GetPageSpecification<T> : Specification<T>
    {
        public GetPageSpecification(int page, int size)
        {
            Query.Skip(page * size).Take(size);
        }
    }
}
