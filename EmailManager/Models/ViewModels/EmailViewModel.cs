using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.ViewModels
{
    public class EmailViewModel
    {
        public string Header { get; set; }
        public Email Email { get; set; }
    }
}