﻿using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class MessageRepository : BaseModelRepository<Message>
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
