﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // from the group model (Entity framework will connect the Primarykey and forign key)
        public Group Group { get; set; }
        public Guid GroupId { get; set; }
    }
}
