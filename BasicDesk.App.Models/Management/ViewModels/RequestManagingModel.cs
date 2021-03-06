﻿using BasicDesk.Data.Models.Requests;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Management.ViewModels
{
    public class RequestManagingModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Resolution { get; set; }

        public IEnumerable<RequestAttachment> Attachments { get; set; }

        public ICollection<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public ICollection<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

        public ICollection<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}
