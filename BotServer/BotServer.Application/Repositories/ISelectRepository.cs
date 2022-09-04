﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.Repositories
{
    public interface ISelectRepository
    {
        public IEnumerable<T> SelectPage<T>(int page = 0, int size = 5) where T : class, IEntity;

        public IEnumerable<T> SelectByTitle<T>(string Title, int page = 0, int size = 5) where T : class, IHasTitle, IEntity;
        public IEnumerable<T> SelectByCreatedTime<T>(int page = 0, int size = 5, bool DESC=false) where T : class,IHasCreated, IEntity;

        public IEnumerable<TKind> SelectWithSortByTimeByParentId<TParents,TKind>(string parentsId, int page = 0, int size = 5, bool DESC = false) where TKind : class, IHasCreated, IHasParent, IEntity where TParents : class,IEntity;


    }
}
